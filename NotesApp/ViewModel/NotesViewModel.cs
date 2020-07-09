using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using SQLite;

namespace NotesApp.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        private Note _selectedNote;

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                SelectedNoteChanged(this,new EventArgs());
                OnPropertyChanged(nameof(SelectedNote));

            }
        }

        private bool _isEditing;

        private Notebook _selectedNotebook;

        public NotesViewModel()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();
            BeginEditCommand = new BeginEditCommand(this);
            HasEditedCommand = new HasEditedCommand(this);

            DatabaseHelper.InitializeDb();
            ReadNotebooksAsync();
            ReadNotesAsync();
        }

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged(nameof(IsEditing));
            }
        }

        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public BeginEditCommand BeginEditCommand { get; set; }
        public HasEditedCommand HasEditedCommand { get; set; }

        public Notebook SelectedNotebook
        {
            get => _selectedNotebook;
            set
            {
                _selectedNotebook = value;
                ReadNotesAsync();
                OnPropertyChanged(nameof(SelectedNotebook));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        public async void CreateNotebookAsync()
        {
            var newNotebook = new Notebook
            {
                Name = "New notebook",
                UserId = App.UserId
            };

            // DatabaseHelper.Insert(newNotebook);

            try
            {
                await App.MobileServiceClient.GetTable<Notebook>().InsertAsync(newNotebook);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            ReadNotebooksAsync();
        }

        public async void CreateNoteAsync(string notebookId)
        {
            var newNote = new Note
            {
                NotebookId = notebookId,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Title = "New note"
            };

            // DatabaseHelper.Insert(newNote);

            try
            {
                await App.MobileServiceClient.GetTable<Note>().InsertAsync(newNote);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            ReadNotesAsync();
        }

        public async void ReadNotebooksAsync()
        {
            //using (var conn = new SQLiteConnection(DatabaseHelper.DbFile))
            //{
            //    var notebooks = conn.Table<Notebook>().ToList();
            //    Notebooks.Clear();
            //    foreach (var notebook in notebooks)
            //    {
            //        Notebooks.Add(notebook);
            //    }
            //}

            try
            {
                var notebooks = await App.MobileServiceClient.GetTable<Notebook>().Where(n => n.UserId == App.UserId)
                    .ToListAsync();
                Notebooks.Clear();
                foreach (var notebook in notebooks)
                {
                    Notebooks.Add(notebook);
                }
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        public async void ReadNotesAsync()
        {
            //using (var conn = new SQLiteConnection(DatabaseHelper.DbFile))
            //{
            //    if (SelectedNotebook != null)
            //    {
            //        var notes = conn.Table<Note>().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();
            //        Notes.Clear();
            //        foreach (var note in notes)
            //        {
            //            Notes.Add(note);
            //        }
            //    }
            //}

            try
            {
                var notes = await App.MobileServiceClient.GetTable<Note>().Where(n => n.NotebookId == SelectedNotebook.Id)
                    .ToListAsync();
                Notebooks.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            catch (Exception e)
            {
               // TODO
            }
        }

        public void StartEditing()
        {
            IsEditing = true;
        }

        public async void HasRenamedAsync(Notebook notebook)
        {
            if (notebook != null)
            {
                // DatabaseHelper.Update(notebook);

                try
                {
                    await App.MobileServiceClient.GetSyncTable<Notebook>().UpdateAsync(notebook);
                    IsEditing = false;
                    ReadNotebooksAsync();
                }
                catch (Exception e)
                {
                    // TODO
                }


            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void UpdateSelectedNoteAsync()
        {
            // DatabaseHelper.Update(SelectedNote);

            try
            {
                await App.MobileServiceClient.GetSyncTable<Note>().UpdateAsync(SelectedNote);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}