using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine.DataSource
{
    public class HqlRecordWrapper : IRecord
    {
        private object[] _data;
        private List<String> _fieldAliases;

        public HqlRecordWrapper(object[] data, List<String> fields)
        {
            _data = data;
            _fieldAliases = fields;
        }

        public string Get(string field)
        {
            // TODO: error handling
            try
            {
                int index = _fieldAliases.IndexOf(field);
                object v = _data[index];
                if (v == null)
                    return "";
                return v.ToString();
            }
            catch
            {
                throw new Exception("Invalid field " + field);
            }
        }
    }
}
