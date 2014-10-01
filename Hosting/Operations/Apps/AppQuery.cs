using Hosting.CodeGeneration;
using Voodoo;
using ra.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Operations;

namespace Hosting.Operations.Apps
{
    [Rest(Verb.Get, Resources.Apps)]
    public class AppQuery : Query<AppQueryRequest, PagedResponse<AppMessage>>
    {
        public AppQuery(AppQueryRequest request)
            : base(request)
        {
        }

        protected override PagedResponse<AppMessage> ProcessRequest()
        {
            response=
                ra.Helpers.ConfigurationStore.Current.Apps.AsQueryable()
                  .PagedResult<Application, AppMessage>(request, c=>Mapper.Map(c));
                
            return response;
        }
    }
}
