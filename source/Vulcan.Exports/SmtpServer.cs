#region Using Directives

using System.Net;
using System.Net.Mail;

#endregion

namespace Vulcan.Exports
{
    public class SmtpServer : ISmtpServer
    {
        public SmtpServer( SmtpTaskModel model )
        {
            Model = model;
        }

        public SmtpTaskModel Model { get; protected set; }

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