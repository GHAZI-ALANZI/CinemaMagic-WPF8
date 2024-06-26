﻿using CinemaMagic.Models.DTOs.StaffManagement;
using CinemaMagic.Views.StaffManagement;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace CinemaMagic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {

        public ICommand StaffDetailCommand { get; set; }

        void staffDetail()
        {
            StaffDetailCommand = new ViewModelCommand(StaffDetail);
        }

        private void StaffDetail(object obj)
        {
            if (obj != null)
            {
                StaffDTO staff = (StaffDTO)obj;
                StaffDetailView staffDetailView = new StaffDetailView(staff);
                staffDetailView.ShowDialog();
            }
        }

    }


    public class StaffDetailViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Birth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        public string Role { get; set; }
        public string NgayVaoLam { get; set; }
        public BitmapImage ImageSource { get; set; }

        public ICommand exitCommand { get; set; }
        StaffDetailView wd;
        public StaffDetailViewModel(StaffDTO staff, StaffDetailView wd)
        {
            Id = staff.IdFormat;
            FullName = staff.FullName;
            Birth = staff.Birth;
            Gender = staff.Gender;
            Email = staff.Email;
            PhoneNumber = staff.PhoneNumber;
            Salary = staff.Salary;
            Role = staff.Role;
            NgayVaoLam = staff.NgayVaoLam;
            ImageSource = staff.ImageSource;
            exitCommand = new ViewModelCommand(exit);
            this.wd = wd;
        }

        private void exit(object obj)
        {
            wd.Close();
        }
    }
}
