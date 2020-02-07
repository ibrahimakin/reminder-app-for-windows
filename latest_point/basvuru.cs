using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latest_point
{
    class basvuru
    {
        int id;
        string isim;
        string kayit;
        string son;
        string sonuc;
        string link;
        int bitti;
        string baslangic;
        string degisim;


        public basvuru(int id, string isim, string baslangic, string degisim)
        {
            this.id = id;
            this.isim = isim;
            this.baslangic = baslangic;
            this.degisim = degisim;
        }

        public basvuru(int id, string isim, string kayit, string son, string sonuc, string link, int bitti, string baslangic, string degisim)
        {
            this.id = id;
            this.isim = isim;
            this.kayit = kayit;
            this.son = son;
            this.sonuc = sonuc;
            this.link = link;
            this.bitti = bitti;
            this.baslangic = baslangic;
            this.degisim = degisim;
        }

        public int Id { get => id; set => id = value; }
        public string Isim { get => isim; set => isim = value; }
        public string Kayit { get => kayit; set => kayit = value; }
        public string Son { get => son; set => son = value; }
        public string Sonuc { get => sonuc; set => sonuc = value; }
        public string Link { get => link; set => link = value; }
        public int Bitti { get => bitti; set => bitti = value; }
        public string Baslangic { get => baslangic; set => baslangic = value; }
        public string Degisim { get => degisim; set => degisim = value; }

        public string bittiToString()
        {
            if(bitti == 1){ return "✓"; }
            return "X";
        }
    }
}
