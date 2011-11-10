using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine.Target
{
    public class EmailWorkItemTarget : IWorkItemTarget
    {
        private String _email;

        public string Evaluate(IRecord record)
        {
            return _email;
        }

        public void LoadConfiguration(string configBlob)
        {
            _email = configBlob;
        }
    }
}
