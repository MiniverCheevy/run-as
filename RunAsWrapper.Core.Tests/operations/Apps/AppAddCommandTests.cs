using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunAsWrapper.Core.Helpers;
using RunAsWrapper.Core.Operations;
using RunAsWrapper.Core.Operations.Apps;

namespace Hosting.Tests.Operations.Apps
{
    [TestClass()]
    public class AppAddCommandTests
    {
        [TestMethod()]
        public void Execute_MissingKey_IsNotOk()
        {
            ConfigurationStore.Refresh();
            var request = new AppMessage() {FullPath = ConfigurationStore.ConfigurationPath};
            var response = new AppAddCommand(request).Execute();
            Assert.AreEqual(Messages.Required, response.Message);
            Assert.AreEqual(false, response.IsOk);
        }

        [TestMethod()]
        public void Execute_MissingPath_IsNotOk()
        {
            ConfigurationStore.Refresh();
            var request = new AppMessage() { Key = "MissingPath" };
            new AppDeleteCommand(request).Execute();
            var response = new AppAddCommand(request).Execute();
            Assert.AreEqual(Messages.Required, response.Message);
            Assert.AreEqual(false, response.IsOk);
        }

        [TestMethod()]
        public void Execute_FileDoesNotExist_IsNotOk()
        {
            ConfigurationStore.Refresh();
            var request = new AppMessage() { Key = "FileDoesNotExist", FullPath = "x:\foo.bar.txt.cs" };
            new AppDeleteCommand(request).Execute();
            ConfigurationStore.Refresh();
            var response = new AppAddCommand(request).Execute();
            Assert.AreEqual(Messages.FileNotFound, response.Message);
            Assert.AreEqual(false, response.IsOk);
        }

        [TestMethod()]
        public void Execute_ValidInput_IsOk()
        {

            var request = new AppMessage() { Key = "ValidInput", FullPath = ConfigurationStore.ConfigurationPath };
            ConfigurationStore.Refresh();            
            new AppDeleteCommand(request).Execute();
            var appCount = ConfigurationStore.Current.Apps.Count;
            var response = new AppAddCommand(request).Execute();
            ConfigurationStore.Refresh();
            var newAppCount = ConfigurationStore.Current.Apps.Count;
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.AreEqual(appCount + 1, newAppCount);
        }
    }
}