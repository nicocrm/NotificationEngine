using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotificationEngine.Action
{
    public class ActionFactory
    {
        public static IWorkItemAction CreateAction(Sage.Entity.Interfaces.IWorkItemAction data)
        {
            IWorkItemAction action = null;
            switch (data.ActionType)
            {
                case "Email Notification":
                    action = new EmailAction();
                    break;
                default:
                    throw new Exception("Invalid action type " + data.ActionType);
            }
            action.LoadConfiguration(data.ActionValue);
            return action;
        }
    }
}
