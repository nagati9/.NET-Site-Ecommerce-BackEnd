using System.ComponentModel.DataAnnotations.Schema;

namespace Gestionnaire2.Models
{
    [Table("paniers")]
    public class Panier
    {
        public int Id { get; set; } // Identifiant unique du panier
        [Column("utilisateur_id")]
        public int UtilisateurId { get; set; } // Clé étrangère vers Utilisateur

        // Propriété de navigation
        public Utilisateur Utilisateur { get; set; }

        public List<PanierProduit> Produits { get; set; } // Liste des produits dans le panier
    }
}
