using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Beca
    {
        public byte IdBeca { get; set; }
        public string? Tipo { get; set; }
        public List<object>? Becas { get; set; }
    }
}
