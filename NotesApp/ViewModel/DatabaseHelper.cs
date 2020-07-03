using System;
using System.IO;
using NotesApp.Model;
using SQLite;

namespace NotesApp.ViewModel
{
    public static class DatabaseHelper
    {
        public static readonly string DbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        public static void InitializeDb()
        {
            using (var conn = new SQLiteConnection(DbFile))
            {
                conn.CreateTable<Notebook>();
                conn.CreateTable<Note>();
            }
        }

        public static bool Insert<T>(T item)
        {
            bool result = false;

            using (var conn = new SQLiteConnection(DbFile))
            {
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
                if (conn.Delete(item) > 0)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
