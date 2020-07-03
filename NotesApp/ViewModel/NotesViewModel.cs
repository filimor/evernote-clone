using System;
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

        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public ObservableCollection<Notebook> Notebooks { get; set; }

        public Notebook SelectedNotebook
        {
            get;
            set;
            //TODO: get notes
        }

        public void CreateNotebook()
        {
            var newNotebook = new Notebook()
            {
                Name = "New notebook"
            };

            DatabaseHelper.Insert(newNotebook);
        }

        public void CreateNote(int notebookId)
        {
            var newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Title = "New note"
            };

            DatabaseHelper.Insert(newNote);
        }
    }
}