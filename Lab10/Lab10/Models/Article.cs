using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab10.Models
{
    public class Article
    {
        //[Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "To short name")]
        [MaxLength(255, ErrorMessage = " To long name, do not exceed")]
        public string Name { get; set; }
        [Required]
        [Range(0, 1000000)]
        public double Price { get; set; }

        public string Image { get; set; } = null; //domyślnie null

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        

        public Article()
        {
        }

        public Article(int id, string name, double price, string image, Category category)
        {
            Id = id;
            Name = name;
            Price = price;
            Image = image;
            Category = category;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
