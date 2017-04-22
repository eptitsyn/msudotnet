using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using task18.Annotations;

namespace task18
{
    class MainViewModel:INotifyPropertyChanged
    {
        private MainModel Model { get; set; }
        public MainViewModel()
        {
            Model = new MainModel();
            Figures = new ObservableCollection<MyFigure>();
            InitializeCommands();
        }

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
        /// координаты мыши на холсте
        /// </summary>
        private Point mouseCoords;
        public Point MouseCoords
        {
            get
            {
                return mouseCoords;
            }

            set
            {
                mouseCoords = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddFigureCommand { get; private set; }
        private void InitializeCommands()
        {
            AddFigureCommand = new RelayCommand(o => AddFigure());
        }

        private bool _havefirstpoint;
        private Point _firstpoint;
        /// <summary>
        /// Добавить фигуру на холст
        /// </summary>
        private void AddFigure()
        {
            if (_havefirstpoint)
            {
                var figure = new MyFigure();
                figure.Foreground = ForegroundBrush;
                figure.Background = BackgroundBrush;


                switch (SelectedListboxShape)
                {
                        case ShapesEnum.Rectangle:
                        {
                            figure.DefiningGeometry = new RectangleGeometry(new Rect(new Point(), new Size(Math.Abs(_firstpoint.X - mouseCoords.X), Math.Abs(_firstpoint.Y - mouseCoords.Y))));
                            break;
                        }
                        case ShapesEnum.RRectangle:
                        {
                            figure.DefiningGeometry = new RectangleGeometry(new Rect(new Point(), new Size(Math.Abs(_firstpoint.X - mouseCoords.X), Math.Abs(_firstpoint.Y - mouseCoords.Y))), 15, 15);
                            break;
                        }
                        case ShapesEnum.Circle:
                        {
                            figure.DefiningGeometry = new EllipseGeometry(new Point(Math.Abs(_firstpoint.X - mouseCoords.X) / 2, Math.Abs(_firstpoint.Y - mouseCoords.Y) / 2), Math.Abs(_firstpoint.X - mouseCoords.X)/2, Math.Abs(_firstpoint.Y - mouseCoords.Y)/2);
                            break;
                        }
                        case ShapesEnum.Triangle:
                    {
                        var triangle = new StreamGeometry() {};
                        using (StreamGeometryContext ctx = triangle.Open())
                        {
                            ctx.BeginFigure(new Point(), true, true);
                            ctx.LineTo(new Point(Math.Abs(_firstpoint.X - mouseCoords.X)/2, Math.Abs(_firstpoint.Y - mouseCoords.Y)), true, false);
                            ctx.LineTo(new Point(Math.Abs(_firstpoint.X - mouseCoords.X), 0), true, false);
                            ctx.LineTo(new Point(0, 0), true, false);
                        }
                        figure.DefiningGeometry = triangle;
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
                
                figure.X = Math.Min(_firstpoint.X, mouseCoords.X);
                figure.Y = Math.Min(_firstpoint.Y, mouseCoords.Y);
                figures.Add(figure);
                _havefirstpoint = false;
            }
            else
            {
                _firstpoint = mouseCoords;
                _havefirstpoint = true;
            }
        }

        public enum ShapesEnum
        {
            Rectangle,
            RRectangle,
            Circle,
            Triangle
        }

        private ShapesEnum selectedListboxShape;
        public ShapesEnum SelectedListboxShape
        {
            get
            {
                return selectedListboxShape;
            }

            set
            {
                selectedListboxShape = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
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



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
