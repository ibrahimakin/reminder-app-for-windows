using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace latest_point
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Ekle : UserControl
    {
        int eklePadeID;
        public Ekle()
        {
            InitializeComponent();
            
            eklePadeID = 0;
            if (currentPage.id == 1)
            {
                checkKitap.IsChecked = true ;
                CheckKitap_Checked(new object(), new RoutedEventArgs());
            }
            else if (currentPage.id == 3)
            {
                checkBasvuru.IsChecked = true;
                CheckBasvuru_Checked(new object(), new RoutedEventArgs());
            }
            currentPage.id = -1;
        }

        /*
        private void SecEkle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        */

        private void CheckKitap_Checked(object sender, RoutedEventArgs e)
        {
            if (eklePadeID == 1) { return; }
            yeniEkle.Content = new kitapEkle();
            eklePadeID = 1;
        }

        private void CheckVideo_Checked(object sender, RoutedEventArgs e)
        {
            if (eklePadeID == 2) { return; }
            yeniEkle.Content = new videoEkle();
            eklePadeID = 2;
        }

        private void CheckBasvuru_Checked(object sender, RoutedEventArgs e)
        {
            if (eklePadeID == 3) { return; }
            yeniEkle.Content = new basvuruEkle();
            eklePadeID = 3;
        }
    }
}
