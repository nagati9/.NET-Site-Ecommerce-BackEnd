namespace Gestionnaire2.Models
{
    public class PanierProduit
    {
        public int Id { get; set; } // Identifiant unique pour cette entrée
        public int PanierId { get; set; } // Référence au panier
        public int ProduitId { get; set; } // Référence au produit (parfum, skincare, etc.)
        public int Quantite { get; set; } // Quantité de ce produit dans le panier

        // Navigation properties
        public Panier Panier { get; set; }
        public Parfum Parfs { get; set; }
        public Skincare skins { get; set; }
        public Vetement vets { get; set; }
    }
}
