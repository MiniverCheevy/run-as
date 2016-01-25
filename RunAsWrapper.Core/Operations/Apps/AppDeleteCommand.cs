using RunAsWrapper.Core.CodeGeneration;
using RunAsWrapper.Core.Helpers;
using Voodoo;
using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations;

namespace RunAsWrapper.Core.Operations.Apps
{
    [Rest(Verb.Delete, Resources.Apps)]
    public class AppDeleteCommand : Command<AppMessage, Response>
    {
        public AppDeleteCommand(AppMessage request)
            : base(request)
        {
        }

        protected override void Validate()
        {
            //Do nothing
        }
        protected override Response ProcessRequest()
        {
            var apps = ConfigurationStore.Current.Apps.Where(c => c.Key == request.Key).ToArray();
            apps.ForEach(c => ConfigurationStore.Current.Apps.Remove(c));            
            ConfigurationStore.Save();
            return response;
        }
    }
}
