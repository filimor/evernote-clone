using System;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    public class NewNotebookCommand : ICommand
    {
        public NewNotebookCommand(NotesViewModel vm)
        {
            Vm = vm;
        }

        public NotesViewModel Vm { get; set; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //TODO: Create new notebook
        }

        public event EventHandler CanExecuteChanged;
    }
}