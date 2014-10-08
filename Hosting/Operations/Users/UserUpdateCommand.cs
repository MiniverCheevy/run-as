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
    [Rest(Verb.Post, Resources.Users)]
    public class UserUpdateCommand : Command<UserMessage, Response>
    {
        public UserUpdateCommand(UserMessage request)
            : base(request)
        {
        }

        protected override void Validate()
        {

            var exists = ConfigurationStore.Current.Users
                .Any(c => c.Key == request.Key);

            if (!exists)
                throw new LogicException(string.Format("Could not find '{0}'", request.Key));

            base.Validate();
        }
        protected override Response ProcessRequest()
        {
            var user = ConfigurationStore.Current.Users
                .First(c => c.Key == request.Key);
            Mapper.Map(request, user);
            ConfigurationStore.Save();
            return response;
        }
    }
}
