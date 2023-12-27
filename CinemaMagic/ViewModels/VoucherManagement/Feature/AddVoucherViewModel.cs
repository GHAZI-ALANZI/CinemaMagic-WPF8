using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.VoucherManagement
{
    public partial class VoucherManagementViewModel
    {
        public ICommand ShowWDVoucherCommand { get; set; }


        void AddVoucher()
        {
            ShowWDVoucherCommand = new ViewModelCommand(ShowWDVoucher);
        }

        private void ShowWDVoucher(object obj)
        {
            AddVoucherView addVoucherView = new AddVoucherView();
            addVoucherView.ShowDialog();
            loadData();
        }
    }


    public class AddVoucherViewModel : MainBaseViewModel
    {
        private AddVoucherView wd;
        public ICommand quitCommand { get; set; }

        // Voucher name
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


        // Voucher details
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
        // Voucher type
        public string Type { get; set; }

        // Expiry date of voucher
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

        //Command for the accept button
        public ICommand acceptCommand { get; set; }
        public AddVoucherViewModel(AddVoucherView wd)
        {
            this.wd = wd;
            quitCommand = new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accept, canAcceptAdd);
            Type = "Vip 1";
        }
        private void quit(object obj)
        {
            wd.Close();
        }
        private void accept(object obj)
        {
            VoucherDA voucherDA = new VoucherDA();

            // Remember to fix checking code existence before adding
            voucherDA.addVoucher(new VoucherDTO(Name, MotSoPTBoTro.createCode(), VoucherDetail, Type, StartDate.Value.ToString("yyyy-MM-dd"), FinDate.Value.ToString("yyyy-MM-dd")));
            YesMessageBox mb = new YesMessageBox("Notification", "Voucher added successfully");
            mb.ShowDialog();
            wd.Close();
        }
        private bool[] _canAccept = new bool[4];// To facilitate pressing the accept button
        private bool canAcceptAdd(object obj)
        {
            return _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3];
        }

        //Validation Functions
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
            DateTime kq = (DateTime)StartDate;
            if (StartDate > FinDate || kq.Date < DateTime.Today)
            {
                StartDateError = "Invalid voucher start date";
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
