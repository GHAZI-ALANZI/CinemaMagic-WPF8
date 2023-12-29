using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using CinemaMagic.Views.MessageBox;
using System.Net.Mail;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.VoucherManagement
{
    public partial class VoucherManagementViewModel
    {

        // Serve for successful email send notification
        private string notify;
        public string Notify
        {
            get { return notify; }
            set
            {
                notify = value;
                OnPropertyChanged(nameof(Notify));
            }
        }


        public ICommand sendVoucherCommand { get; set; }

        private void SendVoucher()
        {
            sendVoucherCommand = new ViewModelCommand(sendVoucher);
        }
        private void sendVoucher(object obj)
        {
            YesNoMessageBox mb = new YesNoMessageBox("Send voucher", "Do you want to send the voucher to the customer?");
            mb.ShowDialog();
            if (mb.DialogResult == true)
            {

                VoucherDTO getVoucher = obj as VoucherDTO;
                string LoaiVoucher = getVoucher.Type;

                CustomerDA customerDA = new CustomerDA();
                List<CustomerDTO> DSKHVip = new List<CustomerDTO>(customerDA.getKHVip(LoaiVoucher));// Reason I created a new list - not using references to prevent issues when sending mail, as new items might be added (as I'm using Tasks).

                Task.Run(async () =>
                {
                    string fromMail = "UITIT008CinemaMagic@gmail.com";
                    string fromPassWord = "ribrkocvhzhpuima";


                    foreach (CustomerDTO item in DSKHVip)
                    {
                        MailMessage mailMessage = new MailMessage();
                        try
                        {
                            mailMessage.From = new MailAddress(fromMail);
                            mailMessage.Subject = "FREE VOUCHER!";
                            mailMessage.To.Add(new MailAddress(item.Email));
                        }
                        catch { }


                        MotSoPTBoTro.sendVoucherByMail(item.FullName, getVoucher, mailMessage, fromMail, fromPassWord, item, LoaiVoucher);
                    }

                    Notify = "Successfully sent voucher to customer's email!";
                    await Task.Delay(2000);
                    Notify = "";
                });
            }
        }
    }
}
