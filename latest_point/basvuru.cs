namespace latest_point
{
    class basvuru
    {
        private int id, bitti, arsiv;
        private string isim, kayit, son, sonuc, link, baslangic, degisim, hash;

        public basvuru(int id, string isim, string kayit, string son, string sonuc, string link, int bitti, string baslangic, string degisim, int arsiv, string hash)
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
            this.arsiv = arsiv;
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
        public int Arsiv { get => arsiv; set => arsiv = value; }

        public string bittiToString()
        {
            if(bitti == 1){ return "✓"; }
            return "X";
        }
    }
}
