using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using RunAsWrapper.Core.Models;
using Voodoo;

namespace RunAsWrapper.Core.Helpers
{
    public static class ConfigurationStore
    {
        private static RaConfiguration current;
        public static string configurationDirectory;
        private static Type[] extraTypes = new Type[] {typeof (UserAccount), typeof (Application)};

        public static void Save()
        {
                var xml = Objectifyer.ToXml(Current, extraTypes, false);
                IoNic.WriteFile(xml, ConfigurationPath);

        }

        public static void Refresh()
        {
                current = null;

        }

        public static RaConfiguration Current
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                
                    if (current == null)
                    {
                        var path = ConfigurationPath;
                        if (!File.Exists(path))
                            return current;

                        var xml = IoNic.ReadFile(path);
                        current = Objectifyer.FromXml<RaConfiguration>(xml,
                                                                       extraTypes);
                    }
                    return current;
            }
        }

        public static string _configurationPath;

        public static string ConfigurationPath
        {
            get
            {
                if (_configurationPath == null)
                {
                    var dir = getDirectory();
                    var path = Path.Combine(dir, "config.ra");
                    _configurationPath = path;
                }
                return _configurationPath;
            }
        }

        private static string getDirectory()
        {
            if (configurationDirectory == null)
            {
                var path = System.AppDomain.CurrentDomain.BaseDirectory;               
                configurationDirectory = path;
            }
            return configurationDirectory;
        }
    }
}

