using ra.Models;
using Voodoo.Messages;

namespace ra.Messages
{
    public class CommandLineResponse : Response
    {
        public UserAccount User { get; set; }
        public Application Application { get; set; }
        public string UserArguments { get; set; }
    }
}