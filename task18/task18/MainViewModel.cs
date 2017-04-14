using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using task18.Annotations;

namespace task18
{
    class MainViewModel:INotifyPropertyChanged
    {
        private MainModel model { get; set; }

        private Brush _foregroundBrush;
        public Brush ForegroundBrush
        {
            get { return _foregroundBrush; }
            set
            {
                if (value != _foregroundBrush)
                {
                    _foregroundBrush = value;
                    OnPropertyChanged();
                }
            } 
        }

        private Brush _backgroundBrush;
        public Brush BackgroundBrush
        {
            get { return _backgroundBrush; }
            set
            {
                if (value != _backgroundBrush)
                {
                    _backgroundBrush = value;
                    OnPropertyChanged();
}
            } 
        }

        private Shape _selectedShape;

        public Shape SelectedShape
        {
            get { return _selectedShape; }
            set
            {
                if (value!= _selectedShape)
                {
                    _selectedShape = value;
                    OnPropertyChanged();
                }
            }
        }
        public MainViewModel()
        {
            model = new MainModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
