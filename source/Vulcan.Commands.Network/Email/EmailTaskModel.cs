#region Using Directives

using System.Collections.Generic;
using Vulcan.Exports.Commands;

#endregion

namespace Vulcan.Commands.Network.Email
{
    public class EmailTaskModel : Command
    {
        public List<string> Recipients { get; set; }
        public string FromAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtmlBody { get; set; }
        public List<string> Attachments { get; set; }
    }
}