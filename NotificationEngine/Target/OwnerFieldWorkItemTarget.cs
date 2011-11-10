using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Sage.Entity.Interfaces;
using Sage.Platform;

namespace NotificationEngine.Target
{
    /// <summary>
    /// Grabs a seccodeid from the query values and expands it to the list of emails on that team
    /// </summary>
    public class OwnerFieldWorkItemTarget : IWorkItemTarget
    {
        private String _recordField;

        public string Evaluate(IRecord record)
        {
            String ownerId = record.Get(_recordField);
            IOwner parent = EntityFactory.GetById<IOwner>(ownerId);
            if (parent == null)
                throw new Exception("Owner id " + ownerId + " not found");
            StringBuilder buf = new StringBuilder();
            GetAllTeamMemberEmails(parent, buf);
            return buf.ToString();
        }

        public void LoadConfiguration(string configBlob)
        {
            XDocument xml = XDocument.Parse(configBlob);
            _recordField = xml.Root.Element("FieldAlias").Value;
        }


        /// <summary>
        /// Recursively retrieve all users under that team and add their emails
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="buf"></param>
        private void GetAllTeamMemberEmails(IOwner parent, StringBuilder buf)
        {
            foreach (IOwnerJoin join in Sage.SalesLogix.Owner.Rules.GetTeamMembers(parent))
            {
                if (join.Child.Type == OwnerType.User)
                {
                    String email = join.Child.User.UserInfo.Email;
                    if (!String.IsNullOrEmpty(email))
                    {
                        if (buf.Length != 0)
                            buf.Append(", ");
                        buf.Append(email);
                    }
                }
                else
                {
                    GetAllTeamMemberEmails(join.Child, buf);
                }
            }
        }
    }
}
