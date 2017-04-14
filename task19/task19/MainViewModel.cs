using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Win32;
using task19.Annotations;

namespace task19
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(MainWindow window)
        {
            model = new MainModel();
            mainWindow = window;
            InitCommands();
        }

        private MainWindow mainWindow { get; set; }
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

        /// <summary>
        ///     Commands
        /// </summary>
        public RelayCommand AddToPlaylistCommand { get; private set; }
        public RelayCommand RemoveFromPlaylistCommand { get; private set; }
        public RelayCommand ExitCommand { get; private set; }
        public RelayCommand PlayPauseCommand { get; private set; }
        private MainModel model { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Initialize commands
        /// </summary>
        private void InitCommands()
        {
            AddToPlaylistCommand = new RelayCommand(o => AddToPlaylist());
            ExitCommand = new RelayCommand(o => Exit());
        }


        private void AddToPlaylist()
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media File|*.wmv; *.mp4; *.avi";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != string.Empty)
            {
                Uri file = new Uri(openFileDialog.FileName);
                if (Playlist == null)
                {
                    Playlist = new ObservableCollection<Uri>();
                }
                Playlist.Add(file);
            }
        }

        private void PlayPause()
        {
            //
        }

        /// <summary>
        ///     Exit Program
        /// </summary>
        private void Exit()
        {
            Application.Current.Shutdown();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}