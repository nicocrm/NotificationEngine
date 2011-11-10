using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine
{
    /// <summary>
    /// Responsible for the actual delivery (eg sending email)
    /// </summary>
    public interface IDeliverySystem : IConfigurable
    {
    }
}
