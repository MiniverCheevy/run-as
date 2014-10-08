using System;
using System.Collections.Generic;
using System.Linq;
using Hosting.Operations.Apps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ra.Helpers;

namespace Hosting.Tests.Operations.Apps
{
    [TestClass()]
    public class AppDeleteCommandTests
    {
        [TestMethod()]
        public void AppDeleteCommandTest()
        {
            
            var request = new AppMessage() { Key = "foo", FullPath = ConfigurationStore.ConfigurationPath };
            new AppDeleteCommand(request).Execute();
            new AppAddCommand(request).Execute();
            var appCount = ConfigurationStore.Current.Apps.Count;
            var response = new AppDeleteCommand(request).Execute();
            ConfigurationStore.Refresh();
            var newAppCount = ConfigurationStore.Current.Apps.Count;
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.AreEqual(appCount-1,ConfigurationStore.Current.Apps.Count);
        }
    }
}
