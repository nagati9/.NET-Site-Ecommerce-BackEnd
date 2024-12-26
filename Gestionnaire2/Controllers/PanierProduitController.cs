using Gestionnaire2.Data;
using Gestionnaire2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gestionnaire2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PanierProduitController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PanierProduitController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // POST: api/PanierProduit/AddToPanier/{produitId}/{quantite}
        [HttpPost("AddToPanier/{produitId}/{quantite}")]
        public async Task<IActionResult> AddToPanier(int produitId, int quantite)
        {
            // Vérifier si l'utilisateur est connecté
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "Utilisateur non authentifié." });
            }

            try
            {
                // Récupérer l'utilisateur connecté
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "Utilisateur non authentifié." });
                }
                var userId = int.Parse(userIdClaim);

                // Vérifier si l'utilisateur a un panier
                var panier = _context.paniers.FirstOrDefault(p => p.UtilisateurId == userId);
                if (panier == null)
                {
                    return NotFound(new { message = "Panier introuvable pour cet utilisateur." });
                }

                // Vérifier que le produit existe
                var produit = await _context.Produits.FindAsync(produitId);
                if (produit == null)
                {
                    return NotFound(new { message = "Produit introuvable." });
                }

                // Ajouter le produit au panier
                var panierProduitEntity = new PanierProduit
                {
                    PanierId = panier.Id,
                    ProduitId = produitId,
                    Quantite = quantite
                };

                _context.paniersproduits.Add(panierProduitEntity);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Produit ajouté au panier avec succès." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne lors de l'ajout au panier.", details = ex.Message });
            }
        }

        [HttpGet("GetProduitsPanier")]
        public IActionResult GetProduitsPanier()
        {
            // Vérifier si l'utilisateur est connecté
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "Utilisateur non authentifié." });
            }

            // Récupérer l'utilisateur connecté
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")?.Value);

            // Vérifier si l'utilisateur a un panier
            var panier = _context.paniers.FirstOrDefault(p => p.UtilisateurId == userId);
            if (panier == null)
            {
                return NotFound(new { message = "Panier introuvable pour cet utilisateur." });
            }

            // Récupérer les produits dans le panier
            var produitsPanier = _context.paniersproduits
                .Where(pp => pp.PanierId == panier.Id)
                .Select(pp => new
                {
                    pp.Id,
                    pp.ProduitId,
                    pp.Quantite,
                    ProduitNom = _context.Produits.FirstOrDefault(p => p.Id == pp.ProduitId).Nom,
                    ProduitPrix = _context.Produits.FirstOrDefault(p => p.Id == pp.ProduitId).Prix
                })
                .ToList();

            if (produitsPanier.Count == 0)
            {
                return Ok(new { message = "Le panier est vide." });
            }

            return Ok(produitsPanier);
        }

    }
}
