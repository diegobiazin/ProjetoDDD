using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDDD.Interfaces
{
    public interface IConnection
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        IQueryable<T> Query<T>();
        void Save(object objeto);
        void Update(object objeto);
        void Delete(object objeto);
        void SaveOrUpdate(object objeto);
        object ExecuteFunction(string functionName, Dictionary<string, object> parameters);
    }
}
