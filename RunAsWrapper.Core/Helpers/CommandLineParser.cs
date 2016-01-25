using System.Linq;
using System.Collections.Generic;
using System;
using RunAsWrapper.Core.Operations.Processes;

namespace RunAsWrapper.Core.Helpers
{
    public class CommandLineParser
    {
        private string commandLineArguments = string.Empty;
        private List<string> userArguments = new List<string>();
        private CommandLineResponse response;

        public void ParseAndExecute(string[] args)
        {
            response = new ParseCommandLine(args).Execute();
            if (!response.IsOk)
            {
                Console.WriteLine(response.Message);
                Console.ReadKey();
                return;
            }
            var request = buildRequest(response);
            var executeResponse = new ProcessRunner(request).Execute();

            if (!executeResponse.IsOk)
            {
                Console.WriteLine(response.Message);
                Console.ReadKey();
                return;
            }
        }

        private RunProcessRequest buildRequest(CommandLineResponse response)
        {
            var request = new RunProcessRequest
                {
                    ProgramPath = response.Application.FullPath,
                    Arguments = response.Application.Arguments,
                    RequiresAdmin = response.Application.RequiresAdmin,
                    UserArguments = response.UserArguments
                };
            if (response.User != null)
                request.UserName = response.User.UserName;

            return request;
        }
    }
}