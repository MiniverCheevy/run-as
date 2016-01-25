using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;

namespace RunAsWrapper.Core.Operations.Apps
{
    public class AppQueryRequest: PagedRequest
    {
        public string Key { get; set; }
        public string SearchText { get; set; }

        public override string DefaultSortMember
        {
            get { return "Key"; }
        }
    }
}
