using Gestionnaire2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitController : ControllerBase
    {
        private AppDbContext _context;

        public ProduitController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/Tests
        // GET: api/Produit
        [HttpGet]
        public async Task<IActionResult> GetProduits()
        {
            var produits = await _context.Produits
                .Include(p => p.TypeParfum)
                .Include(p => p.TypeSkincare)
                .Include(p => p.TypeVetement)
                .Include(p => p.MarqueParfum)
                .Include(p => p.MarqueSkincare)
                .Include(p => p.MarqueVetement)
                .Select(p => new
                {
                    p.Id,
                    p.Nom,
                    p.Prix,
                    p.Stock,
                    p.TypeProduit,
                    TypeParfum = p.TypeParfum != null ? p.TypeParfum.Nom : null,
                    TypeSkincare = p.TypeSkincare != null ? p.TypeSkincare.Nom : null,
                    TypeVetement = p.TypeVetement != null ? p.TypeVetement.Nom : null,
                    MarqueParfum = p.MarqueParfum != null ? p.MarqueParfum.Nom : null,
                    MarqueSkincare = p.MarqueSkincare != null ? p.MarqueSkincare.Nom : null,
                    MarqueVetement = p.MarqueVetement != null ? p.MarqueVetement.Nom : null,
                    p.Description,
                    p.PhotoPath
                })
                .ToListAsync();

            return Ok(produits);
        }
        [HttpGet("GetProduitParType/{type}")]
        //---------------------------------------------------------------------------------------------------------------
        public async Task<IActionResult> GetProduitParType(int type)
        {
            var produits = await _context.Produits
                .Include(p => p.TypeParfum)
                .Include(p => p.TypeSkincare)
                .Include(p => p.TypeVetement)
                .Include(p => p.MarqueParfum)
                .Include(p => p.MarqueSkincare)
                .Include(p => p.MarqueVetement).Where(p=>p.TypeProduit==type)
                .Select(p => new
                {
                    p.Id,
                    p.Nom,
                    p.Prix,
                    p.Stock,
                    p.TypeProduit,
                    TypeParfum = p.TypeParfum != null ? p.TypeParfum.Nom : null,
                    TypeSkincare = p.TypeSkincare != null ? p.TypeSkincare.Nom : null,
                    TypeVetement = p.TypeVetement != null ? p.TypeVetement.Nom : null,
                    MarqueParfum = p.MarqueParfum != null ? p.MarqueParfum.Nom : null,
                    MarqueSkincare = p.MarqueSkincare != null ? p.MarqueSkincare.Nom : null,
                    MarqueVetement = p.MarqueVetement != null ? p.MarqueVetement.Nom : null,
                    p.Description,
                    p.PhotoPath
                })
                .ToListAsync();

            return Ok(produits);
        }
        //-----------------------------------------------------------------------------------------------------------------

    }
}
