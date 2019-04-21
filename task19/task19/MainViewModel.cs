using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Threading;
using task19.Annotations;
using Application = System.Windows.Application;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace task19
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            model = new MainModel();
            InitCommands();
        }

        private MainWindow mainWindow { get; set; }

        private Uri _selectedUri;
        public Uri SelectedUri { get { return _selectedUri;} set {
            if (_selectedUri != value)
            {
                _selectedUri = value;
                OnPropertyChanged();
            }
        } }

        private ObservableCollection<Uri> _playlist;
        public ObservableCollection<Uri> Playlist {
            get
            {
                return _playlist;
            }
            set
            {
                if (value != _playlist)
                {
                    _playlist = value;
                    OnPropertyChanged();
                }
            } }

        private Uri mediaWindowUri;

        public Uri MediaWindowUri
        {
            get { return mediaWindowUri; }

            set
            {
                if (value != mediaWindowUri)
                {
                    mediaWindowUri = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        ///     Commands
        /// </summary>
        public RelayCommand AddToPlaylistCommand { get; private set; }
        public RelayCommand RemoveFromPlaylistCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }
        public RelayCommand PlayCommand { get; private set; }
        public RelayCommand NextCommand { get; private set; }
        public RelayCommand PrevCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }
        public RelayCommand MediaEndedCommand { get; private set; }
        private MainModel model { get; set; }
        

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Initialize commands
        /// </summary>
        private void InitCommands()
        {
            AddToPlaylistCommand = new RelayCommand(o => AddToPlaylist());
            RemoveFromPlaylistCommand= new RelayCommand(o => RemoveFromPlaylist(), o => CanRemove());
            ExitCommand = new RelayCommand(o => Exit());
            PlayCommand = new RelayCommand(o => Play(), o=> CanPlay());
            NextCommand = new RelayCommand(o => Next(), o=> CanNext());
            PrevCommand = new RelayCommand(o => Prev(), o => CanPrev());
            StopCommand = new RelayCommand(o => Stop(), o => CanStop());
            MediaEndedCommand = new RelayCommand(o => MediaEnded());
        }


        private void AddToPlaylist()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media File|*.wmv; *.mp4; *.avi";
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            foreach (string filename in openFileDialog.FileNames)
            {
                Uri file = new Uri(filename);
                if (Playlist == null)
                {
                    Playlist = new ObservableCollection<Uri>();
                }
                Playlist.Add(file);
            }    
        }

        private void RemoveFromPlaylist()
        {
            if (Playlist != null && Playlist.Contains(MediaWindowUri))
            {
                Playlist.Remove(SelectedUri);
            }
        }

        private bool CanRemove()
        {
            return SelectedUri != null;
        }

        private void Play()
        {
            MediaWindowUri = SelectedUri;
        }

        private bool CanPlay()
        {
            return SelectedUri != null;
        }

        private void Next()
        {
            ////loop next
            //if (Playlist.Count > 0)
            //{
            //    SelectedUri = (Playlist.IndexOf(MediaWindowUri) < Playlist.Count)
            //        ? Playlist[Playlist.IndexOf(MediaWindowUri) + 1]
            //        : Playlist[1];
            //    MediaWindowUri = SelectedUri;
            //}
            if (Playlist!= null && Playlist.Count > 0 && Playlist.IndexOf(MediaWindowUri) < Playlist.Count)
            {
                SelectedUri = Playlist[Playlist.IndexOf(MediaWindowUri) + 1];
                MediaWindowUri = SelectedUri;
            }
            else
            {
                MediaWindowUri = null;
            }          
        }

        private bool CanNext()
        {
            if (Playlist != null)
            {
                return Playlist.Count > 0 && Playlist.IndexOf(MediaWindowUri) < Playlist.Count;
            }
            return false;
        }

        private void Prev()
        {
            if (Playlist != null && Playlist.Count > 0 && Playlist.IndexOf(MediaWindowUri) > 0)
            {
                SelectedUri = Playlist[Playlist.IndexOf(MediaWindowUri) - 1];
                MediaWindowUri = SelectedUri;
            }
        }

        private bool CanPrev()
        {
            if (Playlist!=null)
            {
               
                return Playlist.Count > 0 && Playlist.IndexOf(MediaWindowUri) > 0;
            }
            return false;
        }

        private void Stop()
        {
            MediaWindowUri = null;
        }

        private bool CanStop()
        {
            return MediaWindowUri != null;
        }

        /// <summary>
        ///     Exit Program
        /// </summary>
        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private void MediaEnded()
        {
            MessageBox.Show("MediaEnded");
            Next();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}