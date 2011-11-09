using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine
{
    /// <summary>
    /// Generic representation of a record returned by a data source.
    /// </summary>
    public interface IRecord
    {
        String Get(String field);
    }
}
