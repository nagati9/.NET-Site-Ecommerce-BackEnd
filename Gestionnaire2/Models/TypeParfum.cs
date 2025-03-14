﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestionnaire2.Models
{
    [Table("type_parfums")]
    public class TypeParfum
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }
    }
}
