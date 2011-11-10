using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Sage.Platform.Orm;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace NotificationEngine.DataSource
{
    /// <summary>
    /// Runs HQL query.
    /// Limitation right now:
    ///  * you have to have at least 2 fields in qry
    ///  * you have to assign an alias to each
    ///  
    /// Example query:
    /// <HQLDataSource>   <HQLQuery>    select defect.Subject as Subject, defect.AssignedTo.Id as EmailRecipient   from Defect defect    where defect.AssignedDate > :LastChecked   </HQLQuery>  </HQLDataSource> 
    /// </summary>
    public class HqlDataSource : IWorkItemDataSource
    {
        private String _query;
        private List<string> _fields;

        public IEnumerable<IRecord> Read(Interfaces.WorkItem wo)
        {
            List<String> fields = GetFields();
            using (var sess = new SessionScopeWrapper())
            {
                var qry = sess.CreateQuery(wo.EvaluateLiterals(_query, this));
                foreach (object o in qry.List())
                {
                    yield return new HqlRecordWrapper(o is object[] ? (object[])o : new object[] { o },
                        fields);
                }
            }
        }

        public List<string> GetFields()
        {
            if (_fields != null)
                return _fields;
            _fields = new List<string>();
            using (var sess = new SessionScopeWrapper())
            {
                var qry = sess.CreateQuery(Regex.Replace(_query, " where .*", " where 1=0", RegexOptions.IgnoreCase));
                _fields = qry.ReturnAliases.ToList();
            }
            return _fields;
        }

        public string EscapeLiteral(string literalValue)
        {
            return literalValue == null ? "" : literalValue.Replace("'", "''");
        }

        public void LoadConfiguration(string configBlob)
        {
            XDocument xml = XDocument.Parse(configBlob);
            _query = xml.Root.Element("HQLQuery").Value.Trim(); // TODO: error handling
        }
    }
}
