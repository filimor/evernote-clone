using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NotesApp.Model;

namespace NotesApp.ViewModel.Commands
{
    public class HasEditedCommand:ICommand
    {
        public NotesViewModel Vm { get; set; }

        public HasEditedCommand(NotesViewModel vm)
        {
            Vm = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Vm.HasRenamed(parameter as Notebook);
        }

        public event EventHandler CanExecuteChanged;
    }
}
