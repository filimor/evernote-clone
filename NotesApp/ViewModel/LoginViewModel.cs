using System;
using System.Linq;
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

        public async void LoginAsync()
        {
            //using (var conn = new SQLiteConnection(DatabaseHelper.DbFile))
            //{
            //    conn.CreateTable<User>();
            //    var user = conn.Table<User>().FirstOrDefault(u => u.Username == User.Username);
            //    if (user.Password == User.Password)
            //    {
            //        App.UserId = user.Id.ToString();
            //        HasLoggedIn(this, new EventArgs());
            //    }
            //}

            try
            {
                var user = (await App.MobileServiceClient.GetTable<User>().Where(u => u.Username == User.Username).ToListAsync()).FirstOrDefault();
                if (user.Password == User.Password)
                {
                    App.UserId = user.Id;
                    HasLoggedIn(this, new EventArgs());
                }
            }
            catch (Exception e )
            {
                // TODO
            }
        }

        public async void RegisterAsync()
        {
            //using (var conn = new SQLiteConnection(DatabaseHelper.DbFile))
            //{
            //    conn.CreateTable<User>();
            //    if (DatabaseHelper.Insert(User))
            //    {
            //        App.UserId = User.Id.ToString();
            //        HasLoggedIn(this, new EventArgs());
            //    }
            //}

            try
            {
                await App.MobileServiceClient.GetTable<User>().InsertAsync(User);
                App.UserId = User.Id;
                HasLoggedIn(this,new EventArgs());
            }
            catch (Exception e)
            {
                //TODO
            }
        }
    }
}