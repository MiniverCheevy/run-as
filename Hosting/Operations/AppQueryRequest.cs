using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Messages;

namespace Hosting.Messages
{
    public class AppQueryRequest: PagedRequest
    {
        public override string DefaultSortMember
        {
            get { return "Key"; }
        }
    }
}
