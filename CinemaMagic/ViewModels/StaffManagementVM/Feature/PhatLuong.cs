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
            YesNoMessageBox wd = new YesNoMessageBox("Notification", "Do you want to distribute salaries to employees?");
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
                    YesMessageBox mb = new YesMessageBox("Notification", "Today is not salary day!");
                    mb.ShowDialog();
                    mb.Close();
                    return;
                }
                if (MotSoPTBoTro.checkSalary())
                {
                    wd.Close();
                    YesMessageBox mb = new YesMessageBox("Notification", "This month's salary has already been paid!");
                    mb.ShowDialog();
                    mb.Close();
                    return;
                }

                StaffSalaryDA staffSalaryDA = new StaffSalaryDA();
                staffSalaryDA.PhatLuongAll();
                wd.Close();
                YesMessageBox mb2 = new YesMessageBox("Notification", "Successful salary distribution!");
                mb2.ShowDialog();
                mb2.Close();
            }

        }
    }
}
