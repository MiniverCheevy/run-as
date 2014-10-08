﻿using System;
using System.Collections.Generic;
using System.Linq;
using Hosting.Operations.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ra.Helpers;

namespace Hosting.Tests.Operations.Users
{
    [TestClass()]
    public class UserUpdateCommandTests
    {
        [TestMethod()]
        public void Execute_ValidInput_IsOk()
        {
            var request = new UserMessage() { Key = "ValidInput", UserName = "foo@bar.com"};
            ConfigurationStore.Refresh();
            var userCount = ConfigurationStore.Current.Users.Count;
            new UserDeleteCommand(request).Execute();
            new UserAddCommand(request).Execute();
            var response = new UserUpdateCommand(request).Execute();
            ConfigurationStore.Refresh();
            var newUserCount = ConfigurationStore.Current.Users.Count;
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.AreEqual(userCount, ConfigurationStore.Current.Users.Count);
        }

        [TestMethod]
        public void Execute_WrongKey_IsOk()
        {
            var request = new UserMessage() { Key = "WrongKey", UserName = "foo@bar.com"};
            ConfigurationStore.Refresh();
            var userCount = ConfigurationStore.Current.Users.Count;
            new UserDeleteCommand(request).Execute();
            var response = new UserUpdateCommand(request).Execute();
            ConfigurationStore.Refresh();
            var newUserCount = ConfigurationStore.Current.Users.Count;
            Assert.IsTrue(response.Message.StartsWith("Could not find"));
            Assert.AreEqual(false, response.IsOk);
        }
    }
}
