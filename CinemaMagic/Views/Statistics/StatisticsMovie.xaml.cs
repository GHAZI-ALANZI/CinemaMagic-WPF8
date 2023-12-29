using System.Windows;
using System.Windows.Controls;

namespace CinemaMagic.Views.Statistics
{
    /// <summary>
    /// Interaction logic for StatisticsMovie.xaml
    /// </summary>
    public partial class StatisticsMovie : UserControl
    {
        public StatisticsMovie()
        {
            InitializeComponent();
        }
        private void cbBoxChuKy_Loaded(object sender, RoutedEventArgs e)
        {
            cbBoxChuKy.SelectedIndex = 0;
            //GetMonthSource(cbBoxThoiDiem);
        }

        private void cbBoxChuKy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBoxChuKy.SelectedItem == null) return;
            ComboBoxItem s = (ComboBoxItem)cbBoxChuKy.SelectedItem;
            switch (s.Content.ToString())
            {
                case "By year":
                    {
                        GetYearSource(cbBoxThoiDiem);
                        return;
                    }
                case "By month":
                    {
                        GetMonthSource(cbBoxThoiDiem);
                        return;
                    }
            }
        }

        public void GetYearSource(ComboBox cbb)
        {
            if (cbb is null) return;

            List<string> l = new List<string>();

            int now = -1;
            for (int i = 2023; i <= System.DateTime.Now.Year; i++)
            {
                now++;
                l.Add(i.ToString());
            }
            cbb.ItemsSource = l;
            cbb.SelectedIndex = now;
        }
        public void GetMonthSource(ComboBox cbb)
        {
            if (cbb is null) return;

            List<string> l = new List<string>();

            l.Add("Month 1");
            l.Add("Month 2");
            l.Add("Month 3");
            l.Add("Month 4");
            l.Add("Month 5");
            l.Add("Month 6");
            l.Add("Month 7");
            l.Add("Month 8");
            l.Add("Month 9");
            l.Add("Month 10");
            l.Add("Month 11");
            l.Add("Month 12");

            cbb.ItemsSource = l;
            cbb.SelectedIndex = DateTime.Today.Month - 1;
        }
    }
}
