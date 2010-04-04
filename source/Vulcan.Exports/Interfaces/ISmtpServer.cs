#region Using Directives

using System.Net.Mail;

#endregion

namespace Vulcan.Exports
{
    /// <summary>
    /// Represents an SMTP server that can be used to send email messages.
    /// </summary>
    public interface ISmtpServer
    {
        /// <summary>
        /// Sends the mail message.
        /// </summary>
        /// <param name="mailMessage">The preconfigured message to send.</param>
        void Send( MailMessage mailMessage );
    }
}