using System.Collections.Generic;

namespace latest_point
{
    class Tables
    {
        public List<basvuru> Table_Basvuru;
        public List<video> Table_Video;
        public List<kitap> Table_Kitap;

        public Tables(List<basvuru> Table_Basvuru, List<video> Table_Video, List<kitap> Table_Kitap)
        {
            this.Table_Basvuru = Table_Basvuru;
            this.Table_Video = Table_Video;
            this.Table_Kitap = Table_Kitap;
        }
    }
}
