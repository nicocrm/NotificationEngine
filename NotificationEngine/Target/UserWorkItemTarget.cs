using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sage.Platform;
using Sage.Entity.Interfaces;

namespace NotificationEngine.Target
{
    public class UserWorkItemTarget : IWorkItemTarget
    {
        private String _userId;

        public string Evaluate(IRecord record)
        {
            IUserInfo user = EntityFactory.GetById<IUserInfo>(_userId);
            if(user == null)
                throw new Exception("User id " + _userId + " not found");
            return user.Email;
        }

        public void LoadConfiguration(string configBlob)
        {
            throw new NotImplementedException();
        }
    }
}
