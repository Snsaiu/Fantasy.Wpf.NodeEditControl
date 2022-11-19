using Fantasy.Wpf.NodeEditControl.Controls.Bases;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fantasy.Wpf.NodeEditControl.Controls.Lines
{
    /// <summary>
    /// BezierLine.xaml 的交互逻辑
    /// </summary>
    public partial class BezierLine : LineBase
    {
        public BezierLine()
        {
            InitializeComponent();
            this.LineWidth = 2;
            this.Color=new SolidColorBrush(Colors.OrangeRed);
        }

        public override void UpdateStartPoint(Point point)
        {
          this.figure.StartPoint = point;
          this.segment.Point1=new Point(point.X+100, point.Y);
        }

        public override void UpdateEndPoint(Point point)
        {
            this.segment.Point2 = new Point(point.X - 100, point.Y);
          this.segment.Point3=point;
        }

        public override void UpdateColor(SolidColorBrush color)
        {
            this.path.Stroke = color;
        }

        public override void UpdateLineWidth(int width)
        {
          this.path.StrokeThickness = width;
        }
    }
}
