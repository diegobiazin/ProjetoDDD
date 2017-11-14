using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoDDD.Interfaces;
using NHibernate.Cfg;
using System.IO;
using RHCloud.DataAccess.Mapping;

namespace ProjetoDDD.DataAccess
{
    public class Connection : IConnection, IDisposable
    {
        private FluentConfiguration _configuration;
        private ISessionFactory SessionFactory { get; set; }

        private ISession session;
        public ISession Session
        {
            get
            {
                if (session == null)
                    session = SessionFactory.OpenSession();
                return session;
            }
            private set { session = value; }
        }

        private ITransaction Transaction { get; set; }

        public Connection()
        {
            string connString = string.Format("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={{HOST}})(PORT={{PORT}})))" +
                   "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORATST)));User Id={{ID}};Password={{PASSWORD}};");

            _configuration = Fluently.Configure()
                .Database(OracleDataClientConfiguration.Oracle10.ConnectionString(c => c.Is(connString))
                .Driver<NHibernate.Driver.OracleClientDriver>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProdutoMap>())
                .ExposeConfiguration(x => { x.SetInterceptor(new AuditInterceptor()); });
            SessionFactory = _configuration.BuildSessionFactory();
        }

        public void BeginTransaction()
        {
            if (Transaction != null)
                throw new Exception("Transação com o banco já iniciada.");
            Transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            if (Transaction == null)
                throw new Exception("Não existe transação para ser comitada.");
            Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            Dispose();
        }

        public void Delete(object objeto)
        {
            if (Transaction == null)
                DeleteSemTransaction(objeto);
            else
                DeleteComTransaction(objeto);
        }

        private void DeleteComTransaction(object objeto)
        {
            Session.Flush();
            Session.Clear();
            Session.Delete(objeto);
        }

        private void DeleteSemTransaction(object objeto)
        {
            BeginTransaction();
            try
            {
                DeleteComTransaction(objeto);
                Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public IQueryable<T> Query<T>()
        {
            return Session.Query<T>();
        }

        public void Save(object objeto)
        {
            if (Transaction == null)
                SaveSemTransaction(objeto);
            else
                SaveComTransaction(objeto);
        }

        private void SaveComTransaction(object objeto)
        {
            Session.Save(objeto);
        }

        private void SaveSemTransaction(object objeto)
        {
            BeginTransaction();
            try
            {
                SaveComTransaction(objeto);
                Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public void SaveOrUpdate(object objeto)
        {
            if (Transaction == null)
                SaveOrUpdateSemTransaction(objeto);
            else
                SaveOrUpdateComTransaction(objeto);
        }

        private void SaveOrUpdateComTransaction(object objeto)
        {
            Session.SaveOrUpdate(objeto);
        }

        private void SaveOrUpdateSemTransaction(object objeto)
        {
            BeginTransaction();
            try
            {
                SaveOrUpdateComTransaction(objeto);
                Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public void Update(object objeto)
        {
            if (Transaction == null)
                UpdateSemTransaction(objeto);
            else
                UpdateComTransaction(objeto);
        }

        private void UpdateComTransaction(object objeto)
        {
            Session.Update(objeto);
        }

        private void UpdateSemTransaction(object objeto)
        {
            BeginTransaction();
            try
            {
                UpdateComTransaction(objeto);
                Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        public void Dispose()
        {
            if (Transaction.IsActive)
                Transaction.Rollback();
            Transaction = null;
            Session.Close();
            Session = null;
        }

        public object ExecuteFunction(string functionName, Dictionary<string, object> parameters)
        {
            var qry = Session.GetNamedQuery("GetSaldoBanco");
            foreach (var item in parameters)
            {
                qry.SetParameter(item.Key, item.Value);
            }
                     
            var result = qry.List();
            return result;
        }
    }
}
