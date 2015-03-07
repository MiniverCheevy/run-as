using RunAsWrapper.Core.CodeGeneration;
using RunAsWrapper.Core.Helpers;
using RunAsWrapper.Core.Models;
using Voodoo;
using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations;

namespace RunAsWrapper.Core.Operations.Apps
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
            var query =
                ConfigurationStore.Current.Apps.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Key))
                query = query.Where(c => c.Key == request.Key);

            response = query.PagedResult<Application, AppMessage>(request, c=>Mapper.Map(c));
                
            return response;
        }
    }
}
