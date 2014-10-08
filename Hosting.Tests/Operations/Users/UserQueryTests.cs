using System;
using System.Collections.Generic;
using System.Linq;
using Hosting.Operations.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;

namespace Hosting.Tests.Operations.Users
{
    [TestClass]
    public class UserQueryTests
    {
        [TestMethod]
        public void Execute_EmptyRequest_IsOk()
        {
            var request = new UserQueryRequest();
            var response = new UserQuery(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.AreNotEqual(0, response.Data.Count);
        }
    }
}
