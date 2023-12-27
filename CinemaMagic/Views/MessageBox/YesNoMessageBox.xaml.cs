using System.Windows;

namespace CinemaMagic.Views.MessageBox
{
    /// <summary>
    /// Interaction logic for YesNoMessageBox.xaml
    /// </summary>
    public partial class YesNoMessageBox : Window
    {
        public YesNoMessageBox(string Title, string Message)
        {
            InitializeComponent();
            txtMessage.Text = Message;
            txtChuDe.Text = Title;
            System.Media.SystemSounds.Hand.Play();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
