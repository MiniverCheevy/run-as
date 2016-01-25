using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunAsWrapper.Core.CodeGeneration;

namespace Ra.Tests.Hosting.CodeGeneration
{
    [TestClass]
    public class CodeWalkerTests
    {
        [TestMethod]
        public void ctor_UserGet_IsOk()
        {

            var walker = new CodeWalker();
            var keys =walker.Resources.Select(c => c.Name).ToArray();
            Assert.IsTrue(keys.Contains(Resources.Users));
            var user = walker.Resources.Single(c => c.Name == Resources.Users);
            Assert.IsTrue(user.Verbs.Any(c=>c.Name == "Get"));
            var get = user.Verbs.Single(c => c.Name == "Get");

        }
    }
}
