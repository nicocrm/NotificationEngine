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
                    // TODO - this needs to eb dynamic
                    action.DeliverySystem = new SmtpDeliverySystem(new SmtpSettings
                    {
                        EnableSsl = true,
                        SmtpPort = 587,
                        SmtpServer = "smtp.gmail.com",
                        SmtpUser = "SalesLogixDTS@gmail.com",
                        SmtpUserPassword = "L1ttl3$uz!",
                        EmailAddress = "SalesLogixDTS@gmail.com",
                        DisplayName = "Saleslogix DTS",
                        IsBodyHtml = false
                    }
                    );
                    break;
                default:
                    throw new Exception("Invalid action type " + data.ActionType);
            }
            action.LoadConfiguration(data.ActionValue);
            return action;
        }
    }
}
