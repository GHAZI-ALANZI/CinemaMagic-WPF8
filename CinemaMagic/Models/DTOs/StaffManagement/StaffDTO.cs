﻿using CinemaMagic.Models.DataAccessLayer;
using System.Windows.Media.Imaging;

namespace CinemaMagic.Models.DTOs.StaffManagement
{
    public class StaffDTO
    {
        public int Id { get; set; }
        public string IdFormat { get; set; }
        public string FullName { get; set; }
        public string Birth { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        public string Role { get; set; }
        public string NgayVaoLam { get; set; }
        public BitmapImage ImageSource { get; set; }


        public StaffDTO(int id, string fullName, string birth, string gender, string email, string phoneNumber, int salary, string role, string ngayVL)
        {
            Id = id;
            FullName = fullName;
            Birth = birth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Salary = salary;
            Role = role;
            NgayVaoLam = ngayVL;
            IdFormat = StaffDA.formatID(Id);
        }



        public StaffDTO(string fullName, string birth, string gender, string email, string phoneNumber, int salary, string role, string ngayVL)
        {
            FullName = fullName;
            Birth = birth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Salary = salary;
            Role = role;
            NgayVaoLam = ngayVL;
        }



        public StaffDTO(int id, string fullName, string birth, string gender, string email, string phoneNumber, int salary, string role, string ngayVL, BitmapImage imageSource)
        {
            Id = id;
            FullName = fullName;
            Birth = birth;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            Salary = salary;
            Role = role;
            NgayVaoLam = ngayVL;
            IdFormat = StaffDA.formatID(Id);
            ImageSource = imageSource;
        }
    }
}
