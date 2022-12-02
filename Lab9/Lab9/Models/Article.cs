using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab9.Models
{

    public enum Category { Technology, Lifestyle, Health, Food, Garden, Sport, Hobby, Clothes }
    public class Article
    {

        public int Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "To short name")]
        [MaxLength(255, ErrorMessage = " To long name, do not exceed")]
        public string Name { get; set; }
        [Required]
        [Range(0,1000000)]
        public double Price { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }
        public Category Category { get; set; }

        public Article()
        {
        }

        public Article(int id, string name, double price, DateTime expirationDate, Category category)
        {
            Id = id;
            Name = name;
            Price = price;
            ExpirationDate = expirationDate;
            Category = category;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
