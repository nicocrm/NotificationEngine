using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NotificationEngine.Interfaces;

namespace NotificationEngine
{
    public interface IWorkItemDataSource : IConfigurable
    {
        /// <summary>
        /// Retrieve data
        /// </summary>
        /// <returns></returns>
        IEnumerable<IRecord> Read(WorkItem wo);

        List<String> GetFields();

        /// <summary>
        /// Escape quotes, etc in the given literal value.
        /// </summary>
        /// <param name="literalValue"></param>
        /// <returns></returns>
        String EscapeLiteral(String literalValue);
    }
}
