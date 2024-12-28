using Gestionnaire2.Data;
using Gestionnaire2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

                // Vérifier si le produit est déjà dans le panier
                var panierProduit = _context.paniersproduits
                    .FirstOrDefault(pp => pp.PanierId == panier.Id && pp.ProduitId == produitId);

                if (panierProduit != null)
                {
                    // Si le produit existe, augmenter la quantité
                    panierProduit.Quantite += quantite;
                }
                else
                {
                    // Sinon, ajouter une nouvelle entrée
                    panierProduit = new PanierProduit
                    {
                        PanierId = panier.Id,
                        ProduitId = produitId,
                        Quantite = quantite
                    };
                    _context.paniersproduits.Add(panierProduit);
                }

                await _context.SaveChangesAsync();
                return Ok(new { message = "Produit ajouté au panier avec succès." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne lors de l'ajout au panier.", details = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("GetProduitsPanier")]
        public IActionResult GetProduitsPanier()
        {
            // Vérifiez si l'utilisateur est connecté
            if (User.Identity == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "Utilisateur non authentifié." });
            }

            // Récupérez l'utilisateur connecté
            var idClaim = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(idClaim))
            {
                return Unauthorized(new { message = "Impossible de récupérer l'identifiant de l'utilisateur." });
            }

            var userId = int.Parse(idClaim);

            // Vérifiez si l'utilisateur a un panier
            var panier = _context.paniers.FirstOrDefault(p => p.UtilisateurId == userId);
            if (panier == null)
            {
                return NotFound(new { message = "Panier introuvable pour cet utilisateur." });
            }

            // Récupérez les produits dans le panier
            var produitsPanier = _context.paniersproduits
                .Where(pp => pp.PanierId == panier.Id)
                .Select(pp => new
                {
                    pp.Id,
                    pp.ProduitId,
                    pp.Quantite,
                    ProduitNom = _context.Produits.FirstOrDefault(p => p.Id == pp.ProduitId).Nom,
                    ProduitPrix = _context.Produits.FirstOrDefault(p => p.Id == pp.ProduitId).Prix,
                    ProduitPhotos = _context.Produits.FirstOrDefault(p => p.Id == pp.ProduitId).PhotoPath,
                    TypeProduit = _context.Produits.FirstOrDefault(p => p.Id == pp.ProduitId).TypeProduit,
                    MarqueSkincare = _context.Produits.FirstOrDefault(p => p.Id == pp.ProduitId).MarqueSkincare,
                    MarqueParfum = _context.Produits.FirstOrDefault(p => p.Id == pp.ProduitId).MarqueParfum,
                    MarqueVetement = _context.Produits.FirstOrDefault(p => p.Id == pp.ProduitId).MarqueVetement,
                    Description = _context.Produits.FirstOrDefault(p => p.Id == pp.ProduitId).Description,
                    Stock = _context.Produits.FirstOrDefault(p => p.Id == pp.ProduitId).Stock

                })
                .ToList();

            if (produitsPanier.Count == 0)
            {
                return Ok(new { message = "Le panier est vide." });
            }

            return Ok(produitsPanier);
        }

        [Authorize]
        [HttpDelete("DeleteFromPanier/{produitId}/{quantite}")]
        public async Task<IActionResult> DeleteFromPanier(int produitId, int quantite)
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

                // Vérifier si le produit est dans le panier
                var panierProduit = _context.paniersproduits
                    .FirstOrDefault(pp => pp.PanierId == panier.Id && pp.ProduitId == produitId);

                if (panierProduit == null)
                {
                    return NotFound(new { message = "Produit non trouvé dans le panier." });
                }

                if (panierProduit.Quantite > quantite)
                {
                    // Réduire la quantité
                    panierProduit.Quantite -= quantite;
                }
                else
                {
                    // Supprimer le produit du panier
                    _context.paniersproduits.Remove(panierProduit);
                }

                await _context.SaveChangesAsync();
                return Ok(new { message = "Produit mis à jour dans le panier avec succès." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne lors de la mise à jour du panier.", details = ex.Message });
            }
        }



    }
}
