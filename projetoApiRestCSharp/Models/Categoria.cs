using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace projetoApiRestCSharp.Models
{
    public class Categoria
    {
        public ulong id { get; set; }
        [StringLength(100)]
        public string titulo { get; set; }                
    }
}
