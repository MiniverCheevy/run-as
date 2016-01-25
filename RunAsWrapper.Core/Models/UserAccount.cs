using System.Linq;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;

namespace RunAsWrapper.Core.Models
{
    [Serializable]
    public class UserAccount
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public string UserName { get; set; }
    }
}