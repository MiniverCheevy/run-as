using System;
using System.Collections.Generic;
using System.Linq;
using Hosting.Operations.Pathing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ra.Tests.Hosting.Operations.Pathing
{
    [TestClass]
    public class AppendPathToEnvironmentPathCommandTests
    {
        const string pathEnvironmentalVariable = "PATH";
        [TestMethod]
        public void Execute_PathExists_PathIsAdded()
        {
            var path = Environment.GetEnvironmentVariable(pathEnvironmentalVariable);
            var testPath = @"C:\foo";

            var request = new PathRequest {Path = testPath};
            var response = new AppendPathToEnvironmentPathCommand(request).Execute();
            Assert.AreEqual(null,response.Message);
            Assert.AreEqual(true,response.IsOk);

            var currentPath = Environment.GetEnvironmentVariable(pathEnvironmentalVariable);
            Assert.AreEqual(true, currentPath.Contains(testPath));

            Environment.SetEnvironmentVariable(pathEnvironmentalVariable, path);

        }

    }
}
