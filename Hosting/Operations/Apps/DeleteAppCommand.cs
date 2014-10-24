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

namespace Hosting.Operations.Apps
{
    [Rest(Verb.Delete, Resources.Apps)]
    public class AppDeleteCommand : Command<AppMessage, Response>
    {
        public AppDeleteCommand(AppMessage request)
            : base(request)
        {
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
