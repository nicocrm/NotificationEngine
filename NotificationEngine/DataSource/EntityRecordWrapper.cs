using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NotificationEngine.DataSource
{
    /// <summary>
    /// Wrapper for retrieving data from an entity
    /// </summary>
    public class EntityRecordWrapper : IRecord
    {
        private object _data;

        public EntityRecordWrapper(object o)
        {
            _data = o;
        }

        public string Get(string field)
        {
            PropertyInfo prop = _data.GetType().GetProperty(field);
            if (prop != null)
                throw new Exception("Invalid field " + field);
            object v = prop.GetValue(_data, null);
            if (v == null)
                return "";
            return v.ToString();
        }
    }
}
