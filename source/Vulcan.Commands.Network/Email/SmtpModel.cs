using Vulcan.Exports.Commands;

namespace Vulcan.Commands.Network.Email
{
    public class SmtpModel
    {
        public string Password { get; protected set; }
        public string UserName { get; protected set; }
        public bool UseDefaultCredentialsForEmail { get; protected set; }
        public string Host { get; protected set; }
        public int Port { get; protected set; }
    }
}