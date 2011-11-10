using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine.Target
{
    /// <summary>
    /// Responsible for instantiating correct target 
    /// </summary>
    public class TargetFactory
    {
        public static IWorkItemTarget CreateTarget(Sage.Entity.Interfaces.IWorkItemTarget data)
        {
            IWorkItemTarget target = null;
            // TODO: this should be dynamic
            switch (data.TargetType)
            {
                case "QueryField":
                    target = new DynamicWorkItemTarget();
                    break;
                case "OwnerField":
                    target = new OwnerFieldWorkItemTarget();
                    break;
                default:
                    throw new Exception("Invalid target type " + data.TargetType);
            }
            target.LoadConfiguration(data.TargetValue);
            return target;
        }
    }
}
