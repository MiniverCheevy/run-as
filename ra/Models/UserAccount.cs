using System;
using System.Xml.Serialization;

namespace ra.Models
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