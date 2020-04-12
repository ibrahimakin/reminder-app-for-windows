using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace latest_point.Sync
{
    class SyncControl
    {
        private static List<string> tables = new List<string>() { "Table_Basvuru", "Table_Video", "Table_Kitap" };

        public static void StartSync()
        {
            string commandText;
            SQLiteDataReader rdr;
            int id, sayfa, bitti, arsiv;
            string isim, kayit, son, sonuc, kacinci, dakika, link, baslangic, degisim, hash, myData;
            myData = "{\n";
            foreach (string item in tables){
                int i = 0;
                commandText = "select * from " + item + " where sync > 0";
                rdr = Database.DatabaseOperations.GetItems(commandText);
                myData += "\""+item+"\":{";
                while (rdr.Read())
                {
                    i++;
                    // JSON - Request Response
                    id = Convert.ToInt32(rdr["id"]);
                    isim = rdr["isim"].ToString();
                    degisim = rdr["degisim"].ToString();
                    hash = rdr["hash"].ToString();
                    myData +="\"" + i + "\":{\"hash\":\"" + hash + "\"," +
                        "\"degisim\":\"" + degisim + "\"," +
                        "\"data\":{" +
                                    "\"id\":\"" + id + "\"," +
                                    "\"isim\":\"" + isim + "\",";
                    if (item == "Table_Basvuru")
                    {
                        kayit = rdr["kayit"].ToString();
                        son = rdr["son"].ToString();
                        sonuc = rdr["sonuc"].ToString();
                        myData += "\"kayit\":\"" + kayit + "\"," +
                            "\"son\":\"" + son + "\"," +
                            "\"sonuc\":\"" + sonuc + "\",";
                    }
                    else if (item == "Table_Video")
                    {
                        kacinci = rdr["kacinci"].ToString();
                        dakika = rdr["dakika"].ToString();
                        myData += "\"kacinci\":\"" + kacinci + "\"," +
                            "\"dakia\":\"" + dakika + "\",";
                    }
                    else if (item == "Table_Kitap")
                    {
                        sayfa = Convert.ToInt16(rdr["sayfa"]);
                        myData += "\"sayfa\":\"" + sayfa + "\",";
                    }
                    link = rdr["link"].ToString();
                    bitti = Convert.ToInt32(rdr["bitti"]);
                    baslangic = rdr["baslangic"].ToString();
                    arsiv = Convert.ToInt32(rdr["arsiv"]);
                    myData += "\"link\":\"" + link + "\"," +
                            "\"bitti\":\"" + bitti + "\"," +
                            "\"baslangic\":\"" + baslangic + "\"," +
                            "\"arsiv\":\"" + arsiv + "\"}},";
                    
                }
                myData += "},\n";
            }
            myData += "}";
            var path = @"" + Environment.CurrentDirectory + "\\DB\\data.json";

            File.WriteAllText(path, myData);
        }
    }
}
