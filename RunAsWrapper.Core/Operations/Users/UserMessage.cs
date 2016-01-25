using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RunAsWrapper.Core.Operations.Users
{
    public class UserMessage
    {
        [Required(ErrorMessage = Messages.Required)]
        public string Key { get; set; }

        [Required(ErrorMessage = Messages.Required)]
        public string UserName { get; set; }

    }
}
