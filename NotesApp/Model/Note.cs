using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;

namespace NotesApp.Model
{
    public class Note : INotifyPropertyChanged
    {
        private DateTime _createdTime;

        private string _fileLocation;

        private string _id;

        private string _notebookId;

        private string _title;

        private DateTime _updatedTime;

        [PrimaryKey, AutoIncrement]
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        [Indexed]
        public string NotebookId
        {
            get => _notebookId;
            set
            {
                _notebookId = value;
                OnPropertyChanged(nameof(NotebookId));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public DateTime CreatedTime
        {
            get => _createdTime;
            set
            {
                _createdTime = value;
                OnPropertyChanged(nameof(CreatedTime));
            }
        }

        public DateTime UpdatedTime
        {
            get => _updatedTime;
            set
            {
                _updatedTime = value;
                OnPropertyChanged(nameof(UpdatedTime));
            }
        }

        public string FileLocation
        {
            get => _fileLocation;
            set
            {
                _fileLocation = value;
                OnPropertyChanged(nameof(FileLocation));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}