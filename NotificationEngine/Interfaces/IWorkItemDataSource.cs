using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine
{
    public interface IWorkItemDataSource : IConfigurable
    {
        /// <summary>
        /// Retrieve data
        /// </summary>
        /// <returns></returns>
        IEnumerable<IRecord> Read();

        List<String> GetFields();

        /// <summary>
        /// Escape quotes, etc in the given literal value.
        /// </summary>
        /// <param name="literalValue"></param>
        /// <returns></returns>
        String EscapeLiteral(String literalValue);
    }
}
