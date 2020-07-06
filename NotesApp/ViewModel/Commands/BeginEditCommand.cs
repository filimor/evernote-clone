using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    public class BeginEditCommand:ICommand
    {
        public NotesViewModel Vm { get; set; }

        public BeginEditCommand(NotesViewModel vm)
        {
            Vm = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Vm.StartEditing();
        }

        public event EventHandler CanExecuteChanged;
    }
}
