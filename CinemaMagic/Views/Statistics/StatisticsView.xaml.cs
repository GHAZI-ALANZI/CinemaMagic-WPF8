using CinemaMagic.ViewModels.StatisticsVM;
using System.Windows.Controls;

namespace CinemaMagic.Views.Statistics
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : UserControl
    {
        public StatisticsView()
        {
            StatisticsViewModel vm = new();
            InitializeComponent();
            DataContext = vm;
        }
    }
}
