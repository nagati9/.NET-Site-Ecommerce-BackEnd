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
        string user;
        int id;

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

            // Stocker les informations de l'utilisateur dans la session
            HttpContext.Session.SetString("UserId", utilisateur.Id.ToString());
            HttpContext.Session.SetString("UserName", utilisateur.Nom);

            return Ok(new { message = "Connexion réussie.", userId= utilisateur.Id, userName = utilisateur.Nom });
        }
        
       [HttpGet("current-user")]
public IActionResult GetCurrentUser()
{
    var userId = HttpContext.Session.GetInt32("userId");
    if (userId == null)
    {
        return Unauthorized(new { message = "Utilisateur non authentifié." });
    }

    var utilisateur = _context.utilisateurs.SingleOrDefault(u => u.Id == userId.Value);
    if (utilisateur == null)
    {
        return NotFound(new { message = "Utilisateur non trouvé." });
    }

    return Ok(new { userName = utilisateur.Nom });
}



        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Supprime toutes les sessions
            return Ok(new { message = "Déconnexion réussie." });
        }


    }
}