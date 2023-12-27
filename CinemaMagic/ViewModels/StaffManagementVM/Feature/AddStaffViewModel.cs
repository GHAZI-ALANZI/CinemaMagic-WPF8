using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using CinemaMagic.Models.DTOs.StaffManagement;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {
        public ICommand showWdAddStaffCommand { get; set; }
        void addStaff()
        {
            showWdAddStaffCommand = new ViewModelCommand(showWdAddStaff);
        }

        private void showWdAddStaff(object obj)
        {
            StaffAddView staffAddView = new StaffAddView();
            staffAddView.ShowDialog();

            //load  data
            loadData();
        }
    }

    public class AddStaffViewModel : MainBaseViewModel
    {

        private StaffAddView wd;//serve quit and add


        // Add for the table Staff

        // First and last name
        private string fullName;
        public string FullName
        {
            get => fullName;
            set
            {
                fullName = value;
                ValidateFullName();
            }
        }

        private string fullNameError;
        public string FullNameError
        {
            get => fullNameError;
            set
            {
                fullNameError = value;
                OnPropertyChanged(nameof(FullNameError));
            }
        }



        // Gender
        public string Gender { get; set; }



        // Date of birth (validate to ensure the date is before the current date)
        private DateTime? birth;
        public DateTime? Birth
        {
            get => birth;
            set
            {
                birth = value;
                ValidateBirth();
            }
        }

        private string birthError;
        public string BirthError
        {
            get => birthError;
            set
            {
                birthError = value;
                OnPropertyChanged(nameof(BirthError));
            }
        }




        //Email
        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
            }
        }
        private string emailError;
        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged(nameof(EmailError));
            }
        }




        // Phone number
        private string phoneNumber;
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                ValidatePhoneNumber();
            }
        }

        private string phoneNumberError;
        public string PhoneNumberError
        {
            get => phoneNumberError;
            set
            {
                phoneNumberError = value;
                OnPropertyChanged(nameof(PhoneNumberError));
            }
        }




        // Position

        public string Role { get; set; }



        // Start date
        private DateTime? ngayVL;
        public DateTime? NgayVL
        {
            get => ngayVL;
            set
            {
                ngayVL = value;
                ValidateNgayVL();
            }
        }

        private string ngayVLError;
        public string NgayVLError
        {
            get => ngayVLError;
            set
            {
                ngayVLError = value;
                OnPropertyChanged(nameof(NgayVLError));
            }
        }





        // Salary
        private string salary;
        public string Salary
        {
            get => salary;
            set
            {
                salary = value;
                ValidateSalary();
            }
        }
        private string salaryError;
        public string SalaryError
        {
            get => salaryError;
            set
            {
                salaryError = value;
                OnPropertyChanged(nameof(SalaryError));
            }
        }



        //add account table"

        //account name
        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                ValidateUsername();
            }
        }

        private string usernameError;
        public string UsernameError
        {
            get => usernameError;
            set
            {
                usernameError = value;
                OnPropertyChanged(nameof(UsernameError));
            }
        }




        //password entry error
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

        //password re-entry error
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



        private bool[] _canAccept = new bool[7];//serve the purpose of allowing button accept k press

        public ICommand CancelCommand { get; set; }
        public ICommand acceptAddCommand { get; set; }//agree to add

        public AddStaffViewModel(StaffAddView wd)
        {
            this.wd = wd;
            CancelCommand = new ViewModelCommand(cancel);
            acceptAddCommand = new ViewModelCommand(acceptAdd, canAcceptAdd);

            Gender = "Nam";
            Role = "Quản lý";
            Birth = DateTime.UtcNow;
            NgayVL = DateTime.UtcNow;
        }
        private void cancel(object obj)
        {
            wd.Close();
        }

        //agree to add
        private void acceptAdd(object obj)
        {

            if (!ValidatePassword1())
            {
                return;
            }
            if (!ValidatePassword2()) { return; }


            //add vào bảng Staff
            StaffDA staffDA = new StaffDA();
            staffDA.addStaff(new StaffDTO(FullName, Birth.Value.ToString("yyyy-MM-dd"), Gender, Email, PhoneNumber, int.Parse(Salary), Role, NgayVL.Value.ToString("yyyy-MM-dd")));

            //add vào bảng Account
            UserDA userDA = new UserDA();
            userDA.addAccount(new UserDTO(Username, PTChung.EncryptMD5(wd.txtMatKhau.Password), staffDA.identCurrent()));

            YesMessageBox mb = new YesMessageBox("Notification", "Employee added successfully");
            mb.ShowDialog();
            wd.Close();

        }

        private bool canAcceptAdd(object obj)
        {
            bool check = _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3] && _canAccept[4] && _canAccept[5] && _canAccept[6];
            if (check == false || wd.txtMatKhau.Password == "" || wd.txtNhapLaiMatKhau.Password == "")
            {
                return false;
            }
            return true;
        }

        //Validate
        private void ValidateFullName()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                FullNameError = "First and last name cannot be empty!";
                _canAccept[0] = false;
            }
            else
            {
                FullNameError = "";
                _canAccept[0] = true;
            }
        }


        private void ValidateBirth()
        {
            if (Birth > DateTime.UtcNow)
            {
                BirthError = "Date of birth is invalid!";
                _canAccept[1] = false;
            }
            else
            {
                BirthError = "";
                _canAccept[1] = true;
            }
        }

        private void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email cannot be empty!";
                _canAccept[2] = false;
            }
            else if (!Email.Contains("@"))
            {
                EmailError = "Email is invalid!";
                _canAccept[2] = false;
            }
            else
            {
                EmailError = "";
                _canAccept[2] = true;
            }
        }

        private void ValidatePhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                PhoneNumberError = "Phone number cannot be empty!";
                _canAccept[3] = false;
            }
            else if (!PhoneNumber.All(char.IsDigit))
            {
                PhoneNumberError = "The phone number can only contain digits";
                _canAccept[3] = false;
            }
            else
            {
                PhoneNumberError = "";
                _canAccept[3] = true;
            }
        }


        private void ValidateNgayVL()
        {
            if (NgayVL < Birth)
            {
                NgayVLError = "Inception date must be greater than the date of birth!";
                _canAccept[4] = false;
            }
            else
            {
                NgayVLError = "";
                _canAccept[4] = true;
            }
        }


        private void ValidateSalary()
        {
            if (string.IsNullOrWhiteSpace(Salary))
            {
                SalaryError = "Invalid salary!";
                _canAccept[5] = false;
            }
            else if (!Salary.All(char.IsDigit))
            {
                SalaryError = "Salary is invalid!";
                _canAccept[5] = false;
            }
            else if (int.Parse(Salary) < 0)
            {
                SalaryError = "Salary is invalid!";
                _canAccept[5] = false;
            }
            else
            {
                SalaryError = "";
                _canAccept[5] = true;
            }
        }


        private void ValidateUsername()
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                UsernameError = "Username cannot be empty!";
                _canAccept[6] = false;
            }
            else if (Username.Contains(" "))
            {
                UsernameError = "Invalid username!";
                _canAccept[6] = false;
            }
            else if (Username.Length < 6)
            {
                UsernameError = "Username must be greater than 6 characters!";
                _canAccept[6] = false;
            }
            else if (!MotSoPTBoTro.uniqueUsername(Username))
            {
                UsernameError = "The username already exists!";
                _canAccept[6] = false;
            }
            else
            {
                UsernameError = "";
                _canAccept[6] = true;
            }
        }



        private bool ValidatePassword1()
        {
            if (wd.txtMatKhau.Password.Length < 5)
            {
                Password1Error = "The password must be greater than 5 characters!";
                return false;
            }
            else if (wd.txtMatKhau.Password.Contains(" "))
            {
                Password1Error = "The password cannot contain spaces!";
                return false;
            }
            else if (PTChung.ContainsUnicodeCharacter(wd.txtMatKhau.Password))
            {
                Password1Error = "The password cannot contain accents!";
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
            if (wd.txtMatKhau.Password != wd.txtNhapLaiMatKhau.Password)
            {
                Password2Error = "The passwords do not match, please re-enter!";
                return false;
            }
            else
            {
                Password2Error = "";
                return true;
            }
        }
    }
}
