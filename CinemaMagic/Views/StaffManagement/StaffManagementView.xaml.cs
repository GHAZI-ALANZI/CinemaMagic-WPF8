using CinemaMagic.ViewModels.StaffManagementVM;
using System.Windows.Controls;

namespace CinemaMagic.Views.StaffManagement
{
    /// <summary>
    /// Interaction logic for StaffManagementView.xaml
    /// </summary>
    public partial class StaffManagementView : UserControl
    {



        public StaffManagementView(int StaffID)
        {
            InitializeComponent();
            StaffManageVM staffManageVM = new StaffManageVM(this, StaffID);
            this.DataContext = staffManageVM;
        }

    }
}
