using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Voodoo;
using Voodoo.Messages;
using Voodoo.Operations;

namespace RunAsWrapper.Core.Operations.Processes
{
    public class ProcessRunner : Executor<RunProcessRequest, Response>
    {
        private string arguments;
        private string commandLine;
        private string[] executables = new string[] {".exe", ".com", ".bat", ".msc", ".cpl"};
        private ProcessStartInfo startInfo;
        private bool useShellExecute;

        
        public ProcessRunner(RunProcessRequest request) : base(request)
        {
        }

        protected override Response ProcessRequest()
        {
            //http://social.msdn.microsoft.com/Forums/en-US/winforms/thread/28f84724-af3e-4fa1-bd86-b0d1499eaefa#x_FAQAnswer91

            var path = resolveRelativePath(request.ProgramPath);

            startInfo = new ProcessStartInfo();            
            commandLine = path;
            useShellExecute = !executables.Any(c => commandLine.ToLower().EndsWith(c)) || request.RequiresAdmin;
            arguments = request.UserArguments == null
                            ? request.Arguments
                            : string.Format(request.Arguments, request.UserArguments);

            configureProcess();

            var process = new Process() {StartInfo = startInfo};
            process.Start();
            return response;
        }

        private string resolveRelativePath(string path)
        {
            var thisFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var newPath = Path.Combine(thisFolder, path);
            return Path.GetFullPath(newPath);
        }

        private void configureProcess()
        {
            if (request.UserName == null)
                configureProcessForCurrentUser();
            else
                configureProcessWithRunAs();

            startInfo.RedirectStandardOutput = false;
            startInfo.CreateNoWindow = false;

            if (request.RequiresAdmin)
                startInfo.Verb = "runas";
        }

        private void configureProcessWithRunAs()
        {
            commandLine = string.Format("{0} {1}", request.ProgramPath, arguments);
            arguments = string.Format(" /u:{0} /netonly \"{1}\"", request.UserName, commandLine);
            startInfo.FileName = @"c:\windows\system32\runas.exe";
            startInfo.Arguments = arguments;
            startInfo.UseShellExecute = true;
        }

        private void configureProcessForCurrentUser()
        {
            startInfo.FileName = commandLine;

            if (!string.IsNullOrWhiteSpace(arguments))
                startInfo.Arguments = arguments;

            startInfo.UseShellExecute = useShellExecute;
        }
    }
}