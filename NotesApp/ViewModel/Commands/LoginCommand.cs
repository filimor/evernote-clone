using System;
using System.Windows.Input;
using NotesApp.Model;

namespace NotesApp.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginCommand(LoginViewModel vm)
        {
            Vm = vm;
        }

        public LoginViewModel Vm { get; set; }

        public bool CanExecute(object parameter)
        {
            //return parameter is User user &&
            //       !string.IsNullOrEmpty(user.Username) &&
            //       !string.IsNullOrEmpty(user.Password);
            return true;
        }

        public void Execute(object parameter)
        {
            Vm.LoginAsync();
        }

        public event EventHandler CanExecuteChanged;
    }
}