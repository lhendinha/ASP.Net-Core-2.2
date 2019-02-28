using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core22.Models
{
    public class Genre
    {
        [Column("GenreId"), Key, Required]
        public int GenreId { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(30)]
        public string GenreName { get; set; }
    }
}
