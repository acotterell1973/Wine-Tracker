using System;
using System.IO;
using SQLite;
using WineTracker.iOS.Implementation;
using WineTracker.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqLiteDatabase))]
namespace WineTracker.iOS.Implementation
{
    public class SqLiteDatabase : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var sqliteFilename = "WineHunterSQLite.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}
