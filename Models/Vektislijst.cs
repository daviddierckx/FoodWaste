using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvansFysio.Models
{
    [Keyless]
    public class Vektislijst
    {
        public int? Code { get; set; }
        public string? lichaamslocalisatie { get; set; }
        public string? pathologie { get; set; }
    }
}
