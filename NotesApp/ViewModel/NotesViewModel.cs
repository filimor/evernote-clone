using System.Collections.ObjectModel;
using NotesApp.Model;
using NotesApp.ViewModel.Commands;

namespace NotesApp.ViewModel
{
    public class NotesViewModel
    {
        public NotesViewModel()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
        }

        public ObservableCollection<Notebook> Notebooks { get; set; }

        public Notebook SelectedNotebook
        {
            get;
            set;
            //TODO: get notes
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
    }
}