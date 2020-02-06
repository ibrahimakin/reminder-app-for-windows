using System.Windows;
using System.Windows.Controls;

namespace latest_point
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Ekle : UserControl
    {
        int addingPadeID;
        public Ekle()
        {
            InitializeComponent();
            
            addingPadeID = 0;
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

        private void CheckKitap_Checked(object sender, RoutedEventArgs e)
        {
            if (addingPadeID == 1) { return; }
            yeniEkle.Content = new kitapEkle();
            addingPadeID = 1;
        }

        private void CheckVideo_Checked(object sender, RoutedEventArgs e)
        {
            if (addingPadeID == 2) { return; }
            yeniEkle.Content = new videoEkle();
            addingPadeID = 2;
        }

        private void CheckBasvuru_Checked(object sender, RoutedEventArgs e)
        {
            if (addingPadeID == 3) { return; }
            yeniEkle.Content = new basvuruEkle();
            addingPadeID = 3;
        }
    }
}
