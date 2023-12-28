using CinemaMagic.Models.DTOs.StaffManagement;
using CinemaMagic.ViewModels.StaffManagementVM;
using System.Windows;

namespace CinemaMagic.Views.StaffManagement
{
    /// <summary>
    /// Interaction logic for StaffDetailView.xaml
    /// </summary>
    public partial class StaffDetailView : Window
    {
        public StaffDetailView(StaffDTO staff)
        {
            InitializeComponent();
            StaffDetailViewModel staffDetailViewModel = new StaffDetailViewModel(staff, this);
            this.DataContext = staffDetailViewModel;
        }
    }
}
