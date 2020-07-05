using System;
using System.Collections.ObjectModel;
using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using SQLite;

namespace NotesApp.ViewModel
{
    public class NotesViewModel
    {
        public NotesViewModel()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            DatabaseHelper.InitializeDb();
            ReadNotebooks();
            ReadNotes();
        }

        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook _selectedNotebook;

        public Notebook SelectedNotebook
        {
            get => _selectedNotebook;
            set
            {
                _selectedNotebook = value;
                ReadNotes();
            }
        }


        public void CreateNotebook()
        {
            var newNotebook = new Notebook()
            {
                Name = "New notebook",
                UserId = int.Parse(App.UserId)
            };

            DatabaseHelper.Insert(newNotebook);
            ReadNotebooks();
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
            ReadNotes();
        }

        public void ReadNotebooks()
        {
            using (var conn = new SQLiteConnection(DatabaseHelper.DbFile))
            {
                var notebooks = conn.Table<Notebook>().ToList();
                Notebooks.Clear();
                foreach (var notebook in notebooks)
                {
                    Notebooks.Add(notebook);
                }
            }
        }

        public void ReadNotes()
        {
            using (var conn = new SQLiteConnection(DatabaseHelper.DbFile))
            {
                if (SelectedNotebook!=null)
                {
                    var notes = conn.Table<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();
                    Notes.Clear();
                    foreach (var note in notes)
                    {
                        Notes.Add(note);
                    }
                }
            }
        }
    }
}