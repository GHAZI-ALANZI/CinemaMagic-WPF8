using CinemaMagic.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace CinemaMagic.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public bool isClosed;
        public LoginView()
        {
            isClosed = false;
            InitializeComponent();
            LoginViewModel loginViewModel = new LoginViewModel(this);
            this.DataContext = loginViewModel;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            isClosed = true;
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }



    }
}
