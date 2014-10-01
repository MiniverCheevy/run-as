using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Voodoo;
using ra.Messages;
using ra.Models;
using ra.Operations;

namespace ra
{
    public class CommandLineParser
    {
        private string commandLineArguments = string.Empty;
        private List<string> userArguments = new List<string>();

        internal void ParseAndExecute(string[] args)
        {
            var response = new ParseCommandLine(args).Execute();
            if (!response.IsOk)
            {
                Console.WriteLine(response.Message);
                Console.ReadKey();
            }
            var request = buildRequest(response);
            var executeResponse = new ProcessRunner(request).Execute();

            if (!executeResponse.IsOk)
            {
                Console.WriteLine(response.Message);
                Console.ReadKey();
            }
        }

        private RunProcessRequest buildRequest(CommandLineResponse response)
        {
            var request = new RunProcessRequest
                {
                    ProgramPath = response.Application.FullPath,
                    Arguments = response.Application.Arguments,
                    RequiresAdmin = response.Application.RequiresAdmin,
                    UserArgumnets = response.UserArguments
                };
            if (response.User != null)
                request.UserName = response.User.UserName;

            return request;
        }
    }
}