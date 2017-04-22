using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private MainModel Model { get; set; }

        /// <summary>
        /// Здесь храним перечень объектов на холсте
        /// </summary>
        private ObservableCollection<MyFigure> figures;

        public ObservableCollection<MyFigure> Figures
        {
            get
            {
                return figures;
            }

            set
            {
                figures = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// 
        /// </summary>

        private Brush _foregroundBrush = Brushes.Black;
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

        private Brush _backgroundBrush = Brushes.White;
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

        public Shape ListboxSelectedShape
        {
            get
            {
                return listboxSelectedShape;
            }

            set
            {
                if (value != listboxSelectedShape)
                {
                    listboxSelectedShape = value;
                    OnPropertyChanged();
                }
            }
        }



        private Shape listboxSelectedShape;

        public MainViewModel()
        {
            Model = new MainModel();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
