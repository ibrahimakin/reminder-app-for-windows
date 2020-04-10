using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace latest_point.Database
{
    class TableKitap
    {
        public static void AddToTable(string isim, string sayfa, string link)
        {
            string now = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string commandText = "insert into Table_Kitap (isim, sayfa, link, bitti, baslangic, degisim, arsiv) values ('" + isim + "', '" + sayfa + "', '" + link + "', '" + now + "', '" + now + "', '0');";
            DatabaseOperations.UpdateTable(commandText);
        }

        public static List<kitap> GetItems()
        {
            string commandText = "select * from Table_Kitap";
            SQLiteDataReader rdr = DatabaseOperations.GetItems(commandText);

            List<kitap> kitaps = new List<kitap>();
            int id, bitti, sayfa;
            string isim, link, baslangic, degisim;

            while (rdr.Read())
            {
                id = Convert.ToInt32(rdr["id"]);
                isim = rdr["isim"].ToString();
                sayfa = Convert.ToInt16(rdr["sayfa"]);
                link = rdr["link"].ToString();
                baslangic = rdr["baslangic"].ToString();
                degisim = rdr["degisim"].ToString();
                try
                {
                    bitti = Convert.ToInt32(rdr["bitti"]);
                }
                catch (Exception) { bitti = 0; }

                kitap item = new kitap(id, isim, sayfa, link, bitti ,baslangic, degisim);
                kitaps.Add(item);
            }
            return kitaps;
        }

        public static void DeleteFromTable(string id)
        {
            string commandText = "delete from Table_Basvuru where id = '" + id + "'";
            DatabaseOperations.UpdateTable(commandText);
        }

        public static void UpdateRow(string Value, string Column, string id)
        {
            string commandText = "update Table_Basvuru set " + Column + " = '" + Value + "' where id = '" + id + "'";
            DatabaseOperations.UpdateTable(commandText);

            string now = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            commandText = "update Table_Basvuru set degisim = '" + now + "' where id = '" + id + "'";
            DatabaseOperations.UpdateTable(commandText);
        }
    }
}
