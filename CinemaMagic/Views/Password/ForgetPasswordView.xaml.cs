using CinemaMagic.ViewModels.ForgotPassword;
using System.Windows;

namespace CinemaMagic.Views.Password
{
    /// <summary>
    /// Interaction logic for ForgetPasswordView.xaml
    /// </summary>
    public partial class ForgetPasswordView : Window
    {
        public ForgetPasswordView()
        {
            InitializeComponent();
            ForgotPasswordViewModel forgotPasswordViewModel = new ForgotPasswordViewModel(this);
            this.DataContext = forgotPasswordViewModel;
        }
    }
}
