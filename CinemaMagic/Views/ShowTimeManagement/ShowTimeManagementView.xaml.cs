using CinemaMagic.ViewModels.ShowTimeManagementVM;
using System.Windows.Controls;

namespace CinemaMagic.Views.ShowTimeManagement
{
    /// <summary>
    /// Interaction logic for ShowTimeManagementView.xaml
    /// </summary>
    public partial class ShowTimeManagementView : UserControl
    {
        public ShowTimeManagementView(int Staff_Id)
        {
            InitializeComponent();
            ShowTimeManagementViewModel showTimeManagementViewModel = new ShowTimeManagementViewModel(Staff_Id);
            this.DataContext = showTimeManagementViewModel;

        }
    }
}
