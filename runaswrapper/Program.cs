using System;
using System.Collections.Generic;
using System.Linq;
using RunAsWrapper.Core.Helpers;

namespace RunAsWrapper
{
    class Program
    {
        static void Main(string[] args)
        {            
            var parser = new CommandLineParser();
            parser.ParseAndExecute(args);          

        }
    }
}
