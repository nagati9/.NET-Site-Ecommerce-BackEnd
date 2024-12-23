using System.ComponentModel.DataAnnotations.Schema;

namespace Gestionnaire2.Models
{
    public class Test
    {
        public int Id { get; set; } // Correspond à la colonne "id"
        public string Name { get; set; } // Correspond à la colonne "name"
        public int Age { get; set; } // Correspond à la colonne "age"
        public string Email { get; set; } // Correspond à la colonne "email"

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
