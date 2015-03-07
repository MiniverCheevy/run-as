using RunAsWrapper.Core.CodeGeneration;
using RunAsWrapper.Core.Helpers;
using Voodoo;
using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations;

namespace RunAsWrapper.Core.Operations.Users
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
