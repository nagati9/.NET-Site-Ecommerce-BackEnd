using System.ComponentModel.DataAnnotations;

namespace Gestionnaire2.DTO
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Nom { get; set; }

        [Required]
        [StringLength(100)]
        public string Prenom { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateDeNaissance { get; set; }

        [StringLength(20)]
        [Phone]
        public string Telephone { get; set; }

        [StringLength(10)]
        public string IndicatifTelephone { get; set; }

        [StringLength(255)]
        public string Adresse { get; set; }

        [StringLength(100)]
        public string Ville { get; set; }

        [StringLength(10)]
        public string CodePostal { get; set; }

        [StringLength(100)]
        public string Pays { get; set; }

        public int? GenreId { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
