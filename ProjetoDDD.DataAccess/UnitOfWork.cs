using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoDDD.Interfaces;
using ProjetoDDD.Interfaces.Repositories;
using RHCloud.DataAccess.Repositories;

namespace ProjetoDDD.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConnection _iConnection;
        public UnitOfWork(IConnection iConnection)
        {
            _iConnection = iConnection;
        }

        private IProdutoRepository _produtoRepository;
        public IProdutoRepository ProdutoRepository
        {
            get
            {
                if (_produtoRepository == null)
                    _produtoRepository = new ProdutoRepository(_iConnection);
                return _produtoRepository;
            }
        }

        public void BeginTransaction()
        {
            _iConnection.BeginTransaction();
        }

        public void Commit()
        {
            _iConnection.Commit();
        }

        public void Rollback()
        {
            _iConnection.Rollback();
        }
    }
}
