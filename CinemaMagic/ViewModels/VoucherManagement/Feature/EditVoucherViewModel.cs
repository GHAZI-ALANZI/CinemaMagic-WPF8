using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.VoucherManagement
{
    public partial class VoucherManagementViewModel
    {
        public ICommand showWdEditVoucherCommand { get; set; }

        void editVoucher()
        {
            showWdEditVoucherCommand = new ViewModelCommand(showWdEditVoucher);
        }

        private void showWdEditVoucher(object obj)
        {
            EditVoucher editVoucher = new EditVoucher((VoucherDTO)obj);
            editVoucher.ShowDialog();

            loadData();
        }
    }


    public class EditVoucherViewModel : MainBaseViewModel
    {
        private EditVoucher wd;
        public VoucherDTO voucher { get; set; }//voucher current
        public ICommand quitCommand { get; set; }
        public ICommand acceptCommand { get; set; }
        //Voucher Name
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
            }
        }

        private string nameError;
        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged(nameof(NameError));
            }
        }
        // Voucher information
        private string voucherDetail;
        public string VoucherDetail
        {
            get => voucherDetail;
            set
            {
                voucherDetail = value;
                ValidateVoucherDetail();
            }
        }
        private string voucherDetailError;
        public string VoucherDetailError
        {
            get => voucherDetailError;
            set
            {
                voucherDetailError = value;
                OnPropertyChanged(nameof(VoucherDetailError));
            }
        }
        public string Type { get; set; }

        // Start date of voucher offer
        private DateTime? startDate;
        public DateTime? StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                ValidateStartDate();
            }
        }

        private string startDateError;
        public string StartDateError
        {
            get => startDateError;
            set
            {
                startDateError = value;
                OnPropertyChanged(nameof(StartDateError));
            }
        }
        // Voucher expiry date
        private DateTime? finDate;
        public DateTime? FinDate
        {
            get => finDate;
            set
            {
                finDate = value;
                ValidateFinDate();
            }
        }

        private string finDateError;
        public string FinDateError
        {
            get => finDateError;
            set
            {
                finDateError = value;
                OnPropertyChanged(nameof(FinDateError));
            }
        }
        public EditVoucherViewModel(EditVoucher wd)
        {
            this.wd = wd;
            quitCommand = new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accpet, canAcceptEdit);
        }

        public void loadEditCurrent()
        {
            Name = voucher.Name;
            VoucherDetail = voucher.VoucherDetail;
            Type = voucher.Type;

            string[] startDate = voucher.StartDate.Split('/');
            StartDate = new DateTime(int.Parse(startDate[2]), int.Parse(startDate[1]), int.Parse(startDate[0]));

            string[] finDate = voucher.FinDate.Split('/');
            FinDate = new DateTime(int.Parse(finDate[2]), int.Parse(finDate[1]), int.Parse(finDate[0]));
        }

        private void quit(object obj)
        {
            wd.Close();
        }

        private void accpet(object obj)
        {
            VoucherDA voucherDA = new VoucherDA();

            // Remember to handle user errors later
            voucherDA.editVoucher(new VoucherDTO(voucher.Id, Name, VoucherDetail, Type, StartDate.Value.ToString("yyyy-MM-dd"), FinDate.Value.ToString("yyyy-MM-dd")));
            YesNoMessageBox mb = new YesNoMessageBox("Notification", "Do you want to edit voucher information?");
            mb.ShowDialog();
            if (mb.DialogResult == false)
                return;
            else
            {
                wd.Close();
                YesMessageBox msb = new YesMessageBox("Notification", "Voucher information edited successfully");
                msb.ShowDialog();
                msb.Close();
            }

        }
        private bool[] _canAccept = new bool[4];// To facilitate pressing the accept button
        private bool canAcceptEdit(object obj)
        {
            return _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3];
        }

        // Validate functions
        private void ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = "Voucher name cannot be left empty!";
                _canAccept[0] = false;
            }
            else
            {
                NameError = "";
                _canAccept[0] = true;
            }
        }
        private void ValidateVoucherDetail()
        {
            if (string.IsNullOrWhiteSpace(VoucherDetail))
            {
                VoucherDetailError = "Voucher information cannot be left empty!";
                _canAccept[1] = false;
            }
            else
            {
                VoucherDetailError = "";
                _canAccept[1] = true;
            }
        }
        private void ValidateStartDate()
        {
            if (StartDate > FinDate)
            {
                StartDateError = "The voucher start date is invalid";
                _canAccept[2] = false;

            }
            else
            {
                StartDateError = "";
                _canAccept[2] = true;
            }
        }
        private void ValidateFinDate()
        {
            if (FinDate < StartDate)
            {
                FinDateError = "The expiry date of the voucher must be greater than the start date";
                _canAccept[3] = false;
            }
            else
            {
                FinDateError = "";
                _canAccept[3] = true;
            }
        }
    }
}
