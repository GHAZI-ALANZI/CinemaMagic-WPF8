﻿using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using CinemaMagic.Views.CustomerManagement;
using CinemaMagic.Views.MessageBox;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.CustomerManagement
{
    public partial class CustomerManagementViewModel
    {
        public ICommand showwdEditCustomerCommand { get; set; }

        void editCustomer()
        {
            showwdEditCustomerCommand = new ViewModelCommand(showwdEditCustomer);
        }
        private void showwdEditCustomer(object obj)
        {
            EditCustomerView editCustomerView = new EditCustomerView((CustomerDTO)obj);
            editCustomerView.ShowDialog();
            loadData();
        }
    }
    public class EditCustomerViewModel : MainBaseViewModel
    {
        private EditCustomerView wd;
        public CustomerDTO customer { get; set; } //current customer



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




        // Phone number line
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




        private DateTime? regDate;
        public DateTime? RegDate
        {
            get => regDate;
            set
            {
                regDate = value;
                ValidateRegDate();
            }
        }

        private string regDateError;
        public string RegDateError
        {
            get => regDateError;
            set
            {
                regDateError = value;
                OnPropertyChanged(nameof(regDateError));
            }
        }

        private string point;
        public string Point
        {
            get => point;
            set
            {
                point = value;
                ValidatePoint();
            }
        }
        private string pointError;
        public string PointError
        {
            get => pointError;
            set
            {
                pointError = value;
                OnPropertyChanged(nameof(PointError));
            }
        }



        private bool[] _canAccept = new bool[5];

        //command
        public ICommand quitCommand { get; set; }
        public ICommand acceptCommand { get; set; }



        public EditCustomerViewModel(EditCustomerView wd)
        {
            this.wd = wd;
            quitCommand = new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accept, canAccept);
        }



        public void loadEditCurrent()
        {
            FullName = customer.FullName;
            PhoneNumber = customer.PhoneNumber;
            Email = customer.Email;

            string[] regDate = customer.RegDate.Split('/');
            RegDate = new DateTime(int.Parse(regDate[2]), int.Parse(regDate[1]), int.Parse(regDate[0]));

            Point = customer.Point.ToString();

        }
        private void quit(object obj)
        {
            wd.Close();
        }
        private void accept(object obj)
        {
            CustomerDA customerDA = new CustomerDA();
            customerDA.editCustomer(new CustomerDTO(customer.Id, FullName, PhoneNumber, Email, RegDate.Value.ToString("yyyy-MM-dd"), int.Parse(Point)));
            YesMessageBox mb = new YesMessageBox("Notification", "Customer information updated successfully");
            mb.ShowDialog();
            wd.Close();
        }

        private bool canAccept(object obj)
        {
            return _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3] && _canAccept[4];
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


        private void ValidatePhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(PhoneNumber))
            {
                PhoneNumberError = "Phone number cannot be empty!";
                _canAccept[1] = false;
            }
            else if (!PhoneNumber.All(char.IsDigit))
            {
                PhoneNumberError = "Phone number can only contain digits!";
                _canAccept[1] = false;
            }
            else
            {
                PhoneNumberError = "";
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


        private void ValidateRegDate()
        {
            string[] birth = customer.Birth.Split('/');
            DateTime Birth = new DateTime(int.Parse(birth[2]), int.Parse(birth[1]), int.Parse(birth[0]));
            if (RegDate < Birth)
            {
                RegDateError = "Registration date must be later than date of birth!";
                _canAccept[3] = false;
            }
            else
            {
                RegDateError = "";
                _canAccept[3] = true;
            }
        }

        private void ValidatePoint()
        {
            if (string.IsNullOrWhiteSpace(Point))
            {
                PointError = "Invalid point!";
                _canAccept[4] = false;
            }
            else if (!Point.All(char.IsDigit))
            {
                PointError = "Invalid point!";
                _canAccept[4] = false;
            }
            else if (!int.TryParse(Point, out int pointvalue))
            {
                PointError = "Invalid!";
                _canAccept[4] = false;
            }
            else
            {
                PointError = "";
                _canAccept[4] = true;
            }
        }
    }
}
