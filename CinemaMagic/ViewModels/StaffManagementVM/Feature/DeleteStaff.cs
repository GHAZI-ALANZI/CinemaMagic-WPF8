﻿using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.StaffManagement;
using CinemaMagic.Views.MessageBox;
using System.Windows;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {
        public ICommand deleteStaffCommand { get; set; }

        void delete()
        {
            deleteStaffCommand = new ViewModelCommand(deleteStaff);
        }

        private void deleteStaff(object obj)
        {
            // UserDA userDA = new UserDA();
            StaffDA staffDA = new StaffDA();
            if (obj is StaffDTO staff)
            {
                if (StaffId != staff.Id)
                {

                    YesNoMessageBox mb = new YesNoMessageBox("Notification", "Do you want to delete this employee?");
                    mb.ShowDialog();
                    if (mb.DialogResult == false)
                        return;
                    else
                    {

                        staffDA.deleteStaff(staff);
                        mb.Close();
                        YesMessageBox msb = new YesMessageBox("Notification", "Successfully deleted");
                        msb.ShowDialog();
                        msb.Close();
                    }
                    loadData();
                }
                else
                {
                    MessageBox.Show("You cannot delete yourself!");
                }
            }
        }
    }
}
