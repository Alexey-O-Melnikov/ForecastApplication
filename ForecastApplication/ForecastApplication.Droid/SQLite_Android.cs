using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ForecastApplication.Droid;
using SQLite;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(SQLite_Android))]
namespace ForecastApplication.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }
        public SQLiteConnection GetConnection(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            var conn = new SQLiteConnection(path);

            return conn;
        }
    }
}