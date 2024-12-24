using Gestionnaire2.Data;
using Gestionnaire2.DTO;
using Gestionnaire2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] RegisterDto registerDto)
        {
            if (_context.utilisateurs.Any(u => u.Email == registerDto.Email))
            {
                // Retourne un objet JSON pour une erreur
                return BadRequest(new { message = "Cet email est déjà utilisé." });
            }

            var utilisateur = new Utilisateur
            {
                Email = registerDto.Email,
                MotDePasse = PasswordHasher.HashPassword(registerDto.Password),
                Nom = registerDto.Nom
            };

            _context.utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            // Retourne un objet JSON pour le succès
            return Ok(new { message = "Utilisateur créé avec succès." });
        }


        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] LoginDto loginDto)
        {
            var utilisateur = _context.utilisateurs.SingleOrDefault(u => u.Email == loginDto.Email);

            if (utilisateur == null)
            {
                return Unauthorized(new { message = "Email ou mot de passe incorrect." });
            }

            if (!PasswordHasher.VerifyPassword(loginDto.Password, utilisateur.MotDePasse))
            {
                return Unauthorized(new { message = "Email ou mot de passe incorrect." });
            }

            // Si vous utilisez des jetons JWT :
            // var token = GenerateJwtToken(utilisateur);
            // return Ok(new { token });

            // Sinon, retournez un message de succès :
            return Ok(new { message = "Connexion réussie.", userId = utilisateur.Id });
        }


    }
}