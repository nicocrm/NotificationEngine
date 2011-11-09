using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage.Platform;
using Sage.Platform.Orm;
using System.Collections;

namespace NotificationEngine.DataSource
{
    public class HqlDataSource : IWorkItemDataSource
    {
        private String _query;

        public IEnumerable<IRecord> Read()
        {
            using (var sess = new SessionScopeWrapper())
            {
                var qry = sess.CreateQuery(_query);
                IList lst = qry.List();
                foreach (object o in lst)
                    yield return new HqlRecordWrapper(o);
            }
        }

        public List<string> GetFields()
        {
            throw new NotImplementedException();
        }

        public string EscapeLiteral(string literalValue)
        {
            return literalValue == null ? "" : literalValue.Replace("'", "''");
        }

        public void LoadConfiguration(string configBlob)
        {
            throw new NotImplementedException();
        }
    }
}
