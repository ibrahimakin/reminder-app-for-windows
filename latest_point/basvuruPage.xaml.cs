﻿using System;
using System.Collections.Generic;
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

        private void BasvuruPage_Loaded(object sender, RoutedEventArgs e)
        {
            Listele();
        }

        public void Listele()
        {
            try
            {
                basvurus = Database.TableEtkinlik.GetItems();
                fillButtonList();
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
            bitti.Text = basvurus[i].bittiToString();
            kayit.Text = basvurus[i].Kayit;
            son.Text = basvurus[i].Son;
            string adres = basvurus[i].Link;
            link.Text = adres;
            sonuc.Text = basvurus[i].Sonuc;
            baslangic.Text = basvurus[i].Baslangic;
            degisim.Text = basvurus[i].Degisim;

            if (basvurus[i].Bitti == 1)
            {
                bittiEditCB.IsChecked = true;
            }
            else { bittiEditCB.IsChecked = false; }

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
            
            Iptal_Click(sender, e);
        }

        private void fillButtonList()
        {
            int i = 0;
            int bitti = 0;
            foreach (basvuru item in basvurus)
            {
                bitti = item.Bitti;
                Button YeniButon = GenerateButton(item.Isim, bitti);
                YeniButon.Tag = i.ToString();
                YeniButon.Click += new RoutedEventHandler(YeniButon_Click);
                if(bitti == 1)
                {
                    butonlar.Children.Add(YeniButon);
                }
                else
                {
                    bitmeyenButonlar.Children.Add(YeniButon);
                }
                i++;
            }
        }

        private Button GenerateButton(string isim, int done)
        {
            Button yeniButon = new Button();
            ChangeButton(yeniButon, isim, done);
            return yeniButon;
        }

        private void ChangeButton(Button buton, string isim, int done)
        {
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

            buton.Content = yeniGrid;
            buton.Margin = new Thickness(0, 5, 0, 0);
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

        private void isimKaydet_Click(object sender, RoutedEventArgs e)
        {
            string yeni = isimEditText.Text;

            if (yeni == "")
            {
                changeTextAsync("İsim boş olamaz.");
                return;
            }
            int index = Convert.ToInt16(tiklanan.Tag);
            if (yeni == basvurus[index].Isim)
            {
                changeTextAsync("İsim aynı.");
                return;
            }
            string id = basvurus[index].Id.ToString();

            Database.TableEtkinlik.UpdateRow(yeni, "isim", id);

            basvurus[index].Isim = yeni;
            isim.Text = yeni;
            tiklanan.Content = yeni;

            degisimGuncelle(index);

            changeTextAsync("Değiştirildi.");
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

        private void KayitKaydet_Click(object sender, RoutedEventArgs e)
        {
            string yeni = kayitEditText.Text;
            int index = Convert.ToInt16(tiklanan.Tag);
            if (yeni == basvurus[index].Kayit)
            {
                changeTextAsync("Başvuru aynı.");
                return;
            }
            string id = basvurus[index].Id.ToString();

            Database.TableEtkinlik.UpdateRow(yeni, "kayit", id);

            basvurus[index].Kayit = yeni;

            kayit.Text = yeni;

            degisimGuncelle(index);
            changeTextAsync("Değiştirildi.");
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

        private void SonKaydet_Click(object sender, RoutedEventArgs e)
        {
            string yeni = sonEditText.Text;
            int index = Convert.ToInt16(tiklanan.Tag);
            if (yeni == basvurus[index].Son)
            {
                changeTextAsync("Son başvuru aynı.");
                return;
            }
            string id = basvurus[index].Id.ToString();

            Database.TableEtkinlik.UpdateRow(yeni, "son", id);

            basvurus[index].Son = yeni;
            son.Text = yeni;
            degisimGuncelle(index);

            changeTextAsync("Değiştirildi.");
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

        private void sonucKaydet_Click(object sender, RoutedEventArgs e)
        {
            string yeni = sonucEditText.Text;
            int index = Convert.ToInt16(tiklanan.Tag);
            if (yeni == basvurus[index].Sonuc)
            {
                changeTextAsync("Sonuc aynı.");
                return;
            }

            string id = basvurus[index].Id.ToString();

            Database.TableEtkinlik.UpdateRow(yeni, "sonuc", id);

            basvurus[index].Sonuc = yeni;
            sonuc.Text = yeni;
            degisimGuncelle(index);
            changeTextAsync("Değiştirildi.");
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
            string yeni = linkEditText.Text;
            int index = Convert.ToInt16(tiklanan.Tag);
            if (yeni == basvurus[index].Link)
            {
                changeTextAsync("Link aynı.");
                return;
            }
            string id = basvurus[index].Id.ToString();

            Database.TableEtkinlik.UpdateRow(yeni, "link", id);

            basvurus[index].Link = yeni;
            link.Text = yeni;
            degisimGuncelle(index);
            changeTextAsync("Değiştirildi.");
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
            int yeni;
            if (bittiEditCB.IsChecked == true) { yeni = 1; }
            else { yeni = 0; }
            int index = Convert.ToInt16(tiklanan.Tag);
            if (yeni == basvurus[index].Bitti)
            {
                return;
            }
            string id = basvurus[index].Id.ToString();

            Database.TableEtkinlik.UpdateRow(yeni.ToString(), "bitti", id);

            basvurus[index].Bitti = yeni;
            bitti.Text = basvurus[index].bittiToString();
            ChangeButton(tiklanan, basvurus[index].Isim, yeni);

            degisimGuncelle(index);

            changeTextAsync("Değiştirildi.");
        }

        void degisimGuncelle(int index)
        {
            string now = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            degisim.Text = now;
            basvurus[index].Degisim = now;
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
            Database.TableEtkinlik.DeleteFromTable(id);
            basvurus.RemoveAt(index);

            changeTextAsync("Silindi.");
            
            //butonlar.Children.Remove(tiklanan);  //index=tag bozulur.
            butonlar.Children.Clear();
            bitmeyenButonlar.Children.Clear();
            bilgiTemizle();
            fillButtonList();
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

            sonuc.Text = "";
            sonucEdit.Content = " > ";
            sonucEdit.Visibility = Visibility.Hidden;
            sonucEditText.Visibility = Visibility.Hidden;
            sonucKaydet.Visibility = Visibility.Hidden;

            link.Text = "";
            linkEdit.Content = " > ";
            linkEdit.Visibility = Visibility.Hidden;
            linkEditText.Visibility = Visibility.Hidden;
            linkKaydet.Visibility = Visibility.Hidden;

            bitti.Text = "";
            bittiEdit.Content = " > ";
            bittiEdit.Visibility = Visibility.Hidden;
            bittiEditCB.IsChecked = false;
            bittiEditCB.Visibility = Visibility.Hidden;
            bittiKaydet.Visibility = Visibility.Hidden;

            baslangic.Text = "";
            degisim.Text = "";

            //silDurum.Text = "";
            onay.Visibility = Visibility.Hidden;
            iptal.Visibility = Visibility.Hidden;
            videoSilBtn.Visibility = Visibility.Hidden;
        }

        private async Task changeTextAsync(string durum)
        {
            silDurum.Text = durum;
            await Task.Delay(2000);
            silDurum.Text = "";
        }

        

        

        private void linkHyper_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
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
