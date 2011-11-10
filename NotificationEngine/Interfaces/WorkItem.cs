using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage.Entity.Interfaces;
using NotificationEngine.Action;
using NotificationEngine.Target;
using NotificationEngine.DataSource;

namespace NotificationEngine.Interfaces
{
    /// <summary>
    /// Work item
    /// </summary>
    public class WorkItem
    {
        private Sage.Entity.Interfaces.IWorkItem _entity;

        public List<IWorkItemAction> Actions { get; set; }

        public List<IWorkItemTarget> Targets { get; set; }

        public IWorkItemDataSource DataSource { get; set; }

        public DateTime LastExecuted { get; set; }


        /// <summary>
        /// Replace literals in the source string with the corresponding value
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public String EvaluateLiterals(String source, IWorkItemDataSource dataSource)
        {
            // TODO
            return source;
        }

        public void SaveLastExecuted(DateTime lastExecuted)
        {
            _entity.LastExecuted = lastExecuted;
            _entity.WorkItemSchedule.NextTimeToExecute = lastExecuted.AddMinutes(_entity.WorkItemSchedule.Frequency.GetValueOrDefault());
            _entity.Save();
        }

        /// <summary>
        /// Return true if it is time for this work item to be triggered
        /// </summary>
        /// <returns></returns>
        public bool CheckSchedule()
        {
            if (_entity.WorkItemSchedule.NextTimeToExecute != null &&
                _entity.WorkItemSchedule.NextTimeToExecute.Value < DateTime.UtcNow)
                return true;
            return false;
        }

        /// <summary>
        /// Query the data source and execute the actions
        /// </summary>
        public void Execute()
        {
            DateTime lastExecuted = DateTime.UtcNow;
            List<IRecord> records = new List<IRecord>();
            foreach (var rec in DataSource.Read(this))
            {
                records.Add(rec);
                foreach (IWorkItemAction action in Actions)
                {
                    action.Execute(records, Targets);
                }
                records.Clear();  // todo: grouping
            }
            SaveLastExecuted(lastExecuted);
        }

        public WorkItem(IWorkItem wo)
        {
            _entity = wo;
            Actions = new List<IWorkItemAction>();
            foreach (Sage.Entity.Interfaces.IWorkItemAction act in wo.WorkItemActions)
            {
                Actions.Add(ActionFactory.CreateAction(act));
            }
            Targets = new List<IWorkItemTarget>();
            foreach (Sage.Entity.Interfaces.IWorkItemTarget target in wo.WorkItemTargets)
            {
                Targets.Add(TargetFactory.CreateTarget(target));
            }
            DataSource = DataSourceFactory.CreateDataSource(wo.WorkItemDataSource);
        }

    }
}
