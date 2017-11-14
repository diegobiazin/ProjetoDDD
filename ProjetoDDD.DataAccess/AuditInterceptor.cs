using NHibernate;
using NHibernate.Event.Default;
using NHibernate.Type;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoDDD.Interfaces;

namespace ProjetoDDD.DataAccess
{
    public class AuditInterceptor : EmptyInterceptor
    {
        public override bool OnFlushDirty(object entity,
                                          object id,
                          object[] currentState,
                          object[] previousState,
                          string[] propertyNames,
                          IType[] types)
        {
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    if ("DataCadastro".Equals(propertyNames[i]))
                    {
                    currentState[i] = previousState[i];
                        return true;
                    }
                }
        
            return false;
        }

        public override bool OnSave(object entity,
                                    object id,
                    object[] state,
                    string[] propertyNames,
                    IType[] types)
        {
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    if ("DataCadastro".Equals(propertyNames[i]))
                    {
                        state[i] = DateTime.Now;
                        return true;
                    }
                }
            
            return false;
        }

        public override NHibernate.SqlCommand.SqlString OnPrepareStatement(NHibernate.SqlCommand.SqlString sql)
        {
            Trace.WriteLine(sql.ToString());
            return sql;
        }

    }
}
