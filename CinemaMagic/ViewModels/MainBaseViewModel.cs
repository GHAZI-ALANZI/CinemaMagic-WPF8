using System.Windows.Input;

namespace CinemaMagic.ViewModels
{
    public abstract class MainBaseViewModel : BaseViewModel
    {
        private object currentView;
        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }

        public ICommand SwitchViewCommand { get; private set; }
        public MainBaseViewModel()
        {
            SwitchViewCommand = new ViewModelCommand(SwitchView);
        }

        private int Staff_Id;

        public MainBaseViewModel(int Staff_Id)
        {
            SwitchViewCommand = new ViewModelCommand(SwitchView);
            this.Staff_Id = Staff_Id;
            CurrentView = new InformationView(Staff_Id);
        }

        private void SwitchView(object userControlName)
        {
            string UserControlName = userControlName as string;
            switch (UserControlName)
            {
                case "Movies":
                    CurrentView = new MovieManagementView();
                    break;
                case "Error":
                    CurrentView = new ErrorManagementView(Staff_Id);
                    break;
                case "ShowTime":
                    CurrentView = new ShowTimeManagementView(Staff_Id);
                    break;
                case "Staff":
                    CurrentView = new StaffManagementView(Staff_Id);
                    break;
                case "QLSP":
                    CurrentView = new ProductManagementView();
                    break;
                case "Statistics":
                    CurrentView = new StatisticsView();
                    break;
                case "Customer":
                    CurrentView = new CustomerManagementView();
                    break;
                case "Vouchers":
                    CurrentView = new VoucherManagementView();
                    break;
                case "Personal":
                    CurrentView = new InformationView(Staff_Id);
                    break;
            }
        }
    }
}
