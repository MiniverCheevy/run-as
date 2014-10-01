using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;

namespace Hosting.Operations.Apps
{
    public class AppQueryRequest: PagedRequest
    {
        public override string DefaultSortMember
        {
            get { return "Key"; }
        }
    }
}
