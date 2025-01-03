using System.ComponentModel.DataAnnotations.Schema;

namespace Gestionnaire2.DTO
{
    public class ProfilDTO
    {
        public int Id { get; set; } // Identifiant unique de l'utilisateur
        public string Nom { get; set; } // Nom de l'utilisateur
        public string Email { get; set; } // Email unique
    
        public string MotDePasse { get; set; } // Mot de passe haché
    }
}
