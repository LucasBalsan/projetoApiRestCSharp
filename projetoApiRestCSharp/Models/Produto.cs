using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace projetoApiRestCSharp.Models
{
    public class Produto
    {        
        public ulong id { get; set; }
        [StringLength(100)]
        public string titulo { get; set; }
        public string descricao { get; set; }
        public double altura { get; set; }
        public double largura { get; set; }
        public double comprimento { get; set; }
        public double peso { get; set; }
        public ulong codigoBarras { get; set; }
        public double valor { get; set; }        
        public DateTime dataAquisicao { get; set; }               
        public string imagemCaminho { get; set; }
        public Categoria categoria { get; set; }
    }
}
