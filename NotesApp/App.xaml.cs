using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.WindowsAzure.MobileServices;

namespace NotesApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string UserId = string.Empty;
        public static MobileServiceClient MobileServiceClient = new MobileServiceClient("https://clonevernote.azurewebsites.net");
    }
}
