using Hosting.CodeGeneration;
using Voodoo;
using Voodoo.Infrastructure;
using ra.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Messages;
using Voodoo.Operations;
using ra.Models;

namespace Hosting.Operations.Users
{
    [Rest(Verb.Delete, Resources.Users)]
    public class UserDeleteCommand : Command<UserMessage, Response>
    {
        public UserDeleteCommand(UserMessage request)
            : base(request)
        {
        }


        protected override Response ProcessRequest()
        {
            var users = ConfigurationStore.Current.Users.Where(c => c.Key == request.Key).ToArray();
            users.ForEach(c => ConfigurationStore.Current.Users.Remove(c));
            ConfigurationStore.Save();
            return response;
        }
    }
}
