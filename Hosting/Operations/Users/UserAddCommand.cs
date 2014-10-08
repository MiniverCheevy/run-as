using Hosting.CodeGeneration;
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
    [Rest(Verb.Put, Resources.Users)]
    public class UserAddCommand : Command<UserMessage, Response>
    {
        public UserAddCommand(UserMessage request)
            : base(request)
        {
        }

        protected override void Validate()
        {

            var exists = ConfigurationStore.Current.Users
                .Any(c => c.Key == request.Key);

            if (exists)
                throw new LogicException(string.Format("Key '{0}' already exists", request.Key));

            base.Validate();
        }
        protected override Response ProcessRequest()
        {
            var user = new UserAccount();
            Mapper.Map(request, user);
            ConfigurationStore.Current.Users.Add(user);
            ConfigurationStore.Save();
            return response;
        }
    }
}
