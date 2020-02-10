using System.Windows;

namespace latest_point
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DBconnection.ConnTest();
            ConnState.Content = DBconnection.ConnState;
            if (!DBconnection.durum)
            {
                kitapButon.IsEnabled = false;
                videoButon.IsEnabled = false;
            }
            //ConnState.Content = DBconnection.ConnState;
            // XML'de x:Name = "btnEkle"
            // btnEkle.Click += BtnEkle_Click;
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{            
        //}

        // SAYFA  ID
        // Ekle   -1
        // Main    0
        // Kitap   1
        // Video   2
        // Basvuru 3

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage.id == -1) { return; }
            eklePage.Content = new Ekle();            
        }
        private void BtnKitap_Click(object sender, RoutedEventArgs e)
        {
            if(currentPage.id == 1) { return; }
            eklePage.Content = new kitapPage();
            currentPage.id = 1;
        }

        private void BtnVideo_Click(object sender, RoutedEventArgs e)
        {
            if(currentPage.id == 2) { return; }
            eklePage.Content = new videoPage();
            currentPage.id = 2;
        }

        private void basvuruButon_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage.id == 3) { return; }
            eklePage.Content = new basvuruPage();
            currentPage.id = 3;
        }
    }
}
