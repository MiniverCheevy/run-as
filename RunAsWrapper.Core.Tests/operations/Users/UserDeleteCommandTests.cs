using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunAsWrapper.Core.Helpers;
using RunAsWrapper.Core.Operations.Users;

namespace Hosting.Tests.Operations.Users
{
    [TestClass()]
    public class UserDeleteCommandTests
    {
        [TestMethod()]
        public void UserDeleteCommandTest()
        {

            var request = new UserMessage() { Key = "foo",  UserName = "foo@bar.com"};
            new UserDeleteCommand(request).Execute();
            new UserAddCommand(request).Execute();
            var appCount = ConfigurationStore.Current.Users.Count;
            var response = new UserDeleteCommand(request).Execute();
            ConfigurationStore.Refresh();
            var newUserCount = ConfigurationStore.Current.Users.Count;
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.AreEqual(appCount - 1, newUserCount);
        }
    }
}
