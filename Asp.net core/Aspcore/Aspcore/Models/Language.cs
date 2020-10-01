using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aspcore.Models
{
    public class Language
    {
        [Key]
        public int id { get; set; }
        public User user { get; set; }
        [Required]
        public int userId { get; set; }
        [Required]
        public string slug { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public double speak { get; set; }
        [Required]
        public double read { get; set; }
        [Required]
        public double understand { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
