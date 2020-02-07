using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latest_point
{
    public class kitap
    {
        int id;
        string isim;
        int sayfa;
        string link;
        int bitti;
        string baslangic;
        string degisim;

        public kitap(int id, string isim, int sayfa, string link, int bitti, string baslangic, string degisim)
        {
            this.id = id;
            this.isim = isim;
            this.sayfa = sayfa;
            this.link = link;
            this.bitti = bitti;
            this.baslangic = baslangic;
            this.degisim = degisim;
        }

        public int Id { get => id; set => id = value; }
        public string Isim { get => isim; set => isim = value; }
        public int Sayfa { get => sayfa; set => sayfa = value; }
        public string Link { get => link; set => link = value; }
        public int Bitti { get => bitti; set => bitti = value; }
        public string Baslangic { get => baslangic; set => baslangic = value; }
        public string Degisim { get => degisim; set => degisim = value; }

        public string bittiToString()
        {
            if (bitti == 1) { return "✓"; }
            return "X";
        }
    }
}
