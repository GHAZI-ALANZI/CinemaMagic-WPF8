using System.Windows;

namespace CinemaMagic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStart(object sender, EventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
        }
    }

}
