using System;
using System.Linq;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Input;


namespace latest_point
{
    /// <summary>
    /// Interaction logic for basvuruEkle.xaml
    /// </summary>
    public partial class basvuruEkle : UserControl
    {
        public basvuruEkle()
        {
            InitializeComponent();
        }
        private void VideoEkleBtn_Click(object sender, RoutedEventArgs e)
        {
            string isim = isim_tb.Text;
            string kayit = kayit_tb.Text;
            string son = son_tb.Text;
            string link = link_tb.Text;
            string sonuc = sonuc_tb.Text;
            string now = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            SQLiteConnection conn = new SQLiteConnection(DBconnection.DBpath);
            conn.Open();
            if (isim == "")
            {
                changeTextAsync("İsim boş olamaz.");
                return;
            }
            SQLiteCommand command = new SQLiteCommand("insert into Table_Basvuru (isim, kayit, son, link, sonuc, baslangic, degisim) values ('" + isim + "', '" + kayit + "', '" + son + "', '" + link + "', '" + sonuc + "', '" + now + "', '" + now + "');", conn);

            try
            {
                command.ExecuteNonQuery();
                changeTextAsync("Kaydedildi.");
                //ekleDurum.Text = "Kaydedildi.";
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                if (!DBconnection.durum)
                {
                    changeTextAsync("Veritabanı bağlanamadı.");
                }
                else { changeTextAsync("Kayıt mevcut."); }
                //ekleDurum.Text = "Kayıt mevcut.";
            }

            conn.Dispose();
        }

        private void EklePreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            var textBox = sender as TextBox;
            if (textBox.GetLineLength(0) < 1 || textBox.Text.Contains(','))
            {
                e.Handled = Regex.IsMatch(e.Text, "[^0-9]");
                return;
            }
            e.Handled = Regex.IsMatch(e.Text, "[^0-9,0-9]");

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
        
        private void LinkPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private async Task changeTextAsync(string durum)
        {
            ekleDurum.Text = durum;
            await Task.Delay(2000);
            ekleDurum.Text = "";
        }
    }

}
