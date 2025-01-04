using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Gestionnaire2.DTO
{
    public class ProfilDTO
    {
        public int Id { get; set; } // Identifiant unique de l'utilisateur

   
        public string Nom { get; set; } // Nom de l'utilisateur

       
        public string Prenom { get; set; } // Prénom de l'utilisateur

        
        public DateTime DateDeNaissance { get; set; } // Date de naissance de l'utilisateur

      
        public string Email { get; set; } // Email unique

   
        public string Telephone { get; set; } // Numéro de téléphone

        
        public string IndicatifTelephone { get; set; } // Indicatif téléphonique

        public string Adresse { get; set; } // Adresse de l'utilisateur

      
        public string Ville { get; set; } // Ville de l'utilisateur

        public string CodePostal { get; set; } // Code postal

       
        public string Pays { get; set; } // Pays
 
        public int? GenreId { get; set; }
    }
}
