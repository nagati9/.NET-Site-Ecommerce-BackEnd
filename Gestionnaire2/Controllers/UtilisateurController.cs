using Gestionnaire2.Data;
using Gestionnaire2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private AppDbContext _context;

        public UtilisateurController(AppDbContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public IActionResult Get()
        {
            var utilisateurs = _context.utilisateurs.ToList();
            return Ok(utilisateurs);
        }

        // GET: api/Utilisateur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
            var utilisateur = await _context.utilisateurs.FirstOrDefaultAsync(u => u.Id == id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // POST: api/Utilisateur
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            if (await _context.utilisateurs.AnyAsync(u => u.Email == utilisateur.Email))
            {
                return BadRequest("Un utilisateur avec cet email existe déjà.");
            }

            // Ajouter l'utilisateur
            _context.utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            // Créer un panier vide pour cet utilisateur
            var panier = new Panier
            {
                UtilisateurId = utilisateur.Id,
                Produits = new List<PanierProduit>()
            };
            _context.paniers.Add(panier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUtilisateur), new { id = utilisateur.Id }, utilisateur);
        }


        // PUT: api/Utilisateur/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.Id)
            {
                return BadRequest();
            }

            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilisateurExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Utilisateur/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var utilisateur = await _context.utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            _context.utilisateurs.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilisateurExists(int id)
        {
            return _context.utilisateurs.Any(e => e.Id == id);
        }
    }
}

