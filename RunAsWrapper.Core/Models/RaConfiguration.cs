using System;
using System.Collections.Generic;
using System.Linq;

namespace RunAsWrapper.Core.Models
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
    }
}
