using System.Linq;
using System.Collections.Generic;
using System;
using RunAsWrapper.Core.Models;
using Voodoo.Messages;

namespace RunAsWrapper.Core.Operations.Processes
{
    public class CommandLineResponse : Response
    {
        public UserAccount User { get; set; }
        public Application Application { get; set; }
        public string UserArguments { get; set; }
    }
}