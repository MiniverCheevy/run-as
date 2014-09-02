using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ra.Models;
using Voodoo;

namespace ra.Helpers
{
    public static class ConfigurationHelper
    {
        private static RaConfiguration current;
        public static string configurationDirectory;
        public static RaConfiguration Current
        {
            get
            {
                if (current == null)
                {
                    var dir = getDirectory();
                    var path = Path.Combine(dir, "config.ra");
                    if (!File.Exists(path))
                        return current;

                    var xml = IoNic.ReadFile(path);
                    current = Objectifyer.FromXml<RaConfiguration>(xml,
                        new Type[] { typeof(UserAccount), typeof(Application) });
                }
                return current;
            }
        }

        private static string getDirectory()
        {
            if (configurationDirectory == null)
            {
                var dir = System.Environment.CommandLine.ToLower();
                var idx = dir.LastIndexOf(@"\");
                var path = dir.Substring(0, idx).Replace("\"","");
                
                //dir = dir.Replace("ra.vshost.exe", "ra.exe");
                //dir = dir.Substring(1);
                //var idx = dir.ToLower().IndexOf("ra.exe", System.StringComparison.Ordinal);
                //dir = dir.Substring(0, idx - 1);
                configurationDirectory = path;
            }
            return configurationDirectory;
        }
    }
}

