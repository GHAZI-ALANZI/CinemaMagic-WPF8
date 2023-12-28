using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.StaffManagement;
using CinemaMagic.Views.MessageBox;
using CinemaMagic.Views.StaffManagement;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {
        public ICommand showWdEditStaffCommand { get; set; }

        void editStaff()
        {
            showWdEditStaffCommand = new ViewModelCommand(showWdEditStaff);
        }


        private void showWdEditStaff(object obj)
        {
            StaffDTO staff = obj as StaffDTO;
            StaffEditView staffEditView = new StaffEditView(staff);
            staffEditView.ShowDialog();
            loadData();
        }
    }


    public class EditStaffViewModel : MainBaseViewModel
    {
        private StaffEditView wd;//Serve the cancel button
        public ICommand quitCommand { get; set; }// Cancel editing
        public ICommand acceptCommand { get; set; }// Edit confirmation

        public StaffDTO staffDTO { get; set; }// Serve to display current staff when clicking on edit

        public EditStaffViewModel(StaffEditView wd)
        {
            this.wd = wd;
            quitCommand = new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accept, canAccept);
        }

        public void khoitao()
        {
            FullName = staffDTO.FullName;
            Gender = staffDTO.Gender;

            string[] birth = staffDTO.Birth.Split('/');
            Birth = new DateTime(int.Parse(birth[2]), int.Parse(birth[1]), int.Parse(birth[0]));

            Email = staffDTO.Email;
            PhoneNumber = staffDTO.PhoneNumber;
            Role = staffDTO.Role;

            string[] ngayvl = staffDTO.NgayVaoLam.Split("/");
            NgayVL = new DateTime(int.Parse(ngayvl[2]), int.Parse(ngayvl[1]), int.Parse(ngayvl[0]));

            Salary = staffDTO.Salary.ToString();
        }
        private void quit(object obj)
        {
            wd.Close();
        }

        private void accept(object obj)
        {
            StaffDA staffDA = new StaffDA();
            staffDA.updateStaff(new StaffDTO(staffDTO.Id, FullName, Birth.Value.ToString("yyyy-MM-dd"), Gender, Email, PhoneNumber, int.Parse(Salary), Role, NgayVL.Value.ToString("yyyy-MM-dd")));
            YesNoMessageBox mb = new YesNoMessageBox("Notification", "Do you want to edit employee information?");
            mb.ShowDialog();
            if (mb.DialogResult == false)
                return;
            else
            {
                mb.Close();
                YesMessageBox msb = new YesMessageBox("Notification", "Information updated successfully");
                msb.ShowDialog();
                msb.Close();
                wd.Close();
            }
        }

        // Full name
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

        // Date of birth (ensure the date is not greater than the current date)
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

        //phoneNumber
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

        private bool[] _canAccept = new bool[6];// Serve for edit confirmation
        private bool canAccept(object obj)
        {
            return _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3] && _canAccept[4] && _canAccept[5];
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
                EmailError = "Invalid email!";
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
                PhoneNumberError = "The phone number can only contain digits!";
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
                NgayVLError = "Start date must be after the date of birth!";
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
                SalaryError = "Salary cannot be empty!";
                _canAccept[5] = false;
            }
            else if (!Salary.All(char.IsDigit))
            {
                SalaryError = "Invalid salary!";
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
    }
}
