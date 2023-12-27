using System.Windows;

namespace CinemaMagic.Views.MessageBox
{
    /// <summary>
    /// Interaction logic for YesMessageBox.xaml
    /// </summary>
    public partial class YesMessageBox : Window
    {
        public YesMessageBox(string Title, string Message)
        {
            InitializeComponent();
            txtChuDe.Text = Title;
            txtMessage.Text = Message;
            System.Media.SystemSounds.Hand.Play();
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
