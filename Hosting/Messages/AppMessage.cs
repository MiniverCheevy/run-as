using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosting.Messages
{
    public class AppMessage
    {
        
        public string Key { get; set; }
        
        public string FullPath { get; set; }
        
        public string Arguments { get; set; }
        
        public bool RequiresAdmin { get; set; }
        
        public bool UrlEncodeArguments { get; set; }

        public AppMessage()
        {
            Arguments = String.Empty;
        }
    }
}
