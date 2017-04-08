using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using task16.Annotations;

namespace task16
{
    public class Book : INotifyPropertyChanged
    {
        private ObservableCollection<Author> authors;
        private string name;

        private int year;

        public int Year
        {
            get { return year; }

            set
            {
                year = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Author> Authors
        {
            get { return authors; }

            set
            {
                authors = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }

            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Author
    {
        public string Name { get; set; }
    }
}