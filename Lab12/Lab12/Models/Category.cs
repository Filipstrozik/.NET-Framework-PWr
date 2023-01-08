using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "To short name")]
        [MaxLength(255, ErrorMessage = " To long name, do not exceed")]
        [Required]
        public string Name { get; set; }

        public ICollection<Article> Articles { get; set; }

        public Category()
        {
        }
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
            Articles = new List<Article>();
        }
    }
}
