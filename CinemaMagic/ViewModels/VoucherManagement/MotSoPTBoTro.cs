using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CinemaMagic.ViewModels.VoucherManagement
{
    public class MotSoPTBoTro
    {

        // Voucher creation function

        public static string createCode()
        {
            string codeNew = "";

            VoucherDA voucherDA = new VoucherDA();
            List<string> listVoucher = voucherDA.listCode();

            while (true)
            {
                bool check = true;
                Random random = new Random();

                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                StringBuilder stringBuilder = new StringBuilder(10);

                for (int i = 0; i < 10; i++)
                {
                    stringBuilder.Append(chars[random.Next(chars.Length)]);
                }
                codeNew = stringBuilder.ToString();

                foreach (string s in listVoucher)
                {
                    if (codeNew == s)
                    {
                        check = false;
                        break;
                    }
                }

                if (check == true)
                {
                    break;
                }

            }
            return codeNew;
        }

        // Method to create a random Excel file name

        public static string RandomFileName()
        {
            Random random = new Random();
            int passwordLength = random.Next(10, 20);
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var password = new char[passwordLength];

            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }



        //QRCode
        private static MemoryStream memoryImage(string noidungQR)
        {
            QRCodeGenerator QG = new QRCodeGenerator();
            var myData = QG.CreateQrCode(noidungQR, QRCodeGenerator.ECCLevel.H);
            var code = new QRCode(myData);
            using (var bitmap = code.GetGraphic(50))
            {
                var memory = new MemoryStream();
                try
                {
                    bitmap.Save(memory, ImageFormat.Png);
                }
                catch { }
                return memory;
            }
        }


        // Function to send voucher via email to a single customer
        public static void sendVoucherByMail(string fullName, VoucherDTO voucherDTO, MailMessage mailMessage, string fromMail, string fromPassword, CustomerDTO customerDTO, string LoaiVoucher)
        {
            string noidung =
               "Cinema UIT sends to valued customers: " + fullName + ".<br>"
                +
                "Start date: " + voucherDTO.StartDate + ".<br>"
                +
                "Voucher name: " + voucherDTO.Name + ".<br>"
                +

                "Voucher information: " + voucherDTO.VoucherDetail + ".<br>"
                +
                "Code: " + voucherDTO.Code + ".<br>"
                +
                "Expiry date: " + voucherDTO.FinDate + ".<br>"
                +
                "Thank you for your support, your satisfaction is our success!";


            mailMessage.Body = "<html><body>" + noidung + "</body></html>";
            mailMessage.IsBodyHtml = true;


            string noidungQR =
                "Code: " + voucherDTO.Code + ".\n"
                +
                "Voucher name: " + voucherDTO.Name + ".\n"
                +
                "Start date: " + voucherDTO.StartDate + ".\n"
                +
                "End date: " + voucherDTO.FinDate + ".\n"
                +
                "Voucher information: " + voucherDTO.VoucherDetail + ".\n";


            // Start sending QR code and content

            if (memoryImage(noidungQR).Length > 0)// Successfully created memory
            {
                try
                {
                    using (var memory = memoryImage(noidungQR))
                    {
                        memory.Position = 0;
                        Attachment qrAttachment = new Attachment(memory, "qrcode.png", "image/png");

                        mailMessage.Attachments.Add(qrAttachment);

                        try
                        {
                            var smtpClient = new SmtpClient("smtp.gmail.com")
                            {
                                Port = 587,
                                Credentials = new NetworkCredential(fromMail, fromPassword),
                                EnableSsl = true
                            };


                            try
                            {
                                smtpClient.Send(mailMessage);


                                // Update points
                                CustomerDA customerDA = new CustomerDA();
                                customerDA.updatePoint(customerDTO, LoaiVoucher);
                            }
                            catch { }
                        }
                        catch { }
                    }
                }
                catch { }
            }
            else// If there's no QR code, send the raw content
            {
                try
                {
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromMail, fromPassword),
                        EnableSsl = true
                    };
                    try
                    {
                        smtpClient.Send(mailMessage);

                        // Update points
                        CustomerDA customerDA = new CustomerDA();
                        customerDA.updatePoint(customerDTO, LoaiVoucher);
                    }
                    catch { }
                }
                catch { }
            }
        }
    }
}
