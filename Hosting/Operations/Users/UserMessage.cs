using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hosting.Operations.Users
{
    public class UserMessage
    {
        [Required(ErrorMessage = Messages.Required)]
        public string Key { get; set; }

        [Required(ErrorMessage = Messages.Required)]
        public string UserName { get; set; }

    }
}
