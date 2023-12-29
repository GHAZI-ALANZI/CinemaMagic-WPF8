using CinemaMagic.ViewModels.InformationManagement;
using System.Windows.Controls;

namespace CinemaMagic.Views.InformationManagement
{
    /// <summary>
    /// Interaction logic for InformationView.xaml
    /// </summary>
    public partial class InformationView : UserControl
    {
        public InformationView(int Staff_Id)
        {
            InitializeComponent();
            InformationViewModel informationViewModel = new InformationViewModel(Staff_Id, this);
            this.DataContext = informationViewModel;
        }
    }
}
