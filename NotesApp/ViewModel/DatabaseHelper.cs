using System;
using System.IO;
using SQLite;

namespace NotesApp.ViewModel
{
    public static class DatabaseHelper
    {
        private static readonly string DbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        public static bool Insert<T>(T item)
        {
            bool result = false;

            using (var conn = new SQLiteConnection(DbFile))
            {
                conn.CreateTable<T>();
                if (conn.Insert(item) > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool Update<T>(T item)
        {
            bool result = false;

            using (var conn = new SQLiteConnection(DbFile))
            {
                conn.CreateTable<T>();
                if (conn.Update(item) > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        public static bool Delete<T>(T item)
        {
            bool result = false;

            using (var conn = new SQLiteConnection(DbFile))
            {
                conn.CreateTable<T>();
                if (conn.Delete(item) > 0)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
