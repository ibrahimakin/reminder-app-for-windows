using System;

namespace latest_point.Database
{
    class TableEtkinlik
    {
        public static void AddToTable(string isim, string kayit, string son, string link, string sonuc)
        {
            string now = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string commandText = "insert into Table_Basvuru (isim, kayit, son, link, sonuc, baslangic, degisim) values ('" + isim + "', '" + kayit + "', '" + son + "', '" + link + "', '" + sonuc + "', '" + now + "', '" + now + "');";
            DatabaseOperations.addToTable(commandText);
        }

        public static void deleteFromTable()
        {

        }
    }
}
