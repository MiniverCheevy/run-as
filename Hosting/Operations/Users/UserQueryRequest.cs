using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;

namespace RunAsWrapper.Core.Operations.Users
{
    public class UserQueryRequest : PagedRequest
    {
        public override string DefaultSortMember
        {
            get { return "Key"; }
        }
    }
}
