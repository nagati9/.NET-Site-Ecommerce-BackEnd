using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Gestionnaire2.Models
{
    public class Utilisateur
    {
        [Key]
        [Column("id")]
        public int Id { get; set; } // Identifiant unique de l'utilisateur

        [Required]
        [StringLength(100)]
        [Column("nom")]
        public string Nom { get; set; } // Nom de l'utilisateur

        [Required]
        [StringLength(100)]
        [Column("prenom")]
        public string Prenom { get; set; } // Prénom de l'utilisateur

        [DataType(DataType.Date)]
        [Column("dateDeNaissance")]
        public DateOnly DateDeNaissance { get; set; } // Date de naissance de l'utilisateur

        [Required]
        [StringLength(255)]
      
        [Column("email")]
        public string Email { get; set; } // Email unique

        [StringLength(20)]
        [Phone]
        [Column("telephone")]
        public string Telephone { get; set; } // Numéro de téléphone

        [StringLength(10)]
        [Column("indicatifTelephone")]
        public string IndicatifTelephone { get; set; } // Indicatif téléphonique

        [StringLength(255)]
        [Column("adresse")]
        public string Adresse { get; set; } // Adresse de l'utilisateur

        [StringLength(100)]
        [Column("ville")]
        public string Ville { get; set; } // Ville de l'utilisateur

        [StringLength(10)]
        [Column("codePostal")]
        public string CodePostal { get; set; } // Code postal

        [StringLength(100)]
        [Column("pays")]
        public string Pays { get; set; } // Pays

        [Required]
        [Column("mot_de_passe")]
        [JsonIgnore]
        public string MotDePasse { get; set; } // Mot de passe haché

        // Clé étrangère vers la table Genre
        [Column("genre_id")]
        public int? GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; } // Relation avec l'entité Genre


    }
}
