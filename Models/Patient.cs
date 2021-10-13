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

        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Gelieve naam in te vullen")]
        public string Naam { get; set; }

        [Range(0, 150, ErrorMessage = "Gelieve een leeftijd in te vullen")]
        [Required(ErrorMessage = "Gelieve leeftijd in te vullen")]
        [Column(TypeName = "int")]
        public int Leeftijd { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        [Required(ErrorMessage = "Gelieve omschrijving in te vullen")]
        public string Omschrijving { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required(ErrorMessage = "Gelieve diagnose code in te vullen")]
        public string DiagnoseCode { get; set; }

        [Column(TypeName = "varchar(10)")]
        [Required(ErrorMessage = "Gelieve medewerker of student in te vullen")]
        public string MedewerkerOfStudent { get; set; }

        [Column(TypeName = "varchar(200)")]
        [Required(ErrorMessage = "Gelieve in te vullen door wie de intake gedaan word")]
        public string IntakeGedaanDoor { get; set; }

        [Column(TypeName = "varchar(200)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string onderSupervisieVan { get; set; }

        [Column(TypeName = "varchar(200)")]
        [Required(ErrorMessage = "Gelieve hoofdbehandelaar in te vullen")]
        public string HoofdBehandelaar { get; set; }

        [Column(TypeName = "DateTime")]
        [Required(ErrorMessage = "Gelieve datum aanmelding  in te vullen")]
        public DateTime DatumAanmelding { get; set; }

        [Column(TypeName = "Datetime")]
        public DateTime? DatumOntslag { get; set; }

        [Column(TypeName = "varchar(500)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string Opmerkingen { get; set; }

        [Column(TypeName = "varchar(200)")]
        [Required(ErrorMessage = "Gelieve behandelplan in te vullen")]
        public string Behandelplan { get; set; }

        [Column(TypeName = "varchar(200)")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Required(AllowEmptyStrings = true)]
        public string Behandeling { get; set; }



        public string Slug => Naam?.Replace(' ', '-').ToLower() + '-' + Leeftijd.ToString();
    }
}
