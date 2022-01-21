using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    public class Beschikbaar
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName="bit")]
        public bool BeschikbaarOpDieDag { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Dag { get; set; }

        [Column(TypeName = "time(1)")]
        public TimeSpan BeginTijd { get; set; }
        [Column(TypeName = "time(1)")]
        public TimeSpan EindTijd { get; set; }

   
    }
}
