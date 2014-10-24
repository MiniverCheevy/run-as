using System;
using System.Collections.Generic;
using System.Linq;
using ra.Models;

namespace Hosting.Operations.Users
{
    public class Mapper
    {
        public static UserMessage Map(UserAccount source)
        {
            var response = new UserMessage()
            {
                Key = source.Key,
                UserName = source.UserName
            };
            return response;
        }

        public static void Map(UserMessage source, UserAccount target)
        {
            target.Key = source.Key;
            target.UserName = source.UserName;
        }
    }
}
