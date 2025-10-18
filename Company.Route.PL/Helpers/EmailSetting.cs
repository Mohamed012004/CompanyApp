using System.Net;
using System.Net.Mail;

namespace Company.Route.PL.Helpers
{
    public static class EmailSetting
    {
        public static bool SendEmail(Email email)
        {

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true; ;
                // rtdthbvagzwvfedy
                client.Credentials = new NetworkCredential("melsayedewiss@gmail.com", "rtdthbvagzwvfedy");
                client.Send("melsayedewiss@gmail.com", email.To, email.Subject, email.Body);

                return true;
            }
            catch (Exception ex)
            {
                return false;

            }

        }
    }
}
