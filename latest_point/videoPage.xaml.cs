using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace latest_point
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class videoPage : Page
    {
        List<video> videos = new List<video>();
        Button tiklanan;

        public videoPage()
        {
            InitializeComponent();
        }

        public void Listele()
        {
            SQLiteConnection conn = Database.DatabaseOperations.getConn();
            SQLiteCommand command = new SQLiteCommand("select * from Table_Video",conn);
            try
            {
                SQLiteDataReader rdr = command.ExecuteReader();
                int i = 0;
                int id = 0;
                string isim = " ";
                int bitti = 0;
                while (rdr.Read())
                {
                    id = Convert.ToInt32(rdr["id"]);
                    isim = rdr["isim"].ToString();
                    try
                    {
                        bitti = Convert.ToInt32(rdr["bitti"]);
                    }
                    catch (Exception){ }

                    video item = new video(id, isim, rdr["kacinci"].ToString(), rdr["dakika"].ToString(), rdr["link"].ToString(), bitti,rdr["baslangic"].ToString(), rdr["degisim"].ToString());
                    videos.Add(item);

                    Button YeniButon = GenerateButton(isim, bitti);

                    YeniButon.Tag = i.ToString();
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
            
        }

        private void YeniButon_Click(object sender, RoutedEventArgs e)
        {
            if (sender == tiklanan) { return; }
            isimText.Visibility = Visibility.Visible;
            isimEdit.Visibility = Visibility.Visible;

            bittiEdit.Visibility = Visibility.Visible;

            kacinciText.Visibility = Visibility.Visible;
            kacinciEdit.Visibility = Visibility.Visible;

            dakikaText.Visibility = Visibility.Visible;
            dakikaEdit.Visibility = Visibility.Visible;

            baslangicText.Visibility = Visibility.Visible;
            degisimText.Visibility = Visibility.Visible;
            videoSilBtn.Visibility = Visibility.Visible;
            tiklanan = (Button)sender;
            int i = Convert.ToInt16(tiklanan.Tag);
            isim.Text = videos[i].Isim;
            bitti.Text = videos[i].bittiToString();
            kacinci.Text = videos[i].Kacinci;
            dakika.Text = videos[i].Dakika;
            baslangic.Text = videos[i].Baslangic;
            degisim.Text = videos[i].Degisim;
            Iptal_Click(sender, e);
        }

        private void VideoPage_Loaded(object sender, RoutedEventArgs e)
        {
            Listele();
        }

        private void KacinciEdit_Click(object sender, RoutedEventArgs e)
        {
            if(kacinciEdit.Content.ToString() == " > ")
            {
                kacinciEdit.Content = " < ";
                kacinciEditText.Visibility = Visibility.Visible;
                kacinciKaydet.Visibility = Visibility.Visible;
            }
            else
            {
                kacinciEdit.Content = " > ";
                kacinciEditText.Visibility = Visibility.Hidden;
                kacinciKaydet.Visibility = Visibility.Hidden;
            }
        }

        private void DakikaEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dakikaEdit.Content.ToString() == " > ")
            {
                dakikaEdit.Content = " < ";
                dakikaEditText.Visibility = Visibility.Visible;
                dakikaKaydet.Visibility = Visibility.Visible;
            }
            else
            {
                dakikaEdit.Content = " > ";
                dakikaEditText.Visibility = Visibility.Hidden;
                dakikaKaydet.Visibility = Visibility.Hidden;
            }
        }

        private void KacinciKaydet_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = videos[index].Id.ToString();

            SQLiteConnection conn = Database.DatabaseOperations.getConn();
            
            SQLiteCommand command = new SQLiteCommand("update Table_Video set kacinci = '" + kacinciEditText.Text + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            videos[index].Kacinci = kacinciEditText.Text;

            kacinci.Text = kacinciEditText.Text;
            degisimGuncelle(index);
        }

        private void DakikaKaydet_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = videos[index].Id.ToString();
            SQLiteConnection conn = Database.DatabaseOperations.getConn();
            SQLiteCommand command = new SQLiteCommand("update Table_Video set dakika = '" + dakikaEditText.Text + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            videos[index].Dakika = dakikaEditText.Text;
            
            dakika.Text = dakikaEditText.Text;
            degisimGuncelle(index);
        }

        void degisimGuncelle(int index)
        {
            string id = videos[index].Id.ToString();
            SQLiteConnection conn = Database.DatabaseOperations.getConn();
            string now = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            degisim.Text = now;
            SQLiteCommand command = new SQLiteCommand("update Table_Video set degisim = '" + degisim.Text + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            videos[index].Degisim = now;
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
            silDurum.Text = (Convert.ToInt16(tiklanan.Tag)+1) + ". kayıt silinsin mi?";
            onay.Visibility = Visibility.Visible;
            iptal.Visibility = Visibility.Visible;
        }

        private void Onay_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = videos[index].Id.ToString();
            SQLiteConnection conn = Database.DatabaseOperations.getConn();
            SQLiteCommand command = new SQLiteCommand("delete from Table_Video where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            videos.RemoveAt(index);

            changeTextAsync();

            onay.Visibility = Visibility.Hidden;
            iptal.Visibility = Visibility.Hidden;
            videoSilBtn.Visibility = Visibility.Visible;
            //silDurum.Text = "";
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
            kacinciText.Visibility = Visibility.Hidden;
            dakikaText.Visibility = Visibility.Hidden;
            baslangicText.Visibility = Visibility.Hidden;
            degisimText.Visibility = Visibility.Hidden;

            isim.Text = "";
            isimEdit.Content = " > ";
            isimEditText.Visibility = Visibility.Hidden;
            isimKaydet.Visibility = Visibility.Hidden;

            kacinci.Text = "";
            kacinciEdit.Content = " > ";
            kacinciEdit.Visibility = Visibility.Hidden;
            kacinciEditText.Visibility = Visibility.Hidden;
            kacinciKaydet.Visibility = Visibility.Hidden;

            dakika.Text = "";
            dakikaEdit.Content = " > ";
            dakikaEdit.Visibility = Visibility.Hidden;
            dakikaEditText.Visibility = Visibility.Hidden;
            dakikaKaydet.Visibility = Visibility.Hidden;

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

        private void isimKaydet_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = videos[index].Id.ToString();

            SQLiteConnection conn = Database.DatabaseOperations.getConn();

            string yeni = kacinciEditText.Text;
            
            SQLiteCommand command = new SQLiteCommand("update Table_Video set isim = '" + yeni + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();

            videos[index].Isim = yeni;
            kacinci.Text = yeni;
            tiklanan.Content = yeni;

            degisimGuncelle(index);
        }

        private void isimEdit_Click(object sender, RoutedEventArgs e)
        {
            if (isimEdit.Content.ToString() == " > ")
            {
                isimEdit.Content = " < ";
                isimEditText.Visibility = Visibility.Visible;
                isimEditText.Width = 120;
                isimKaydet.Visibility = Visibility.Visible;
            }
            else
            {
                isimEdit.Content = " > ";
                isimEditText.Visibility = Visibility.Hidden;
                isimEditText.Width = 0;
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

        private void linkEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LinkPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void linkKaydet_Click(object sender, RoutedEventArgs e)
        {

        }
        private void linkHyper_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private Button GenerateButton(string isim, int done)
        {
            Button yeniButon = new Button();
            Grid yeniGrid = new Grid();
            ColumnDefinition column0 = new ColumnDefinition();
            ColumnDefinition column1 = new ColumnDefinition();
            column0.Width = new GridLength(btnsWidth.Width.Value - 15);

            yeniGrid.ColumnDefinitions.Add(column0);
            yeniGrid.ColumnDefinitions.Add(column1);

            TextBlock butonIsim = new TextBlock();
            butonIsim.Text = isim;

            TextBlock bitti = new TextBlock();
            if (done == 1) { bitti.Text = "✓"; }

            butonIsim.HorizontalAlignment = HorizontalAlignment.Center;
            bitti.HorizontalAlignment = HorizontalAlignment.Right;

            Grid.SetColumn(bitti, 1);

            yeniGrid.Children.Add(butonIsim);
            yeniGrid.Children.Add(bitti);

            yeniButon.Content = yeniGrid;
            yeniButon.Margin = new Thickness(0, 5, 0, 0);

            return yeniButon;
        }

        private void bittiEdit_Click(object sender, RoutedEventArgs e)
        {
            if (bittiEdit.Content.ToString() == " > ")
            {
                bittiEdit.Content = " < ";
                bittiEditCB.Visibility = Visibility.Visible;
                bittiKaydet.Visibility = Visibility.Visible;
            }
            else
            {
                bittiEdit.Content = " > ";
                bittiEditCB.Visibility = Visibility.Hidden;
                bittiKaydet.Visibility = Visibility.Hidden;
            }
        }

        private void bittiKaydet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
