using System;
using System.Data.SQLite;

namespace latest_point
{
    public class DBconnection
    {
        public static string DBpath = @"Data Source=" + Environment.CurrentDirectory + "\\DB\\db_NeredeKaldim.db;Version=3;Compress=True;Read Only=False;";
        public static string ConnState;
        public static bool durum = false;
        public static void ConnTest()
        {
            using ( SQLiteConnection conn = new SQLiteConnection(DBpath))
            {
                try
                {
                    conn.Open();
                    SQLiteCommand command = new SQLiteCommand("select * from Table_Kitap", conn);
                    command.ExecuteNonQuery();
                    ConnState = "";
                    durum = true;
                }
                catch(Exception)
                {
                    ConnState = "Veritabanı bağlanamadı.";
                    durum = false;
                }
            }
        }
    }
}
