using CinemaMagic.Models.DTOs.StaffManagement;
using CinemaMagic.ViewModels.StaffManagementVM;
using System.Windows;

namespace CinemaMagic.Views.StaffManagement
{
    /// <summary>
    /// Interaction logic for StaffEditView.xaml
    /// </summary>
    public partial class StaffEditView : Window
    {
        public StaffEditView(StaffDTO staff)
        {
            InitializeComponent();
            EditStaffViewModel editStaffViewModel = new EditStaffViewModel(this);
            editStaffViewModel.staffDTO = staff;
            editStaffViewModel.khoitao();
            this.DataContext = editStaffViewModel;
        }
    }
}
