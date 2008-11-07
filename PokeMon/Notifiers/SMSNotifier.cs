using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace PokeMon
{
    class SMSNotifier : EmailNotifier
    {
        public SMSNotifier(string audience) 
            : base(audience) 
        {
        }


        public SMSNotifier(string audience, Result.ResultValue threshold)
            : base(audience, threshold)
        {
        }

        protected override MailMessage CreateMessage(Result result)
        {
            MailMessage mail = new MailMessage();

            mail.To.Add(ConvertToEmailAddresses(Audience));
            mail.From = new MailAddress(FromEmailAddress);

            // Don't worry about a subject, just send all the info in the body
            mail.Body = result.ToString();

            return mail;
        }


        public string ConvertToEmailAddresses(string newAudience)
        {
            // eliminate any phone number formatting or whitespace
            newAudience = newAudience.Replace("(", "");
            newAudience = newAudience.Replace(")", "");
            newAudience = newAudience.Replace("-", "");
            newAudience = newAudience.Replace(".", "");
            newAudience = newAudience.Replace(" ", "");

            // remove any trailing semi-colons.  If we don't, we could get the domain name copied twice 
            // if the entire string ends with a semi-colon
            newAudience = newAudience.TrimEnd(new char[] { ';' });

            // replace all semi-colons with domain name and a comma.  A comma because the MailMessage
            // To field takes a series of addresses seperated by commas, not semi-colons.
            newAudience = newAudience.Replace(";", SMSDomain + ",");

            // return new string with domain appended to get the last phone number in the string
            return newAudience + SMSDomain;
        }

        private const string SMSDomain = "@teleflip.com";
    }
}
