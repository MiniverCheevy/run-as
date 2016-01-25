using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo;
using Voodoo.Messages;
using Voodoo.Operations;

namespace RunAsWrapper.Core.Operations.Pathing
{
    public class IsAppOnCurrentPathQuery:Query<PathRequest, Response<bool>>
    {
        public IsAppOnCurrentPathQuery(PathRequest request)
            : base(request)
        {
        }

        protected override Response<bool> ProcessRequest()
        {
            const string pathEnvironmentalVariable = "PATH";
            var path = Environment.GetEnvironmentVariable(pathEnvironmentalVariable);
            response.Data = path.To<string>().ToLower().Contains(request.Path.ToLower());
            return response;
        }
    }
}
