#region Using Directives

using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using Vulcan.Exports;

#endregion

namespace Vulcan.Commands.Network.Email
{
    public class EmailSender : Disposable
    {
        public EmailSender( ISmtpServer smtpServer, EmailTaskModel emailTaskModel )
        {
            EmailTaskModel = emailTaskModel;
            SmtpServer = smtpServer;
        }

        public EmailTaskModel EmailTaskModel { get; private set; }
        public ISmtpServer SmtpServer { get; private set; }

        public void Send()
        {
            try
            {
                using ( MailMessage mailMessage = CreateMailMessage() )
                {
                    Trace.TraceInformation( "Sending email from [{0}] to [{1}] titled [{2}] with contents [{3}]",
                                            mailMessage.From,
                                            string.Join( ",",
                                                         mailMessage.To.Select( mailAddress => mailAddress.Address ).
                                                             ToArray() ),
                                            mailMessage.Subject, mailMessage.Body );
                    SmtpServer.Send( mailMessage );
                }
            }
            catch ( Exception )
            {
                Trace.TraceError( "Error sending email notification: {0}", EmailTaskModel );
                throw;
            }
        }

        private MailMessage CreateMailMessage()
        {
            var mailMessage = new MailMessage {From = new MailAddress( EmailTaskModel.FromAddress )};
            foreach (
                MailAddress recipientAddress in
                    EmailTaskModel.Recipients.Select( recipient => new MailAddress( recipient ) ) )
            {
                mailMessage.To.Add( recipientAddress );
            }
            foreach (
                Attachment attachment in EmailTaskModel.Attachments.Select( fileName => new Attachment( fileName ) ) )
            {
                mailMessage.Attachments.Add( attachment );
            }
            mailMessage.Body = EmailTaskModel.Body;
            mailMessage.Subject = EmailTaskModel.Subject;
            mailMessage.IsBodyHtml = EmailTaskModel.IsHtmlBody;
            return mailMessage;
        }
    }
}