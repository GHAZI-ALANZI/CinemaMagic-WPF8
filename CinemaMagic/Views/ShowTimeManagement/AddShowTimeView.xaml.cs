using CinemaMagic.ViewModels.ShowTimeManagementVM;
using System.Windows;

namespace CinemaMagic.Views.ShowTimeManagement
{
    /// <summary>
    /// Interaction logic for AddShowTimeView.xaml
    /// </summary>
    public partial class AddShowTimeView : Window
    {
        public AddShowTimeView(string phong)
        {
            InitializeComponent();
            AddShowTimeViewModel addShowTimeViewModel = new AddShowTimeViewModel(phong, this);
            this.DataContext = addShowTimeViewModel;
        }
    }
}
