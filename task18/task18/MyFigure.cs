using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace task18
{
    public class MyFigure
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Brush Foreground { get; set; }
        public Brush Background { get; set; }
        public Geometry DefiningGeometry { get; set; }
    }
}
