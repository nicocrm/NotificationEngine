using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage.Scheduling;
using NotificationEngine.Interfaces;
using NotificationEngine.DataSource;
using NotificationEngine.Target;
using Sage.Entity.Interfaces;
using Sage.Platform;

namespace NotificationEngine
{

    public class NotificationJob : JobBase
    {
        private IList<WorkItem> _workItems = null;

        protected override void OnExecute()
        {
            if (_workItems == null)
                LoadWorkItems();

            foreach (WorkItem item in _workItems)
            {
                if (item.CheckSchedule())
                {
                    item.Execute();                    
                }
            }
        }

        private void LoadWorkItems()
        {
            _workItems = new List<WorkItem>();
            foreach (IWorkItem wo in EntityFactory.GetRepository<IWorkItem>().FindAll())
            {
                _workItems.Add(new WorkItem(wo));
            }
            //WorkItem testWo = new WorkItem();
            //testWo.Action = new EmailAction();
            //testWo.Action.LoadConfiguration("<Config><Body>Body</Body><Subject>Subject</Subject></Config>");
            //testWo.DataSource = new EntityDataSource();
            //testWo.DataSource.LoadConfiguration("<Config><Entity>Account</Entity><Where>AccountName like 'a%'</Where></Config>");
            //testWo.Targets = new List<IWorkItemTarget>();
            //EmailWorkItemTarget tgt = new EmailWorkItemTarget();
            //tgt.LoadConfiguration("ngaller@sssworld.com");
            //testWo.Targets.Add(tgt);            
        }
    }
}
