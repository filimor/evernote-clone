using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    public class LoginCommand :ICommand
    {
        public LoginViewModel Vm { get; set; }

        public LoginCommand(LoginViewModel vm)
        {
            Vm = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            // TODO: Login functionality
        }

        public event EventHandler CanExecuteChanged;


    }
}
