using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ra
{
    [Serializable]
    public class RaConfiguration
    {
        public List<UserAccount> Users { get; set; }
        public List<Application> Apps { get; set; }
        public RaConfiguration()
        {
            Users = new List<UserAccount>();
            Apps = new List<Application>();
        }

        private static RaConfiguration _current;
        public static RaConfiguration Current
        { 
            get {
                if (_current == null)
                { 
                    //var dir = System.Environment.CurrentDirectory;
                    var dir = System.Environment.CommandLine.ToLower ();
                    dir = dir.Replace("ra.vshost.exe", "ra.exe");
                    dir = dir.Substring(1);
                    var idx = dir.ToLower().IndexOf("ra.exe");
                    dir = dir.Substring(0, idx - 1);
                    var path = Path.Combine(dir, "config.ra");
                    if (File.Exists(path))
                    {

                        var xml = IoNic.FromFile(path);
                        _current = Objectifyer.FromXml<RaConfiguration>(xml,
                            new Type[] { typeof(UserAccount), typeof(Application) });
                    }
                    //else
                    //{
                    //    _current = new RaConfiguration();
                    //    _current.Apps.Add(new Application { FullPath = "a", Key = "b", RequiresAdmin = false });
                    //    _current.Users.Add(new UserAccount { Key = "a", UserName = "b" });
                    //    var xml = Objectifyer.ToXml(_current);
                    //    IoNic.ToFile(xml, path);
                    //}                    
                }
                return _current;
            }
        }
    }
    [Serializable]
    public class UserAccount
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public string UserName { get; set; }
    }
    [Serializable]
    public class Application
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public string FullPath { get; set; }
        [XmlAttribute]
        public string Arguments { get; set; }
        [XmlAttribute]
        public bool RequiresAdmin { get; set; }

        public Application()
        {
            Arguments = string.Empty;
        }
    }
    
}
