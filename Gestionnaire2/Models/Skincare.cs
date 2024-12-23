using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestionnaire2.Models
{
    [Table("skincare")]
    public class Skincare
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
        [Column("type_id")]
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
     
        public TypeSkincare Type { get; set; }

        [Required]
        [Column("marque_id")]
        public int MarqueId { get; set; }

        [ForeignKey("MarqueId")]
       
        public MarqueSkincare Marque { get; set; }
        [Column("photo_path")]
        public string PhotoPath { get; set; }
    }


}
