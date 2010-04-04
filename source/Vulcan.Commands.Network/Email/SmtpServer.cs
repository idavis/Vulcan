#region Using Directives

using System.Net;
using System.Net.Mail;
using Vulcan.Exports;

#endregion

namespace Vulcan.Commands.Network.Email
{
    public class SmtpServer : ISmtpServer
    {
        public SmtpServer( SmtpModel model )
        {
            Model = model;
        }

        public SmtpModel Model { get; protected set; }

        #region ISmtpServer Members

        public void Send( MailMessage mailMessage )
        {
            using ( var smtpClient = new SmtpClient( Model.Host, Model.Port ) )
            {
                if ( !Model.UseDefaultCredentialsForEmail )
                {
                    smtpClient.Credentials = new NetworkCredential( Model.UserName, Model.Password );
                }
                smtpClient.Send( mailMessage );
            }
        }

        #endregion
    }
}