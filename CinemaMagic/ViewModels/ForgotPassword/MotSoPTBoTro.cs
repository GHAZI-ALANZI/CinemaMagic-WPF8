using CinemaMagic.Models.DataAccessLayer;
using System.Net;
using System.Net.Mail;

namespace CinemaMagic.ViewModels.ForgotPassword
{
    public class MotSoPTBoTro
    {

        public static bool sendMail(string username, string mailReceive)
        {
            try
            {
                string fromMail = "UITIT008CinemaMagic@gmail.com";
                string fromPassWord = "ribrkocvhzhpuima";

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(fromMail);
                mailMessage.Subject = "Password recovery";
                mailMessage.To.Add(new MailAddress(mailReceive));


                string noidung = "Your new password is: ";
                string passwordNew = RandomPasswordNew();
                mailMessage.Body = "<html><body>" + noidung + passwordNew + "</body></html>";
                mailMessage.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassWord),
                    EnableSsl = true
                };
                smtpClient.Send(mailMessage);


                // Modify the password in the account table for the given username
                UserDA userDA = new UserDA();
                userDA.changePassword(username, PTChung.EncryptMD5(passwordNew));
                return true;
            }
            catch { return false; }
        }



        public static string RandomPasswordNew()
        {
            Random random = new Random();
            int passwordLength = random.Next(5, 11);
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var password = new char[passwordLength];

            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }
    }
}
