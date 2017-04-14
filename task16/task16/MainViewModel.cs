using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using task16.Annotations;

namespace task16
{
    class MainViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<Book> _books;

        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set
            {
                if (_books != value)
                {
                    _books = value;
                    OnPropertyChanged();
                }
            }
        }
        MainModel model { get; set; }
        public MainViewModel()
        {
            model = new MainModel();
            InitializeCommands();
        }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get { return _selectedBook; }
            set {
                    if (_selectedBook != value)
                    {
                        _selectedBook = value;
                        OnPropertyChanged();
                    }
                }
        }

        private Author _selectedAuthor;
        public Author SelectedAuthor
                {
                    get
                    {
                        return _selectedAuthor;
                    }

                    set
                    {
                        if (SelectedAuthor != value)
                        {
                            _selectedAuthor = value;
                            OnPropertyChanged();
                        }
                    }
                }

        public RelayCommand NewCommand { get; private set; }
        public RelayCommand AddBookCommand { get; private set; }
        public RelayCommand RemoveBookCommand { get; private set; }
        public RelayCommand AddAuthorCommand { get; private set; }
        public RelayCommand RemoveAuthorCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }


        private void InitializeCommands()
        {
            AddBookCommand = new RelayCommand(o => AddBook(), o => CanAddBook());
            RemoveBookCommand = new RelayCommand(RemoveBook, CanRemoveBook);
            AddAuthorCommand = new RelayCommand(o => AddAuthor(), o=>CanAddAuthor());
            RemoveAuthorCommand = new RelayCommand( RemoveAuthor, CanRemoveAuthor);
            NewCommand = new RelayCommand(o => New());
            LoadCommand = new RelayCommand(o => Load());
            SaveCommand = new RelayCommand(o => Save(), o=> CanSave());
            ExitCommand = new RelayCommand(o => Exit());
        }

        private void New()
        {
            model.New();
            Books = new ObservableCollection<Book>(model.Books);
        }

        private void Load()
        {
            model.Load();
            Books = new ObservableCollection<Book>(model.Books);
        }

        private void Save()
        {
            model.Books = Books.ToArray();
            model.Save();
        }

        private bool CanSave()
        {
            return Books != null;
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }
        private void AddBook()
        {
            Book book = new Book()
            {
                Name = "Название",
                Year = 2017,
                Authors = new ObservableCollection<Author>()
            }
            ;
            Books.Add(book);
            SelectedBook = book;
        }

        private bool CanAddBook()
        {
            return Books != null;
        }

        private void RemoveBook(object selectedItem)
        {
            Book book = (Book) selectedItem;
            Books.Remove(book);
        }
        private bool CanRemoveBook(object selectedItem)
        {
            return selectedItem is Book;
        }
        private void AddAuthor()
        {
            Author author = new Author()
            {
                Name = "Имя"
            };
            SelectedBook.Authors.Add(author);
        }

        private bool CanAddAuthor()
        {
            return SelectedBook != null;
        }

        private void RemoveAuthor(object selectedItem)
        {
            Author author = (Author)selectedItem;
            SelectedBook.Authors.Remove(author);
        }
        private bool CanRemoveAuthor(object selectedItem)
        {
            return selectedItem != null && selectedItem is Author;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
