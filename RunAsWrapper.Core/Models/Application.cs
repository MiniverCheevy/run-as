using System.Linq;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;

namespace RunAsWrapper.Core.Models
{
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
        [XmlAttribute]
        public bool UrlEncodeArguments { get; set; }

        public Application()
        {
            Arguments = String.Empty;
        }
    }
}