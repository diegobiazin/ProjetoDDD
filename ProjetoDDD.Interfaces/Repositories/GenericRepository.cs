using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDDD.Interfaces.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    {
        private readonly IConnection _iConnection;
        public GenericRepository(IConnection iConnection)
        {
            _iConnection = iConnection;
        }

        public void Delete(TEntity objeto)
        {
            _iConnection.Delete(objeto);
        }

        public IQueryable<TEntity> Query()
        {
            return _iConnection.Query<TEntity>();
        }

        public void Save(TEntity objeto)
        {
            _iConnection.Save(objeto);
        }

        public void Update(TEntity objeto)
        {
            _iConnection.Update(objeto);
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
