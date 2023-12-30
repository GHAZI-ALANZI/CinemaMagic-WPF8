using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Views.MessageBox;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {
        public ICommand PhatLuongCommand { get; set; }

        private void phatluong()
        {
            PhatLuongCommand = new ViewModelCommand(PhatLuong);
        }

        private void PhatLuong(object obj)
        {
            string[] s = DateTime.Today.ToString("yyyy-MM-dd").Split('-');
            YesNoMessageBox wd = new YesNoMessageBox("Notification", "Do you want to pay salary to employees?");
            wd.ShowDialog();
            if (wd.DialogResult == false)
            {
                return;
            }
            else
            {
                if (s[2] != "20")
                {
                    wd.Close();
                    YesMessageBox mb = new YesMessageBox("Notification", "Today is not a payday!");
                    mb.ShowDialog();
                    mb.Close();
                    return;
                }
                if (MotSoPTBoTro.checkSalary())
                {
                    wd.Close();
                    YesMessageBox mb = new YesMessageBox("Notification", "Salary has already been paid this month!");
                    mb.ShowDialog();
                    mb.Close();
                    return;
                }

                StaffSalaryDA staffSalaryDA = new StaffSalaryDA();
                staffSalaryDA.PhatLuongAll();
                wd.Close();
                YesMessageBox mb2 = new YesMessageBox("Notification", "Salary successfully paid!");
                mb2.ShowDialog();
                mb2.Close();
            }

        }
    }
}
