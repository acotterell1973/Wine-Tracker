using SQLite;

namespace WineTracker.Interface
{
    public interface IDatabaseConnection
    {
        SQLiteConnection DbConnection();
        SQLiteAsyncConnection DbAsyncConnection();
    }
}
