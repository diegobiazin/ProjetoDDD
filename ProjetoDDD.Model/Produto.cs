using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDDD.Model
{
    public class Produto
    {
        public virtual int Codigo { get; set; }
        public virtual string Descricao { get; set; }
    }
}
