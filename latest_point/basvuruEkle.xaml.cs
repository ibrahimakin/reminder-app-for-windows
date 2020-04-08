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

            if (isim == "")
            {
                changeTextAsync("İsim boş olamaz.");
                return;
            }

            try
            {
                Database.TableEtkinlik.AddToTable(isim, kayit, son, link, sonuc);
                changeTextAsync("Kaydedildi.");
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                if (!Database.DatabaseOperations.getState())
                {
                    changeTextAsync("Veritabanı bağlanamadı.");
                }
                else { changeTextAsync("Kayıt mevcut."); }
            }
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
