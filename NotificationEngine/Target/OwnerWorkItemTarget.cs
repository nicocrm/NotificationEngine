using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage.Entity.Interfaces;
using Sage.Platform;

namespace NotificationEngine.Target
{
    public class OwnerWorkItemTarget : IWorkItemTarget
    {
        private String _ownerId;

        public string Evaluate(IRecord record)
        {
            IOwner parent = EntityFactory.GetById<IOwner>(_ownerId);
            if(parent == null)
                throw new Exception("Owner id " + _ownerId + " not found");
            StringBuilder buf = new StringBuilder();
            GetAllTeamMemberEmails(parent, buf);
            return buf.ToString();
        }        

        public void LoadConfiguration(string configBlob)
        {
            throw new NotImplementedException();
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
