using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                            writeInitialFile();
                            

                        var xml = IoNic.ReadFile(path);
                        current = Objectifyer.FromXml<RaConfiguration>(xml,
                                                                       extraTypes);
                    }
                    return current;
            }
        }

        private static void writeInitialFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "RunAsWrapper.Core.config.ra";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                IoNic.WriteFile(result,ConfigurationPath);
                
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

        private static string _configAppPath;
        public static string ConfigAppPath
        {
            get
            {
                if (_configAppPath == null)
                {
                    var dir = getDirectory();
                    var path = Path.Combine(dir, "config.exe");
                    _configAppPath = path;
                }
                return _configAppPath;
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

