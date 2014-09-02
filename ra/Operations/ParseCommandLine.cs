using System.Collections.Generic;
using System.Linq;
using System.Web;
using ra.Helpers;
using ra.Messages;
using ra.Models;
using Voodoo.Infrastructure;
using Voodoo.Operations;

namespace ra.Operations
{
    public class ParseCommandLine : Executor<string[], CommandLineResponse>
    {
        private RaConfiguration config;
        private int applicationKeyIndex;

        public ParseCommandLine(string[] request)
            : base(request)
        {
        }

        protected override void Validate()
        {
            if (request.Length == 0)
                throw new LogicException(Messages.Messages.AtLeastOneArgumentIsRequired);
            base.Validate();
        }

        protected override CommandLineResponse ProcessRequest()
        {
            config = ConfigurationHelper.Current;
            response.User = getUserIfPresentInArgs();
            response.Application = getApplicationFromArgs();
            response.UserArguments = getAdditionalArguments();
            return response;
        }

        private UserAccount getUserIfPresentInArgs()
        {
            var firstArgument = request.First().ToLower();
            var user = config.Users.FirstOrDefault(c => c.Key.ToLower() == firstArgument);
            return user;
        }

        private Application getApplicationFromArgs()
        {
            applicationKeyIndex = 0 + (response.User == null ? 0 : 1);
            var applicationKey = request[applicationKeyIndex].ToLower();
            var application = config.Apps.FirstOrDefault(c => c.Key == applicationKey);
            if (application == null)
                throw new LogicException(Messages.Messages.ApplicationNotFound);
            return application;
        }

        private string getAdditionalArguments()
        {
            var userArguments = new List<string>();
            if (request.Count() <= applicationKeyIndex)
                return null;

            for (var i = applicationKeyIndex + 1; i < request.Count(); i++)
            {
                if (!string.IsNullOrWhiteSpace(request[i]))
                {
                    var arg = request[i];
                    if (response.Application.UrlEncodeArguments)
                        arg = HttpUtility.UrlEncode((string) arg);
                    userArguments.Add(arg);
                }
            }
            return userArguments.Any() ? string.Join(" ", userArguments) :null;
        }
    }
}