namespace Gestionnaire2.Models
{
    public class Panier
    {
        public int Id { get; set; } // Identifiant unique du panier
        public int UtilisateurId { get; set; } // Référence vers l'utilisateur propriétaire du panier

        // Navigation property
        public Utilisateur Utilisateur { get; set; }
        public List<PanierProduit> Produits { get; set; } // Liste des produits dans le panier
    }
}
