using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine
{
    public interface IConfigurable
    {
        /// <summary>
        /// Reads in the XML config
        /// </summary>
        /// <param name="configBlob"></param>
        void LoadConfiguration(String configBlob);

    }
}
