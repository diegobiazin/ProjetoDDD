using ProjetoDDD.Interfaces;
using ProjetoDDD.Interfaces.Repositories;
using ProjetoDDD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RHCloud.DataAccess.Repositories
{
    public class ProdutoRepository : GenericRepository<Produto>, IProdutoRepository
    {
        private readonly IConnection _iConnection;
        public ProdutoRepository(IConnection iConnection) : base(iConnection)
        {
            _iConnection = iConnection;
        }
    }
}
