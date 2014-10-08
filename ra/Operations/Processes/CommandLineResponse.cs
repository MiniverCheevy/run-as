using System.Linq;
using System.Collections.Generic;
using System;
using ra.Models;
using Voodoo.Messages;

namespace ra.Operations.Processes
{
    public class CommandLineResponse : Response
    {
        public UserAccount User { get; set; }
        public Application Application { get; set; }
        public string UserArguments { get; set; }
    }
}