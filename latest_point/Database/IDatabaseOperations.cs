namespace latest_point.Database
{
    interface IDatabaseOperations
    {
        void addToTable(string commandText);
        void rowUpdate();
        void deleteFromTable();
    }
}
