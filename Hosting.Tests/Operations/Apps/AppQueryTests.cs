using System;
using System.Collections.Generic;
using System.Linq;
using Hosting.Operations.Apps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hosting.Tests.Operations.Apps
{
    [TestClass]
    public class AppQueryTests
    {
        [TestMethod]
        public void Execute_EmptyRequest_IsOk()
        {
            var request = new AppQueryRequest();
            var response = new AppQuery(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.AreNotEqual(0, response.Data.Count);
        }
    }
}
