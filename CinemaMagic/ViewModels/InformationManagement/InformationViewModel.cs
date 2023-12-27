using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.StaffManagement;
using System.Windows.Media.Imaging;

namespace CinemaMagic.ViewModels.InformationManagement
{
    public partial class InformationViewModel : MainBaseViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Birth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string NgayVL { get; set; }
        public int Salary { get; set; }

        private BitmapImage imageSource;
        public BitmapImage? ImageSource
        {
            get => imageSource;
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    OnPropertyChanged(nameof(ImageSource));
                }
            }
        }


        private int Staff_Id;
        private InformationView informationView;
        public InformationViewModel(int Staff_Id, InformationView informationView)
        {
            this.Staff_Id = Staff_Id;
            loadData();
            EditImage();
            this.informationView = informationView;
            ChangePassword();
        }

        private void loadData()
        {
            StaffDA staffDA = new StaffDA();
            StaffDTO staffDTO = staffDA.Staffstaff_Id(Staff_Id);

            if (staffDTO == null)// If there's an error connecting to SQL, use default settings
            {
                Id = "NV001";
                FullName = "Jane Hans";
                Gender = "Male";
                Birth = "10/10/1999";
                Email = "jane@gmail.com";
                PhoneNumber = "1234567890";
                Role = "Manager";
                NgayVL = "12/12/2023";
                Salary = 9999999;
            }
            else
            {
                Id = staffDTO.IdFormat;
                FullName = staffDTO.FullName;
                Gender = staffDTO.Gender;
                Birth = staffDTO.Birth;
                Email = staffDTO.Email;
                PhoneNumber = staffDTO.PhoneNumber;
                Role = staffDTO.Role;
                NgayVL = staffDTO.NgayVaoLam;
                Salary = staffDTO.Salary;
                ImageSource = staffDTO.ImageSource;
            }
        }

    }
}
