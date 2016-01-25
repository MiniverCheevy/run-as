using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Operations;

namespace RunAsWrapper.Core.Operations.Pathing
{
    public class PathRequest
    {
        public string Path { get; set; }
    }

    public class AppendPathToEnvironmentPathCommand: Command<PathRequest,Response>
    {
        public AppendPathToEnvironmentPathCommand(PathRequest request) : base(request)
        {
        }

        protected override Response ProcessRequest()
        {
            const string pathEnvironmentalVariable = "PATH";
            var path = Environment.GetEnvironmentVariable(pathEnvironmentalVariable);
            if (path != null && ! path.ToLower().Contains(request.Path.ToLower()))
            {
                path = string.Format("{0};{1};", path, request.Path);
                Environment.SetEnvironmentVariable(pathEnvironmentalVariable, path);
            }
            return response;
        }
    }
}
