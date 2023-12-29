using CinemaMagic.Models.DTOs;
using CinemaMagic.ViewModels.ErrorManagementVM;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CinemaMagic.Views.ErrorManagement
{
    /// <summary>
    /// Interaction logic for ErrorManagementView.xaml
    /// </summary>
    public partial class ErrorManagementView : UserControl
    {
        private bool lvLoaded;
        public ErrorManagementView(int Staff_Id)
        {
            ErrorManagementViewModel vm = new(Staff_Id);
            InitializeComponent();
            DataContext = vm;
            lvLoaded = false;
        }
        private bool ErrorFilter(object item)
        {
            ErrorDTO error = item as ErrorDTO;
            string filter = (cbBoxFilter.SelectedItem as ComboBoxItem).Content.ToString();
            if (filter == "Entire") return true;
            if (filter == "Pending receipt")
            {
                return error.Status == "Pending receipt";
            }
            else
            if (filter == "In progress")
            {
                return error.Status == "In progress";
            }
            else
            if (filter == "In progress")
            {
                return error.Status == "In progress";
            }
            return error.Status == "Cancelled";

        }
        private void cbBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!lvLoaded)
            {
                return;
            }

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvError.ItemsSource);
            view.Filter = ErrorFilter;
            CollectionViewSource.GetDefaultView(lvError.ItemsSource).Refresh();
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            lvLoaded = true;
        }
    }
}
