using RunAsWrapper.Core.CodeGeneration;
using RunAsWrapper.Core.Helpers;
using Voodoo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Operations;

namespace RunAsWrapper.Core.Operations.Apps
{
    [Rest(Verb.Post, Resources.Apps)]
    public class AppUpdateCommand : Command<AppMessage, Response>
    {
        public AppUpdateCommand(AppMessage request)
            : base(request)
        {
        }

        protected override void Validate()
        {
            base.Validate();

            var exists = ConfigurationStore.Current.Apps
                .Any(c => c.Key == request.Key);

            if (!exists)
                throw new LogicException(string.Format("Could not find '{0}'", request.Key));            
        }
        protected override Response ProcessRequest()
        {
            var app = ConfigurationStore.Current.Apps
                .First(c => c.Key == request.Key);
            Mapper.Map(request, app);            
            ConfigurationStore.Save();
            return response;
        }
    }
}
