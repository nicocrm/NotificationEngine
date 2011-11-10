using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage.Platform;
using Sage.Platform.Orm;
using System.Collections;
using System.Xml.Linq;
using NotificationEngine.Interfaces;

namespace NotificationEngine.DataSource
{
    public class EntityDataSource : IWorkItemDataSource
    {
        private String _entity;
        private String _where;

        public IEnumerable<IRecord> Read(WorkItem workItem)
        {
            String query = String.Format("from {0} where {1}", _entity, workItem.EvaluateLiterals(_where, this));

            using (var sess = new SessionScopeWrapper())
            {
                var qry = sess.CreateQuery(query);
                IList lst = qry.List();
                foreach (object o in lst)
                    yield return new EntityRecordWrapper(o);
            }
        }

        public List<string> GetFields()
        {
            return null;
        }

        public string EscapeLiteral(string literalValue)
        {
            return literalValue == null ? "" : literalValue.Replace("'", "''");
        }

        public void LoadConfiguration(string configBlob)
        {
            XDocument xml = XDocument.Parse(configBlob);
            XElement e = xml.Root.Element("Entity");
            if (e != null)
                _entity = e.Value;
            else
                throw new Exception("Missing Entity node");
            e = xml.Root.Element("Where");
            if (e != null)
                _where = e.Value;
            else
                throw new Exception("Missing Where node");
        }
    }
}
