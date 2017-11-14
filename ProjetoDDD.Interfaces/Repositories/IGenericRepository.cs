using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDDD.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Save(TEntity objeto);
        void Update(TEntity objeto);
        void Delete(TEntity objeto);
        IQueryable<TEntity> Query();
    }
}
