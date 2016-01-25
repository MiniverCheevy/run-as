using System.Linq;
using System.Collections.Generic;
using System;

namespace RunAsWrapper.Core.Operations.Processes
{
    public class RunProcessRequest
    {
        public string UserName { get; set; }
        public string ProgramPath { get; set; }
        public string Arguments { get; set; }
        public string UserArguments { get; set; }
        public bool RequiresAdmin { get; set; }
    }
}