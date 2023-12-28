using CinemaMagic.ViewModels.StaffManagementVM;
using System.Windows;

namespace CinemaMagic.Views.StaffManagement
{
    /// <summary>
    /// Interaction logic for StaffAddView.xaml
    /// </summary>
    public partial class StaffAddView : Window
    {
        public StaffAddView()
        {
            InitializeComponent();
            AddStaffViewModel addStaffViewModel = new AddStaffViewModel(this);
            this.DataContext = addStaffViewModel;
        }
    }
}
