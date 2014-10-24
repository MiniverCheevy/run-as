using RunAsWrapper.Core.CodeGeneration;
using RunAsWrapper.Core.Helpers;
using Voodoo;
using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Operations;

namespace RunAsWrapper.Core.Operations.Users
{
    [Rest(Verb.Get, Resources.Users)]
    public class UserQuery : Query<UserQueryRequest, PagedResponse<UserMessage>>
    {
        public UserQuery(UserQueryRequest request)
            : base(request)
        {
        }

        protected override PagedResponse<UserMessage> ProcessRequest()
        {
            response= ConfigurationStore.Current.Users
                  .AsQueryable()
                  .PagedResult(request, c=>Mapper.Map(c));                  
                
            return response;
        }
    }
}
