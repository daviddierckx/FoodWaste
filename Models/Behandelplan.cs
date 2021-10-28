using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvansFysio.Models
{
    public class Behandelplan
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve behandelplan naam in te vullen")]
        public string BehandelPlanNaam { get; set; }
        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Gelieve de duur van elke sessie in te vullen")]
        public int Duur { get; set; }
        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Gelieve hoeveel behandelingen per week in te vullen")]
        public int Hoeveel { get; set; }
        [Column(TypeName = "int")]
        public int PatientId;


        public virtual Patient Patient { get; set; }
    }
}