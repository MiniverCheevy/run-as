using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ra
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
