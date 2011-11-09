using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine
{
    public interface IWorkItem
    {
        IWorkItemAction Action { get; }

        List<IWorkItemTarget> Targets { get; }

        IWorkItemDataSource DataSource { get; }


        /// <summary>
        /// Replace literals in the source string with the corresponding value
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        String EvaluateLiterals(String source);
    }
}
