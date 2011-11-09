using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine
{
    public interface IWorkItemTarget : IConfigurable
    {
        String Evaluate(IRecord record);
    }
}
