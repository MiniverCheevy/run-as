using System;
using System.Collections.Generic;
using System.Linq;
using RunAsWrapper.Core.Models;

namespace RunAsWrapper.Core.Operations.Apps
{
    public class Mapper
    {
        public static AppMessage Map(Application source)
        {
            var response = new AppMessage() {
                Key = source.Key,
                Arguments = source.Arguments,
                FullPath = source.FullPath,
                RequiresAdmin = source.RequiresAdmin,
                UrlEncodeArguments = source.UrlEncodeArguments
            };
            return response;
        }

        public static void Map(AppMessage source, Application target)
        {
            target.Key = source.Key;
            target.Arguments = source.Arguments;
            target.FullPath = source.FullPath;
            target.RequiresAdmin = source.RequiresAdmin;
            target.UrlEncodeArguments = source.UrlEncodeArguments;
        }
    }
}
