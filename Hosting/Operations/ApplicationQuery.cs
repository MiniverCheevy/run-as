using Hosting.CodeGeneration;
using Hosting.Messages;
using Voodoo;
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
    [Rest(Verb.Get, Resources.Apps)]
    public class ApplicationQuery : Query<AppQueryRequest, PagedResponse<AppMessage>>
    {
        public ApplicationQuery(AppQueryRequest request)
            : base(request)
        {
        }

        protected override PagedResponse<AppMessage> ProcessRequest()
        {
            response=
                ra.Helpers.ConfigurationHelper.Current.Apps.AsQueryable()
                  .PagedResult<Application, AppMessage>(request, c=>Mapper.Map(c));
                
            return response;
        }
    }
}
