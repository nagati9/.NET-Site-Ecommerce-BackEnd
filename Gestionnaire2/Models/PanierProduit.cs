using System.ComponentModel.DataAnnotations.Schema;

namespace Gestionnaire2.Models
{

    [Table("panier_produits")]
    public class PanierProduit
    {
        public int Id { get; set; } // Identifiant unique pour cette entrée
        [Column("panier_id")]
        public int PanierId { get; set; } // Référence au panier
        [Column("produit_id")]

        public int ProduitId { get; set; } // Référence au produit (parfum, skincare, etc.)
        public int Quantite { get; set; } // Quantité de ce produit dans le panier

        // Navigation properties
        public Panier Panier { get; set; }
        
    }
}
