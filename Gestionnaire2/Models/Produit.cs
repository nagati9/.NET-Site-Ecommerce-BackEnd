using System.ComponentModel.DataAnnotations.Schema;

namespace Gestionnaire2.Models
{
    public class Produit
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public int Stock { get; set; }

        [Column("type_produit")]
        public int TypeProduit { get; set; }

        // Foreign Key for TypeParfum
        [Column("Type_Parfum_Id")]
        public int? TypeParfumId { get; set; }
        [ForeignKey("TypeParfumId")]
        public virtual TypeParfum TypeParfum { get; set; }

        // Foreign Key for TypeSkincare
        [Column("Type_Skincare_Id")]
        public int? TypeSkincareId { get; set; }
        [ForeignKey("TypeSkincareId")]
        public virtual TypeSkincare TypeSkincare { get; set; }

        // Foreign Key for TypeVetement
        [Column("Type_Vetement_Id")]
        public int? TypeVetementId { get; set; }
        [ForeignKey("TypeVetementId")]
        public virtual TypeVetement TypeVetement { get; set; }

        // Foreign Key for MarqueParfum
        [Column("Marque_Parfum_Id")]
        public int? MarqueParfumId { get; set; }
        [ForeignKey("MarqueParfumId")]
        public virtual MarqueParfum MarqueParfum { get; set; }

        // Foreign Key for MarqueSkincare
        [Column("Marque_Skincare_Id")]
        public int? MarqueSkincareId { get; set; }
        [ForeignKey("MarqueSkincareId")]
        public virtual MarqueSkincare MarqueSkincare { get; set; }

        // Foreign Key for MarqueVetement
        [Column("Marque_Vetement_Id")]
        public int? MarqueVetementId { get; set; }
        [ForeignKey("MarqueVetementId")]
        public virtual MarqueVetement MarqueVetement { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Photo_Path")]
        public string PhotoPath { get; set; }
    }

}
