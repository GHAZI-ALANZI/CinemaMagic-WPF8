using CinemaMagic.Models.DTOs.ProductManagement;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CinemaMagic.Models.DTOs.Bills
{
    public class BillDTO : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string BillDate { get; set; }

        private int total;
        public int Total
        {
            get => total;
            set
            {
                total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        public BillDTO() { }


        public int Staff_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Showtime_Id { get; set; }


        private int discount;
        public int Discount
        {
            get => discount;
            set
            {
                discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }
        public string Note { get; set; }


        public string Interval { get; set; }
        public string NameAuditorium { get; set; }
        public string Seats { get; set; }
        public int QuantityTicket { get; set; }
        public int PerTicketPrice { get; set; }
        public string TicketPrice { get; set; }
        public string MovieTitle { get; set; }
        public int TotalPriceTicket { get; set; }
        public string StartDate { get; set; }
        public string StartAndEndTime { get; set; }


        public ObservableCollection<ProductDTO> DSSP { get; set; }
        public int TotalPriceProduct { get; set; }
        public bool[] _canAccept = new bool[3];
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

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }




        private void ValidateFullName()
        {
            if (string.IsNullOrWhiteSpace(FullName))
            {
                FullNameError = "Name error!!!";
                _canAccept[1] = false;
            }
            else
            {
                FullNameError = "";
                _canAccept[1] = true;
            }
        }

        private void ValidateEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailError = "Email error!!!";
                _canAccept[2] = false;

            }
            else if (!Email.Contains("@"))
            {
                EmailError = "Email Not Valide!";
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
                PhoneNumberError = "phone number not valide!";
                _canAccept[0] = false;
            }
            else if (!PhoneNumber.All(char.IsDigit))
            {
                PhoneNumberError = "phone number not valide!";
                _canAccept[0] = false;

            }
            else
            {
                PhoneNumberError = "";
                _canAccept[0] = true;
            }
        }



    }
}
