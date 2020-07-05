using System;
using System.Windows.Input;
using NotesApp.Model;

namespace NotesApp.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public RegisterCommand(LoginViewModel vm)
        {
            Vm = vm;
        }

        public LoginViewModel Vm { get; set; }

        public bool CanExecute(object parameter)
        {
            //return parameter is User user &&
            //       !string.IsNullOrEmpty(user.Username) &&
            //       !string.IsNullOrEmpty(user.Password) &&
            //       !string.IsNullOrEmpty(user.Email) &&
            //       !string.IsNullOrEmpty(user.LastName) &&
            //       !string.IsNullOrEmpty(user.Email);
            return true;
        }

        public void Execute(object parameter)
        {
            Vm.Register();
        }

        public event EventHandler CanExecuteChanged;
    }
}