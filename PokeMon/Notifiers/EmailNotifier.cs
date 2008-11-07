using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace PokeMon
{
    class EmailNotifier : Notifier
    {
        public EmailNotifier(string audience)
            : base(audience)
        {
        }

        public EmailNotifier(string audience, Result.ResultValue threshold)
            : base(audience, threshold)
        {
        }

        public override void Notify(Result result)
        {
            MailMessage message = CreateMessage(result);

            SendMail(message);
        }

        protected virtual MailMessage CreateMessage(Result result)
        {
            MailMessage mail = new MailMessage();

            mail.To.Add(Audience);
            mail.From = new MailAddress(FromEmailAddress);

            mail.Subject = result.ActionName + " " + result.Value.ToString() + "ed";
            mail.Body = result.ToString();

            return mail;
        }

        protected void SendMail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;

            client.SendAsync(mail, null);
        }

        protected const string FromEmailAddress = "PokeMon@TwoSpaces.Net";
    }
}
