using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoDDD.Interfaces.Repositories;

namespace ProjetoDDD.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        IProdutoRepository ProdutoRepository { get; }
    }
}
