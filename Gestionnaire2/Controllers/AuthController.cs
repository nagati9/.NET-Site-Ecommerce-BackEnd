using Gestionnaire2.Data;
using Gestionnaire2.DTO;
using Gestionnaire2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                return BadRequest(new { message = "Cet email est déjà utilisé." });
            }

            // Création de l'utilisateur
            var utilisateur = new Utilisateur
            {
                Email = registerDto.Email,
                MotDePasse = PasswordHasher.HashPassword(registerDto.Password),
                Nom = registerDto.Nom
            };

            _context.utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            // Création du panier pour l'utilisateur
            var panier = new Panier
            {
                UtilisateurId = utilisateur.Id
            };

            _context.paniers.Add(panier);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Utilisateur et panier créés avec succès." });
        }



        [HttpPost("signin")]
        public IActionResult SignIn([FromBody] LoginDto loginDto)
        {
            var utilisateur = _context.utilisateurs.SingleOrDefault(u => u.Email == loginDto.Email);

            if (utilisateur == null || !PasswordHasher.VerifyPassword(loginDto.Password, utilisateur.MotDePasse))
            {
                return Unauthorized(new { message = "Email ou mot de passe incorrect." });
            }

            // Générer le token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("VotreCleSecretePourJWT1234567890!"); // Assurez-vous que la clé fait au moins 32 caractères
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, utilisateur.Nom),
            new Claim(ClaimTypes.NameIdentifier, utilisateur.Id.ToString())
        }),
                Expires = DateTime.UtcNow.AddHours(1), // Durée de validité du token
                Issuer = "https://localhost:7249",
                Audience = "https://localhost:7249",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                userName = utilisateur.Nom
            });
        }

        [HttpGet("current-user")]
        public IActionResult GetCurrentUser()
        {
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "Utilisateur non authentifié." });
            }

            var userName = User.Identity.Name;
            return Ok(new { userName });
        }




        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Supprime toutes les sessions
            return Ok(new { message = "Déconnexion réussie." });
        }


    }
}