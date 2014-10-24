using RunAsWrapper.Core.CodeGeneration;
using RunAsWrapper.Core.Helpers;
using RunAsWrapper.Core.Models;
using Voodoo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Operations;

namespace RunAsWrapper.Core.Operations.Apps
{
    [Rest(Verb.Put, Resources.Apps)]
    public class AppAddCommand: Command<AppMessage, Response>
    {
        public AppAddCommand(AppMessage request) : base(request)
        {
        }

        protected override void Validate()
        {

            var exists = ConfigurationStore.Current.Apps
                .Any(c => c.Key == request.Key);
            
            if (exists)
                throw new LogicException(string.Format("Key '{0}' already exists", request.Key));

            base.Validate();
        }
        protected override Response ProcessRequest()
        {
            var app = new Application();
            Mapper.Map(request, app);
            ConfigurationStore.Current.Apps.Add(app);
            ConfigurationStore.Save();
            return response;
        }
    }
}
