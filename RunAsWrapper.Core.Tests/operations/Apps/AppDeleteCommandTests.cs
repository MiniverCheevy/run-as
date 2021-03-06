﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunAsWrapper.Core.Helpers;
using RunAsWrapper.Core.Operations.Apps;

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
            Assert.AreEqual(appCount - 1, newAppCount);
        }
    }
}
