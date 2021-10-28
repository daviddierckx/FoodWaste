using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public class Opmerkingen
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve Opmerking  in te vullen")]
        public string Opmerking { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve Datum  in te vullen")]
        public string Datum { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve Opmerkingen gemaakt door  in te vullen")]
        public string OpmerkingenGemaaktDoor { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve Zichtbaar voor patiënt  in te vullen")]
        public string ZichtbaarVoorPatiënt { get; set; }
        

        [ForeignKey("PatientId")]
        [Column(TypeName = "int")]
        public int PatientId { get; set; }


        public virtual Patient Patient { get; set; }
    }
}
