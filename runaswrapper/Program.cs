using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using RunAsWrapper.Core.Helpers;
using Voodoo;

namespace RunAsWrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            if (ConfigurationManager.AppSettings["debug"].To<bool>())
                System.Diagnostics.Debugger.Launch();

            var parser = new CommandLineParser();
            parser.ParseAndExecute(args);          

        }
    }
}
