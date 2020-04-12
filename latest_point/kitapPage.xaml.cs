using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace latest_point
{
    /// <summary>
    /// Interaction logic for kitapPage.xaml
    /// </summary>
    public partial class kitapPage : Page
    {
        List<kitap> kitaps = new List<kitap>();
        Button tiklanan;

        public kitapPage()
        {
            InitializeComponent();
        }

        public void Listele()
        {
            SQLiteConnection conn = Database.DatabaseOperations.getConn();
            SQLiteCommand command = new SQLiteCommand("select * from Table_Kitap", conn);
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
                    catch (Exception) { }

                    kitap item = new kitap(id, isim, Convert.ToInt16(rdr["sayfa"]), rdr["link"].ToString(), bitti, rdr["baslangic"].ToString(), rdr["degisim"].ToString());
                    kitaps.Add(item);

                    Button YeniButon = GenerateButton(isim, bitti);
                    
                    YeniButon.Tag = i.ToString();
                    YeniButon.Click += new RoutedEventHandler(YeniButon_Click);
                    kitaplar.Children.Add(YeniButon);

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

            sayfaText.Visibility = Visibility.Visible;
            sayfaEdit.Visibility = Visibility.Visible;

            linkText.Visibility = Visibility.Visible;
            linkEdit.Visibility = Visibility.Visible;

            baslangicText.Visibility = Visibility.Visible;
            degisimText.Visibility = Visibility.Visible;
            
            kitapSilBtn.Visibility = Visibility.Visible;

            tiklanan = (Button)sender;
            int i = Convert.ToInt16(tiklanan.Tag);

            isim.Text = kitaps[i].Isim;
            bitti.Text = kitaps[i].bittiToString();
            sayfa.Text = kitaps[i].Sayfa.ToString();
            link.Text = kitaps[i].Link;
            baslangic.Text = kitaps[i].Baslangic;
            degisim.Text = kitaps[i].Degisim;

            Iptal_Click(sender, e);
        }

        private void KitapPage_Loaded(object sender, RoutedEventArgs e)
        {
            Listele();
        }

        private void SayfaEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sayfaEdit.Content.ToString() == " > ")
            {
                sayfaEdit.Content = " < ";
                sayfaEditText.Visibility = Visibility.Visible;
                sayfaKaydet.Visibility = Visibility.Visible;
            }
            else
            {
                sayfaEdit.Content = " > ";
                sayfaEditText.Visibility = Visibility.Hidden;
                sayfaKaydet.Visibility = Visibility.Hidden;
            }
        }

        private void SayfaKaydet_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = kitaps[index].Id.ToString();
            SQLiteConnection conn = Database.DatabaseOperations.getConn();
            SQLiteCommand command = new SQLiteCommand("update Table_Kitap set sayfa = '" + sayfaEditText.Text + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            kitaps[index].Sayfa = Convert.ToInt16(sayfaEditText.Text);

            sayfa.Text = sayfaEditText.Text;
            degisimGuncelle(index);
        }

        void degisimGuncelle(int index)
        {
            string id = kitaps[index].Id.ToString();
            SQLiteConnection conn = Database.DatabaseOperations.getConn();
            string now = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            degisim.Text = now;
            SQLiteCommand command = new SQLiteCommand("update Table_Kitap set degisim = '" + degisim.Text + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            kitaps[index].Degisim = now;
        }

        private void SayfaPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void EditPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void KitapSilBtn_Click(object sender, RoutedEventArgs e)
        {
            kitapSilBtn.Visibility = Visibility.Hidden;
            silDurum.Text = (Convert.ToInt16(tiklanan.Tag) + 1) + ". kayıt silinsin mi?";
            onay.Visibility = Visibility.Visible;
            iptal.Visibility = Visibility.Visible;
        }

        private void Onay_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = kitaps[index].Id.ToString();
            SQLiteConnection conn = Database.DatabaseOperations.getConn();
            SQLiteCommand command = new SQLiteCommand("delete from Table_Kitap where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();
            kitaps.RemoveAt(index);

            changeTextAsync();

            onay.Visibility = Visibility.Hidden;
            iptal.Visibility = Visibility.Hidden;
            kitapSilBtn.Visibility = Visibility.Visible;
            //silDurum.Text = "";
            kitaplar.Children.Clear();
            bilgiTemizle();
            Listele();
        }

        private void Iptal_Click(object sender, RoutedEventArgs e)
        {
            silDurum.Text = "";
            onay.Visibility = Visibility.Hidden;
            iptal.Visibility = Visibility.Hidden;
            kitapSilBtn.Visibility = Visibility.Visible;
        }

        private void bilgiTemizle()
        {
            isimText.Visibility = Visibility.Hidden;
            sayfaText.Visibility = Visibility.Hidden;
            baslangicText.Visibility = Visibility.Hidden;
            degisimText.Visibility = Visibility.Hidden;

            isim.Text = "";
            isimEdit.Content = " > ";
            isimEdit.Visibility = Visibility.Hidden;
            isimEditText.Visibility = Visibility.Hidden;
            isimKaydet.Visibility = Visibility.Hidden;

            sayfa.Text = "";
            sayfaEdit.Content = " > ";
            sayfaEdit.Visibility = Visibility.Hidden;
            sayfaEditText.Visibility = Visibility.Hidden;
            sayfaKaydet.Visibility = Visibility.Hidden;

            link.Text = "";
            linkEdit.Content = " > ";
            linkEdit.Visibility = Visibility.Hidden;
            linkEditText.Visibility = Visibility.Hidden;
            linkKaydet.Visibility = Visibility.Hidden;

            baslangic.Text = "";
            degisim.Text = "";

            //silDurum.Text = "";
            onay.Visibility = Visibility.Hidden;
            iptal.Visibility = Visibility.Hidden;
            kitapSilBtn.Visibility = Visibility.Hidden;
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
            string id = kitaps[index].Id.ToString();

            SQLiteConnection conn = Database.DatabaseOperations.getConn();

            string yeni = isimEditText.Text;

            SQLiteCommand command = new SQLiteCommand("update Table_Kitap set isim = '" + yeni + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();

            kitaps[index].Isim = yeni;
            isim.Text = yeni;
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

        private void LinkPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void linkKaydet_Click(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt16(tiklanan.Tag);
            string id = kitaps[index].Id.ToString();

            SQLiteConnection conn = Database.DatabaseOperations.getConn();

            string yeni = linkEditText.Text;

            SQLiteCommand command = new SQLiteCommand("update Table_Kitap set link = '" + yeni + "' where id = '" + id + "' ", conn);
            command.ExecuteNonQuery();

            kitaps[index].Link = yeni;
            link.Text = yeni;

            degisimGuncelle(index);
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
