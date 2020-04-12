using System;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace latest_point
{
    /// <summary>
    /// Interaction logic for kitapEkle.xaml
    /// </summary>
    public partial class kitapEkle : UserControl
    {
        public kitapEkle()
        {
            InitializeComponent();
        }

        private void KitapEkleBtn_Click(object sender, RoutedEventArgs e)
        {
            string isim = isim_tb.Text;
            string sayfa = sayfa_tb.Text;
            string now = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            SQLiteConnection conn = Database.DatabaseOperations.getConn();
            if (isim == "")
            {
                changeTextAsync("İsim boş olamaz.");
                return;
            }
            SQLiteCommand command = new SQLiteCommand("insert into Table_Kitap (isim, sayfa, baslangic, degisim) values ('" + isim + "', '" + sayfa + "', '"  + now + "', '" + now + "');", conn);

            try
            {
                command.ExecuteNonQuery();
                changeTextAsync("Kaydedildi.");
                //ekleDurum.Text = "Kaydedildi.";
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                if (!Database.DatabaseOperations.GetState())
                {
                    changeTextAsync("Veritabanı bağlanamadı.");
                }
                else { changeTextAsync("Kayıt mevcut."); }
                //ekleDurum.Text = "Kayıt mevcut.";
            }

        }

        private void EklePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void EklePreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void IsimPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (isim_tb.GetLineLength(0) < 1)
            {
                e.Handled = e.Key == Key.Space;
            }
        }

        private async Task changeTextAsync(string durum)
        {
            ekleDurum.Text = durum;
            await Task.Delay(2000);
            ekleDurum.Text = "";
        }

        private void LinkPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }
    }
}
