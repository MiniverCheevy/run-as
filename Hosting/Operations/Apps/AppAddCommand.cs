using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Messages;
using Voodoo.Operations;

namespace Hosting.Operations.Apps
{
    public class AppAddCommand: Command<AppMessage, Response>
    {
        public AppAddCommand(AppMessage request) : base(request)
        {
        }

        protected override Response ProcessRequest()
        {
            throw new NotImplementedException();
        }
    }
}
