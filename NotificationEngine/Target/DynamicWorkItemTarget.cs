using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine.Target
{
    /// <summary>
    /// Evaluate the target based on the current record retrieved from the datasource
    /// </summary>
    public class DynamicWorkItemTarget : IWorkItemTarget
    {
        private String _recordField;

        public string Evaluate(IRecord record)
        {
            return record.Get(_recordField);
        }

        public void LoadConfiguration(string configBlob)
        {
            throw new NotImplementedException();
        }
    }
}
