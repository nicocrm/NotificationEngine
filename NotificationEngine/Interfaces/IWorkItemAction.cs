using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine
{
    /// <summary>
    /// Responsible for gathering the data and putting it into a correct format for the delivery system
    /// </summary>
    public interface IWorkItemAction : IConfigurable
    {
        IDeliverySystem DeliverySystem { get; }

        void Execute(List<IRecord> records, List<IWorkItemTarget> targets);
    }
}
