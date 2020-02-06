using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace latest_point
{
    class video
    {
        int id;
        string isim;
        string kacinci;
        string dakika;
        string baslangic;
        string degisim;

        public video(int id, string isim, string kacinci, string dakika, string baslangic, string degisim)
        {
            this.id = id;
            this.isim = isim;
            this.kacinci = kacinci;
            this.dakika = dakika;
            this.baslangic = baslangic;
            this.degisim = degisim;
        }

        public int Id { get => id; set => id = value; }
        public string Isim { get => isim; set => isim = value; }
        public string Kacinci { get => kacinci; set => kacinci = value; }
        public string Dakika { get => dakika; set => dakika = value; }
        public string Baslangic { get => baslangic; set => baslangic = value; }
        public string Degisim { get => degisim; set => degisim = value; }
    }
}
