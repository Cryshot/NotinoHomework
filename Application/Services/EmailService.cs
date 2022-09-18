using NotinoHomework.Application.Interfaces;
using System.Net.Mail;

namespace NotinoHomework.Application.Services;

public class EmailService : IEmailService
{
    private const string TO_EMAIL_ADDRESS = "to@email.adr";
    private const string FROM_EMAIL_ADDRESS = "from@email.adr";

    private const string SMTP_HOST = "mail.server";

    public void Send(string attachmentAsString, string attachmentName)
    {
        var mail = PrepareMail(attachmentAsString, attachmentName);
        SendMail(mail);
    }

    private static MailMessage PrepareMail(string attachmentAsString, string attachmentName)
    {
        var mail = new MailMessage
        {
            Subject = "Converted Document",
            From = new MailAddress(FROM_EMAIL_ADDRESS),
        };

        mail.To.Add(TO_EMAIL_ADDRESS);

        var atta = Attachment.CreateAttachmentFromString(attachmentAsString, attachmentName);
        mail.Attachments.Add(atta);

        return mail;
    }

    private static void SendMail(MailMessage mail)
    {
        using var smtpClient = new SmtpClient(SMTP_HOST);
        smtpClient.Send(mail);
    }
}