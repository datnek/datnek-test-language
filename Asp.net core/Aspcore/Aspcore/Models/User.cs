using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aspcore.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string slug { get; set; }
        [Required]
        public string username { get; set; }
        [Required, EmailAddress]
        public string email { get; set; }
        [MinLength(3), Required]
        public string password { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        public IList<Language> languages { get; set; }

        [NotMapped]
        public string token { get; set; }


    }
}
