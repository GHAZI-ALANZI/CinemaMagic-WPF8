﻿using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CinemaMagic.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        LoginView loginView;
        public MainView(int Staff_Id, LoginView loginView)
        {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel(Staff_Id);
            this.DataContext = mainViewModel;
            this.loginView = loginView;
            StaffDA staffDA = new StaffDA();
            if (!staffDA.checkQuanLy(Staff_Id))
            {
                btnQLNhanSu.Visibility = Visibility.Collapsed;
            }
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.blurPanel.Opacity = 0.2;
            this.blurPanel.Visibility = Visibility.Visible;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.blurPanel.Visibility = Visibility.Hidden;
            this.blurPanel.Opacity = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            btnQLPhim.Background = Brushes.Transparent;
            btnQLSuatChieu.Background = Brushes.Transparent;
            btnQLKhachHang.Background = Brushes.Transparent;
            btnQLNhanSu.Background = Brushes.Transparent;
            btnQLSanPham.Background = Brushes.Transparent;
            btnVoucher.Background = Brushes.Transparent;
            btnCaNhan.Background = Brushes.Transparent;
            btnSuCo.Background = Brushes.Transparent;
            btnThongKe.Background = Brushes.Transparent;
            Button button = (Button)sender;
            button.Background = new SolidColorBrush(Color.FromRgb(0, 11, 207));
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
            loginView.Show();
        }
    }
}
