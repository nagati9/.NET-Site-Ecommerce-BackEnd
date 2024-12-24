using Gestionnaire2.Data;
using Gestionnaire2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanierController : ControllerBase
    {
        private AppDbContext _context;
        public PanierController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Panier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Panier>>> GetPaniers()
        {
            return await _context.paniers.Include(p => p.Produits).ToListAsync();
        }

        // GET: api/Panier/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Panier>> GetPanier(int id)
        {
            var panier = await _context.paniers.Include(p => p.Produits)
                                               .FirstOrDefaultAsync(p => p.Id == id);

            if (panier == null)
            {
                return NotFound();
            }

            return panier;
        }

        // POST: api/Panier
        [HttpPost]
        public async Task<ActionResult<Panier>> PostPanier(Panier panier)
        {
            _context.paniers.Add(panier);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPanier), new { id = panier.Id }, panier);
        }

        // PUT: api/Panier/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPanier(int id, Panier panier)
        {
            if (id != panier.Id)
            {
                return BadRequest();
            }

            _context.Entry(panier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PanierExists(id))
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

        // DELETE: api/Panier/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePanier(int id)
        {
            var panier = await _context.paniers.FindAsync(id);
            if (panier == null)
            {
                return NotFound();
            }

            _context.paniers.Remove(panier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PanierExists(int id)
        {
            return _context.paniers.Any(e => e.Id == id);
        }
    }
}

