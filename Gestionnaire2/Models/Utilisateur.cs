using System.ComponentModel.DataAnnotations.Schema;

namespace Gestionnaire2.Models
{
    public class Utilisateur
    {
        public int Id { get; set; } // Identifiant unique de l'utilisateur
        public string Nom { get; set; } // Nom de l'utilisateur
        public string Email { get; set; } // Email unique
        [Column("mot_de_passe")]
        public string MotDePasse { get; set; } // Mot de passe haché

        
    }
}
