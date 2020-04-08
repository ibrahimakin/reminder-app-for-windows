using System;
using System.Data.SQLite;

namespace latest_point.Database
{
    class DatabaseOperations
    {
        private static string DBpath = @"Data Source=" + Environment.CurrentDirectory + "\\DB\\db_NeredeKaldim.db;Version=3;Compress=True;Read Only=False;";
        private static SQLiteConnection conn;
        private static bool ConnState;

        public static void openConn()
        {
            try
            {
                conn = new SQLiteConnection(DBpath);
                conn.Open();
                SQLiteCommand command = new SQLiteCommand("select id from Table_Kitap", conn);
                command.ExecuteNonQuery();
                ConnState = true;
            }
            catch (Exception)
            {
                ConnState = false;
            }
        }

        public static bool getState()
        {
            return ConnState;
        }

        public static SQLiteConnection getConn()
        {
            return conn;
        }

        public static void addToTable(string commandText)
        {
            SQLiteCommand command = new SQLiteCommand(commandText, conn);
            command.ExecuteNonQuery();
        }

        public static void deleteFromTable()
        {

        }

        public static void rowUpdate()
        {

        }
    }
}
