namespace Gestionnaire2.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vetement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nom { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Prix { get; set; }

        [Required]
        [Column("type_Id")]
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public TypeVetement Type { get; set; }

        [Required]
        [Column("Marque_Id")]
        public int MarqueId { get; set; }
        [ForeignKey("MarqueId")]
        public MarqueVetement Marque { get; set; }

    }

}
