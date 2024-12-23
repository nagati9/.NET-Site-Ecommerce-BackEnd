using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestionnaire2.Models
{
    [Table("type_skincare")]
    public class TypeSkincare
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

    }

}
