using System;
using System.Windows.Input;
using NotesApp.Model;

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
            return parameter is Notebook;
        }

        public void Execute(object parameter)
        {
            var selectedNotebook = parameter as Notebook;
            Vm.CreateNoteAsync(selectedNotebook.Id);
        }

        public event EventHandler CanExecuteChanged;
    }
}