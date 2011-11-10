using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine.DataSource
{
    public class DataSourceFactory
    {
        public static IWorkItemDataSource CreateDataSource(Sage.Entity.Interfaces.IWorkItemDataSource data)
        {
            IWorkItemDataSource ds = null;
            switch (data.DataSourceType)
            {
                case "HQL":
                    ds = new HqlDataSource();
                    break;
                default:
                    throw new Exception("Invalid datasource type " + data.DataSourceType);
            }
            ds.LoadConfiguration(data.DataSourceValue);
            return ds;
        }
    }
}
