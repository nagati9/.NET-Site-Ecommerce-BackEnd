using System.ComponentModel.DataAnnotations;

namespace Gestionnaire2.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; } // Identifiant unique du genre

        [Required]
        [StringLength(50)]
        public string Label { get; set; } // Libellé du genre, par exemple "Féminin", "Masculin", etc.

        // Propriété de navigation inverse vers les utilisateurs associés à ce genre
        public ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
