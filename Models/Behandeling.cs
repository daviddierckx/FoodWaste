using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public class Behandeling
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve Type  in te vullen")]
        public string Type { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve omschrijving  in te vullen")]
        public string Omschrijving { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve Oefenzaal of behandelruimte  in te vullen")]
        public string OefenzaalOfBehandel { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve bijzonderheden  in te vullen")]
        public string Bijzonderheden { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve behandeling uitgevoerd door in te vullen")]
        public string BehandelingUitgevoerdDoor { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve datum uitgevoer op  in te vullen")]
        public string BehandelingUitgevoerdDatum { get; set; }


        [ForeignKey("PatientId")]
        [Column(TypeName = "int")]
        public int PatientId { get; set; }


        public virtual Patient Patient { get; set; }
    }
}
