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
            string commandText = "insert into Table_Kitap (isim, sayfa, baslangic, degisim) values ('" + isim + "', '" + sayfa + "', '" + now + "', '" + now + "');";
            DatabaseOperations.UpdateTable(commandText);
        }

        public static List<basvuru> GetItems()
        {
            string commandText = "select * from Table_Basvuru";
            SQLiteDataReader rdr = DatabaseOperations.GetItems(commandText);

            List<basvuru> basvurus = new List<basvuru>();
            int id, bitti;
            string isim, kayit, son, sonuc, link, baslangic, degisim;

            while (rdr.Read())
            {
                id = Convert.ToInt32(rdr["id"]);
                isim = rdr["isim"].ToString();
                kayit = rdr["kayit"].ToString();
                son = rdr["son"].ToString();
                sonuc = rdr["sonuc"].ToString();
                link = rdr["link"].ToString();
                baslangic = rdr["baslangic"].ToString();
                degisim = rdr["degisim"].ToString();
                try
                {
                    bitti = Convert.ToInt32(rdr["bitti"]);
                }
                catch (Exception) { bitti = 0; }

                basvuru item = new basvuru(id, isim, kayit, son, sonuc, link, bitti, baslangic, degisim);
                basvurus.Add(item);
            }
            return basvurus;
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
