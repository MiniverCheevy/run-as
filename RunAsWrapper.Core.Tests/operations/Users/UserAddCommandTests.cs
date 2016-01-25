using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunAsWrapper.Core.Helpers;
using RunAsWrapper.Core.Operations;
using RunAsWrapper.Core.Operations.Users;

namespace Hosting.Tests.Operations.Users
{
    [TestClass()]
    public class UserAddCommandTests
    {
        [TestMethod()]
        public void Execute_MissingKey_IsNotOk()
        {
            ConfigurationStore.Refresh();
            var request = new UserMessage() {UserName = "foo@bar.com"};
            var response = new UserAddCommand(request).Execute();
            Assert.AreEqual(Messages.Required, response.Message);
            Assert.AreEqual(false, response.IsOk);
        }

        [TestMethod()]
        public void Execute_MissingUserName_IsNotOk()
        {
            ConfigurationStore.Refresh();
            var request = new UserMessage() {Key = "MissingUserName"};
            new UserDeleteCommand(request).Execute();
            var response = new UserAddCommand(request).Execute();
            Assert.AreEqual(Messages.Required, response.Message);
            Assert.AreEqual(false, response.IsOk);
        }


        [TestMethod()]
        public void Execute_ValidInput_IsOk()
        {
            var request = new UserMessage() {Key = "ValidInput", UserName = "foo@bar.com"};
            ConfigurationStore.Refresh();
            new UserDeleteCommand(request).Execute();
            var appCount = ConfigurationStore.Current.Users.Count;
            var response = new UserAddCommand(request).Execute();
            ConfigurationStore.Refresh();
            var newUserCount = ConfigurationStore.Current.Users.Count;
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.AreEqual(appCount + 1, newUserCount);
        }
    }
}