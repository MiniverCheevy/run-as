using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ra.Operations.Processes;

namespace Ra.Tests.Operations.Processes
{    
    [TestClass,Ignore]
    public class RunProcessTests
    {
        private const string vs = @"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\devenv.exe";
        private const string vsArgs = "-nosplash";
        private const string notepad = @"c:\windows\notepad.exe";
        private const string filePath = @"C:\Dropbox\Lib\";
        private const string turtles = @"https://www.google.com/search?q=turtles";
        private const string explorer = @"C:\Windows\System32\explorer.exe";
        private const string chrome = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
        private const string userName = "someone@gmail.com";

        [TestMethod]
        public void Execute_AppOnly_IsOk()
        {
            var request = new RunProcessRequest() { ProgramPath = notepad };
            var response = new ProcessRunner(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);

        }

        [TestMethod]
        public void Execute_AppAndAdmin_IsOk()
        {
            var request = new RunProcessRequest() {ProgramPath = vs,RequiresAdmin = true};
            var response = new ProcessRunner(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);

        }
        [TestMethod]
        public void Execute_AppAndAdminAndArgs_IsOk()
        {
            var request = new RunProcessRequest() { ProgramPath = vs, RequiresAdmin = true, Arguments = vsArgs};
            var response = new ProcessRunner(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);

        }
        [TestMethod]
        public void Execute_AppAndAdminAndArgsAndUser_IsOk()
        {
            var request = new RunProcessRequest() { ProgramPath = vs, RequiresAdmin = true, Arguments = vsArgs,UserName = userName};
            var response = new ProcessRunner(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);

        }
        [TestMethod,Ignore]
        public void Execute_ExplorerWithFilePath_IsOk()
        {
            var request = new RunProcessRequest() { ProgramPath = explorer, Arguments = filePath};
            var response = new ProcessRunner(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);

        }
        [TestMethod,Ignore]
        public void Execute_ChromeWithSearch_IsOk()
        {
            var request = new RunProcessRequest() { ProgramPath = chrome, Arguments = turtles };
            var response = new ProcessRunner(request).Execute();
            Assert.AreEqual(null, response.Message);
            Assert.AreEqual(true, response.IsOk);

        }
    }
}

