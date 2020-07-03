using System;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public NewNoteCommand(NotesViewModel vm)
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
            //TODO: Create new note
        }

        public event EventHandler CanExecuteChanged;
    }
}