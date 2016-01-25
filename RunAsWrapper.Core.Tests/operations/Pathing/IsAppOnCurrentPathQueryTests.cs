using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunAsWrapper.Core.Operations.Pathing;

namespace Hosting.Tests.Operations.Pathing
{
    [TestClass]
    public class IsAppOnCurrentPathQueryTests
    {
        const string pathEnvironmentalVariable = "PATH";
        [TestMethod]
        public void Execute_IsOnPath_ReturnsTrue()
        {
            var path = Environment.GetEnvironmentVariable(pathEnvironmentalVariable);
            var testPath = @"C:\foo";
            var newPath = string.Format("{0};{1};", path, testPath);
            Environment.SetEnvironmentVariable(pathEnvironmentalVariable, newPath);
            var request = new PathRequest { Path = testPath };
            var response = new IsAppOnCurrentPathQuery(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.AreEqual(true, response.Data);          

            Environment.SetEnvironmentVariable(pathEnvironmentalVariable, path);

        }
        [TestMethod]
        public void Execute_IsNotOnPath_ReturnsFalse()
        {
            const string testPath = @"C:\bar";
                        
            var request = new PathRequest { Path = testPath };
            var response = new IsAppOnCurrentPathQuery(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.AreEqual(false, response.Data);
            
        }

    }
}
