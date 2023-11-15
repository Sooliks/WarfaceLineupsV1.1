



using System.Net;
using System.Net.Mail;

namespace WarfaceLineups.Utils;

public class EmailService
{
    private static readonly string Mail = ";
    private static readonly string Password = "";
    private static readonly string Username = "";
    private static readonly string Host = "smtp.mail.ru";
    private static readonly int Port = 587;
    
    public static bool SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            using (MailMessage mm = new MailMessage(Mail, email))
            {
                mm.Subject = subject;
                mm.Body = message;
                mm.IsBodyHtml = false;
                using (SmtpClient sc = new SmtpClient(Host, Port))
                {
                    sc.EnableSsl = true;
                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = new NetworkCredential(Mail, Password);
                    sc.Send(mm);
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }
}
