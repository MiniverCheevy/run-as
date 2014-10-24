using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RunAsWrapper.Core.CodeGeneration;
using Voodoo.Messages;
using Voodoo.Operations;

namespace RunAsWrapper.Core.Operations.Processes
{
    [Rest(Verb.Delete, Resources.Session)]
    public class ShutdownCommand:Command<EmptyRequest,Response>
    {
        public ShutdownCommand(EmptyRequest request) : base(request)
        {
        }

        protected override Response ProcessRequest()
        {
            Environment.Exit(0);
            return response;
        }
    }
}
