using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Gelieve naam in te vullen")]
        public string Naam { get; set; }
        [Range(0, 150, ErrorMessage = "Gelieve een leeftijd in te vullen")]
        [Required(ErrorMessage = "Gelieve leeftijd in te vullen")]
        public int? Leeftijd { get; set; }

        public string Slug => Naam?.Replace(' ', '-').ToLower() + '-' + Leeftijd?.ToString();
    }
}
