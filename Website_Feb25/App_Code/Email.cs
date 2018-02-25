using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Text;

/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{
    private string emailTo;
    private string body;
    private string subject;
    private static string emailFrom = "elkTest484@gmail.com";
    private static string emailPass = "Pwd#64cole";
    
    public Email(string emailTo, string body, string subject)
    {
        EmailTo = emailTo;
        Body = body;
        Subject = subject;
    }

    public string EmailTo
    {
        get
        {
            return emailTo;
        }
        private set
        {
            emailTo = value;
        }
    }

    public string Body
    {
        get
        {
            return body;
        }
        private set
        {
            body = value;
        }
    }

    public string Subject
    {
        get
        {
            return subject;
        }
        private set
        {
            subject = value;
        }
    }

    public void sendEmail()
    {
        try
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(this.emailTo);
            mail.From = new System.Net.Mail.MailAddress(emailFrom,"Elk", System.Text.Encoding.UTF8);
            mail.Subject = "Elk Account Email";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = this.body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(emailFrom, emailPass);
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;

            client.Send(mail);
        }
        catch (Exception)
        {

        }
    }
}