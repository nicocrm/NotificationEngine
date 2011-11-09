using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine
{
    /// <summary>
    /// Generic representation for a configuration field
    /// </summary>
    public interface ConfigurationField
    {
        String Name { get; set; }

        ConfigurationFieldType Type { get; set; }
    }

    public enum ConfigurationFieldType
    {
        TextField,
        TextArea,
        RichText
    }
}
