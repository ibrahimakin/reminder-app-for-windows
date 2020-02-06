using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Diagnostics;

namespace latest_point
{
    /// <summary>
    /// Interaction logic for basvuruPage.xaml
    /// </summary>
    public partial class basvuruPage : UserControl
    {
        List<basvuru> basvurus = new List<basvuru>();
        Button tiklanan;
        public basvuruPage()
        {
            InitializeComponent();
        }
        public void Listele()
        {
            SQLiteConnection conn = new SQLiteConnection(DBconnection.DBpath);
            conn.Open();
            SQLiteCommand command = new SQLiteCommand("select * from Table_Basvuru", conn);
            try
            {
                SQLiteDataReader rdr = command.ExecuteReader();
                int i = 0;
                int id = 0;
                string b;
                while (rdr.Read())
                {
                    id = Convert.ToInt32(rdr["id"]);
                    basvuru item = new basvuru(id, rdr["isim"].ToString(), rdr["kayit"].ToString(), rdr["son"].ToString(), rdr["link"].ToString(), rdr["sonuc"].ToString(), rdr["baslangic"].ToString(), rdr["degisim"].ToString());
                    basvurus.Add(item);

                    Button YeniButon = new Button();
                    YeniButon.Content = item.Isim;
                    YeniButon.Margin = new Thickness(0, 5, 0, 0);
                    b = i.ToString();
                    YeniButon.Tag = b;
                    YeniButon.Click += new RoutedEventHandler(YeniButon_Click);
                    butonlar.Children.Add(YeniButon);

                    i++;
                }
            }
            catch (Exception /*e*/)
            {
                MessageBox.Show("Veritabanı bağlanamadı.");
                //MessageBox.Show(e.ToString());
            }
            finally
            {
                conn.Dispose();
            }
        }

        private void YeniButon_Click(object sender, RoutedEventArgs e)
        {
            if (sender == tiklanan) { return; }
            isimText.Visibility = Visibility.Visible;
            isimEdit.Visibility = Visibility.Visible;
            kayitText.Visibility = Visibility.Visible;
            kayitEdit.Visibility = Visibility.Visible;
            sonText.Visibility = Visibility.Visible;            
            sonEdit.Visibility = Visibility.Visible;
            linkText.Visibility = Visibility.Visible;
            linkEdit.Visibility = Visibility.Visible;
            sonucText.Visibility = Visibility.Visible;
            sonucEdit.Visibility = Visibility.Visible;
            baslangicText.Visibility = Visibility.Visible;
            degisimText.Visibility = Visibility.Visible;     
            videoSilBtn.Visibility = Visibility.Visible;
            tiklanan = (Button)sender;
            int i = Convert.ToInt16(tiklanan.Tag);
            isim.Text = basvurus[i].Isim;
            kayit.Text = basvurus[i].Kayit;
            son.Text = basvurus[i].Son;
            string adres = basvurus[i].Link;
            link.Text = adres;
            try
            {
                linkHyper.NavigateUri = new Uri(adres);
                linkHyper.IsEnabled = true;
            }
            catch (Exception)
            {
                try
                {
                    linkHyper.NavigateUri = new Uri("http://"+adres);
                    linkHyper.IsEnabled = true;
                }
                catch(Exception) {
                    linkHyper.IsEnabled = false;
                }
            }            
            sonuc.Text = basvurus[i].Sonuc;
            baslangic.Text = basvurus[i].Baslangic;
            degisim.Text = basvurus[i].Degisim;
            Iptal_Click(sender, e);
        }

        private void BasvuruPage_Loaded(object sender, RoutedEventArgs e)
        {
            Listele();
        }

        private void KayitEdit_Click(object sender, RoutedEventArgs e)
        {
            if (kayitEdit.Content.ToString() == " > ")
            {
                kayitEdit.Content = " < ";
                kayitEditText.Visibility = Visibility.Visible;
                kayitKaydet.Visibility = Visibility.Visible;
            }
            else
            {
                kayitEdit.Content = " > ";
                kayitEditText.Visibility = Visibility.Hidden;
                kayitKaydet.Visibility = Visibility.Hidden;
            }
        }

        private void SonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sonEdit.Content.ToString() == " > ")
            {
                sonEdit.Content = " < ";
                sonEditText.Visibility = Visibility.Visible;
                sonKaydet.Visibility = Visibility.Visible;
            }
            else
            {
                sonEdit.Content = " > ";
                sonEditText.Visibility = Visibility.Hidden;
                sonKaydet.Visibility = Visibility.Hidden;
            }
        }

        private void KayitKaydet_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = basvurus[index].Id.ToString();

            SQLiteConnection conn = new SQLiteConnection(DBconnection.DBpath);
            conn.Open();
            SQLiteCommand command = new SQLiteCommand("update Table_Basvuru set kayit = '" + kayitEditText.Text + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            basvurus[index].Kayit = kayitEditText.Text;

            kayit.Text = kayitEditText.Text;
            conn.Dispose();
            degisimGuncelle(index);
        }

        private void SonKaydet_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = basvurus[index].Id.ToString();
            SQLiteConnection conn = new SQLiteConnection(DBconnection.DBpath);
            conn.Open();
            SQLiteCommand command = new SQLiteCommand("update Table_Basvuru set son = '" + sonEditText.Text + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            basvurus[index].Son = sonEditText.Text;

            son.Text = sonEditText.Text;
            conn.Dispose();
            degisimGuncelle(index);
        }

        void degisimGuncelle(int index)
        {
            string id = basvurus[index].Id.ToString();
            SQLiteConnection conn = new SQLiteConnection(DBconnection.DBpath);
            conn.Open();
            string now = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            degisim.Text = now;
            SQLiteCommand command = new SQLiteCommand("update Table_Basvuru set degisim = '" + degisim.Text + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            basvurus[index].Degisim = now;
            conn.Dispose();
        }

        private void EditPreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            var textBox = sender as TextBox;
            if (textBox.GetLineLength(0) < 1 || textBox.Text.Contains(','))
            {
                e.Handled = Regex.IsMatch(e.Text, "[^0-9]");
                return;
            }
            e.Handled = Regex.IsMatch(e.Text, "[^0-9,0-9]");

        }

        private void EditPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void VideoSilBtn_Click(object sender, RoutedEventArgs e)
        {
            videoSilBtn.Visibility = Visibility.Hidden;
            silDurum.Text = (Convert.ToInt16(tiklanan.Tag) + 1) + ". kayıt silinsin mi?";
            onay.Visibility = Visibility.Visible;
            iptal.Visibility = Visibility.Visible;


        }

        private void Onay_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = basvurus[index].Id.ToString();
            SQLiteConnection conn = new SQLiteConnection(DBconnection.DBpath);
            conn.Open();
            SQLiteCommand command = new SQLiteCommand("delete from Table_Basvuru where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            conn.Dispose();
            basvurus.RemoveAt(index);

            changeTextAsync();

            butonlar.Children.Clear();
            bilgiTemizle();
            Listele();
        }

        private void Iptal_Click(object sender, RoutedEventArgs e)
        {
            silDurum.Text = "";
            onay.Visibility = Visibility.Hidden;
            iptal.Visibility = Visibility.Hidden;
            videoSilBtn.Visibility = Visibility.Visible;
        }

        private void bilgiTemizle()
        {
            isimText.Visibility = Visibility.Hidden;
            kayitText.Visibility = Visibility.Hidden;
            sonText.Visibility = Visibility.Hidden;
            linkText.Visibility = Visibility.Hidden;
            sonucText.Visibility = Visibility.Hidden;
            baslangicText.Visibility = Visibility.Hidden;
            degisimText.Visibility = Visibility.Hidden;

            isim.Text = "";
            isimEdit.Content = " > ";
            isimEdit.Visibility = Visibility.Hidden;
            isimEditText.Visibility = Visibility.Hidden;
            isimKaydet.Visibility = Visibility.Hidden;

            kayit.Text = "";
            kayitEdit.Content = " > ";
            kayitEdit.Visibility = Visibility.Hidden;
            kayitEditText.Visibility = Visibility.Hidden;
            kayitKaydet.Visibility = Visibility.Hidden;

            son.Text = "";
            sonEdit.Content = " > ";
            sonEdit.Visibility = Visibility.Hidden;
            sonEditText.Visibility = Visibility.Hidden;
            sonKaydet.Visibility = Visibility.Hidden;

            link.Text = "";
            linkEdit.Content = " > ";
            linkEdit.Visibility = Visibility.Hidden;
            linkEditText.Visibility = Visibility.Hidden;
            linkKaydet.Visibility = Visibility.Hidden;

            sonuc.Text = "";
            sonucEdit.Content = " > ";
            sonucEdit.Visibility = Visibility.Hidden;
            sonucEditText.Visibility = Visibility.Hidden;
            sonucKaydet.Visibility = Visibility.Hidden;

            baslangic.Text = "";
            degisim.Text = "";

            //silDurum.Text = "";
            onay.Visibility = Visibility.Hidden;
            iptal.Visibility = Visibility.Hidden;
            videoSilBtn.Visibility = Visibility.Hidden;
        }

        private async Task changeTextAsync()
        {
            silDurum.Text = "Silindi.";
            await Task.Delay(2000);
            silDurum.Text = "";
        }

        private void linkEdit_Click(object sender, RoutedEventArgs e)
        {
            if (linkEdit.Content.ToString() == " > ")
            {
                linkEdit.Content = " < ";
                linkEditText.Visibility = Visibility.Visible;
                linkKaydet.Visibility = Visibility.Visible;
            }
            else
            {
                linkEdit.Content = " > ";
                linkEditText.Visibility = Visibility.Hidden;
                linkKaydet.Visibility = Visibility.Hidden;
            }
        }

        private void linkKaydet_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = basvurus[index].Id.ToString();

            SQLiteConnection conn = new SQLiteConnection(DBconnection.DBpath);
            conn.Open();
            SQLiteCommand command = new SQLiteCommand("update Table_Basvuru set link = '" + linkEditText.Text + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            basvurus[index].Link = linkEditText.Text;

            link.Text = linkEditText.Text;
            conn.Dispose();
            degisimGuncelle(index);
        }

        private void sonucKaydet_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = basvurus[index].Id.ToString();

            SQLiteConnection conn = new SQLiteConnection(DBconnection.DBpath);
            conn.Open();
            SQLiteCommand command = new SQLiteCommand("update Table_Basvuru set sonuc = '" + sonucEditText.Text + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            basvurus[index].Sonuc = sonucEditText.Text;

            sonuc.Text = sonucEditText.Text;
            conn.Dispose();
            degisimGuncelle(index);
        }

        private void sonucEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sonucEdit.Content.ToString() == " > ")
            {
                sonucEdit.Content = " < ";
                sonucEditText.Visibility = Visibility.Visible;
                sonucKaydet.Visibility = Visibility.Visible;
            }
            else
            {
                sonucEdit.Content = " > ";
                sonucEditText.Visibility = Visibility.Hidden;
                sonucKaydet.Visibility = Visibility.Hidden;
            }
        }

        private void linkHyper_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void isimKaydet_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = basvurus[index].Id.ToString();            

            SQLiteConnection conn = new SQLiteConnection(DBconnection.DBpath);
            conn.Open();
            
            string yeni = isimEditText.Text;
            
            SQLiteCommand command = new SQLiteCommand("update Table_Basvuru set isim = '" + yeni + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();

            basvurus[index].Isim = yeni;
            isim.Text = yeni;
            tiklanan.Content = yeni;

            conn.Dispose();
            degisimGuncelle(index);
        }

        private void isimEdit_Click(object sender, RoutedEventArgs e)
        {
            if (isimEdit.Content.ToString() == " > ")
            {
                isimEdit.Content = " < ";
                isimEditText.Visibility = Visibility.Visible;
                isimKaydet.Visibility = Visibility.Visible;
            }
            else
            {
                isimEdit.Content = " > ";
                isimEditText.Visibility = Visibility.Hidden;
                isimKaydet.Visibility = Visibility.Hidden;
            }
        }

        private void IsimPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (isimEditText.GetLineLength(0) < 1)
            {
                e.Handled = e.Key == Key.Space;
            }
        }
        private void LinkPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;            
        }
    }
}
