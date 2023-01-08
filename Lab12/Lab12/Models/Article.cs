using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lab12.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "To short name")]
        [MaxLength(255, ErrorMessage = " To long name, do not exceed")]
        public string Name { get; set; }

        [Required]
        [Range(0, 1000000)]
        public double Price { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; } = null;
        public string? filePath { get; set; } = null;

        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        [NotMapped]
        public string Photo =>
            filePath is { } path ? $"/upload/{path}" : "/image/NoImage.jpg";

    }
}
