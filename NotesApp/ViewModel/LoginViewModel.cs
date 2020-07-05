using System;
using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using SQLite;

namespace NotesApp.ViewModel
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            User = new User();
            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
        }

        public User User { get; set; }

        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }

        public event EventHandler HasLoggedIn;

        public void Login()
        {
            using (var conn = new SQLiteConnection(DatabaseHelper.DbFile))
            {
                conn.CreateTable<User>();
                var user = conn.Table<User>().FirstOrDefault(u => u.Username == User.Username);
                if (user.Password == User.Password)
                {
                    App.UserId = user.Id.ToString();
                    HasLoggedIn(this, new EventArgs());
                }
            }
        }

        public void Register()
        {
            using (var conn = new SQLiteConnection(DatabaseHelper.DbFile))
            {
                conn.CreateTable<User>();
                if (DatabaseHelper.Insert(User))
                {
                    App.UserId = User.Id.ToString();
                    HasLoggedIn(this, new EventArgs());
                }
            }
        }
    }
}