using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunAsWrapper.Core.Helpers;
using RunAsWrapper.Core.Operations.Apps;

namespace Hosting.Tests.Operations.Apps
{
    [TestClass()]
    public class AppUpdateCommandTests
    {
        [TestMethod()]
        public void Execute_ValidInput_IsOk()
        {
            var request = new AppMessage() { Key = "ValidInput", FullPath = ConfigurationStore.ConfigurationPath };
            ConfigurationStore.Refresh();
            var appCount = ConfigurationStore.Current.Apps.Count;
            new AppDeleteCommand(request).Execute();
            new AppAddCommand(request).Execute();   
            var response = new AppUpdateCommand(request).Execute();
            ConfigurationStore.Refresh();
            var newAppCount = ConfigurationStore.Current.Apps.Count;
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.AreEqual(appCount, newAppCount);
        }

        [TestMethod]
        public void Execute_WrongKey_IsOk()
        {
            var request = new AppMessage() { Key = "WrongKey", FullPath = ConfigurationStore.ConfigurationPath };
            ConfigurationStore.Refresh();
            new AppDeleteCommand(request).Execute();
            var response = new AppUpdateCommand(request).Execute();
            ConfigurationStore.Refresh();
            Assert.IsTrue(response.Message.StartsWith("Could not find"));
            Assert.AreEqual(false, response.IsOk);            
        }
    }
}
