using Hosting.CodeGeneration;
using ra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Messages;
using Voodoo.Operations;

namespace Hosting.Operations
{
    [Rest(Verb.Get, Resources.Users)]
    public class UserAccountQuery:Query<EmptyRequest,ListResponse<UserAccount>>
    {
        public UserAccountQuery(EmptyRequest request) : base(request)
        {
        }

        protected override ListResponse<UserAccount> ProcessRequest()
        {
            response.Data = ra.Helpers.ConfigurationHelper.Current.Users;
            return response;
        }
    }
}
