using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    [Keyless]
    public class VektislijstVerrichtingen
    {
        public string? Waarde { get; set; }
        public string? Omschrijving { get; set; }
        public string? Toelichting_verplicht { get; set; }
    }
}
