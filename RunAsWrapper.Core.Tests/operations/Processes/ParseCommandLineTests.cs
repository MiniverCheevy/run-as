using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RunAsWrapper.Core.Helpers;
using RunAsWrapper.Core.Operations.Processes;

namespace Hosting.Tests.Operations.Processes
{
    [TestClass]
    public class ParseCommandLineTests
    {
        public ParseCommandLineTests()
        {
            ConfigurationStore.configurationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);            
        }

        [TestMethod]
        public void Execute_NoArguments_IsNotOk()
        {
            var request = new string[] { };
            var response = new ParseCommandLine(request).Execute();
            Assert.AreEqual(Messages.AtLeastOneArgumentIsRequired, response.Message);
            Assert.AreEqual(false, response.IsOk);
        }

        [TestMethod]
        public void Execute_NoUserApplicationNoArgs_FindsApplication()
        {
            const string app = "google";
            var request = new string[] { app };
            var response = new ParseCommandLine(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.IsNotNull(response.Application);
            Assert.AreEqual(app, response.Application.Key);
            Assert.AreEqual(null, response.User);
            Assert.AreEqual(null, response.UserArguments);
        }

        [TestMethod]
        public void Execute_UserApplicationNoArgs_FindsBoth()
        {
            const string user = "work";
            const string app = "google";
            var request = new string[] { user, app };
            var response = new ParseCommandLine(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.IsNotNull(response.Application);
            Assert.IsNotNull(response.User);
            Assert.AreEqual(app, response.Application.Key);
            Assert.AreEqual(user, response.User.Key);
            Assert.AreEqual(null, response.UserArguments);
        }

        [TestMethod]
        public void Execute_UserApplicationArgs_FindsBothAndArgs()
        {
            const string user = "work";
            const string app = "google";
            const string args = "Red Fish Blue Fish";
            var request = new string[] { user, app, args};
            var response = new ParseCommandLine(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);
            Assert.IsNotNull(response.Application);
            Assert.IsNotNull(response.User);
            Assert.AreEqual(app, response.Application.Key);
            Assert.AreEqual(user, response.User.Key);
            Assert.AreEqual(HttpUtility.UrlEncode(args), response.UserArguments);
        }

    }
}
