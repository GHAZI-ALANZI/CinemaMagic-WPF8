using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Views.MessageBox;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.InformationManagement
{
    public partial class InformationViewModel
    {
        public ICommand changePasswordCommand { get; set; }


        // Error: Invalid original password
        private string password1Error;
        public string Password1Error
        {
            get => password1Error;
            set
            {
                password1Error = value;
                OnPropertyChanged(nameof(Password1Error));
            }
        }



        // Error: Incorrect new password"
        private string password2Error;
        public string Password2Error
        {
            get => password2Error;
            set
            {
                password2Error = value;
                OnPropertyChanged(nameof(Password2Error));
            }
        }

        // Error: Re-enter new password mismatch"
        private string password3Error;
        public string Password3Error
        {
            get => password3Error;
            set
            {
                password3Error = value;
                OnPropertyChanged(nameof(Password3Error));
            }
        }


        private void ChangePassword()
        {
            changePasswordCommand = new ViewModelCommand(changePassword, canChangePassword);
        }

        private void changePassword(object obj)
        {
            if (!ValidatePassword1())
            {
                return;
            }
            if (!ValidatePassword2()) { return; }
            if (!ValidatePassword3()) { return; }

            UserDA userDA = new UserDA();
            userDA.changePassword(Staff_Id, PTChung.EncryptMD5(informationView.txtMKMoi.Password));
            YesMessageBox mb = new YesMessageBox("Notification", "Password changed successfully");
            mb.ShowDialog();
            informationView.txtMKCu.Password = "";
            informationView.txtMKMoi.Password = "";
            informationView.txtXacNhanMKMoi.Password = "";
        }

        private bool canChangePassword(object obj)
        {
            if (informationView.txtMKCu.Password == "" || informationView.txtMKMoi.Password == "" || informationView.txtXacNhanMKMoi.Password == "")
            {
                return false;
            }
            return true;
        }



        // validate
        private bool ValidatePassword1()
        {
            UserDA userDA = new UserDA();
            if (PTChung.EncryptMD5(informationView.txtMKCu.Password) != userDA.passwordStaff_Id(Staff_Id))
            {
                Password1Error = "The old password is incorrect, please try again!";
                return false;
            }
            else
            {
                Password1Error = "";
                return true;
            }
        }


        private bool ValidatePassword2()
        {
            if (informationView.txtMKMoi.Password.Length < 5)
            {
                Password2Error = "The password must be longer than 5 characters!";
                return false;
            }
            else if (informationView.txtMKMoi.Password.Contains(" "))
            {
                Password2Error = "The password cannot contain spaces!";
                return false;
            }
            else if (PTChung.ContainsUnicodeCharacter(informationView.txtMKMoi.Password))
            {
                Password2Error = "The password cannot contain accents!";
                return false;
            }
            else
            {
                Password2Error = "";
                return true;
            }
        }


        private bool ValidatePassword3()
        {
            if (informationView.txtMKMoi.Password != informationView.txtXacNhanMKMoi.Password)
            {
                Password3Error = "Not matching with the new password!";
                return false;
            }
            else
            {
                Password3Error = "";
                return true;
            }
        }
    }
}
