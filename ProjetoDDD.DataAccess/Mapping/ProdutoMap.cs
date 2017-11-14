using FluentNHibernate.Mapping;
using ProjetoDDD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHCloud.DataAccess.Mapping
{
    public class ProdutoMap : ClassMap<Produto>
    {
        public ProdutoMap()
        {
            Table("Produto");

            Id(x => x.Codigo).Column("Codigo")
               .GeneratedBy.Increment()
               .UnsavedValue(0);

            Map(x => x.Descricao).Column("Descricao");
        }
    }
}
