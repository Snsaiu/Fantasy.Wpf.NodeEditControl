

using System;
using System.Collections.Generic;
using System.Diagnostics;
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

using static System.Net.Mime.MediaTypeNames;

namespace Fantasy.Wpf.NodeEditControl.Controls
{
    /// <summary>
    /// ArrowLine.xaml 的交互逻辑
    /// </summary>
    public partial class ArrowLine : LineBase
    {

        /// <summary>
        /// line width and line tail circle radio
        /// </summary>
        private int _lineWidth = 1;

        private bool _triangleCanMove = false;

        private bool _circleCanMove = false;

        public ArrowLine()
        {
            InitializeComponent();
            this.line.Stroke = this.Color;
            this.line.StrokeThickness = this.LineWidth;
            this.triangle.Stroke = this.Color;
            
            //this.triangle.MouseDown += (s, e) =>
            //{
            //    this._triangleCanMove = true;
            //};
    
            //this.triangle.MouseUp += (s, e) =>
            //{
            //  this._triangleCanMove = false;
            //    this.triangle.StrokeThickness = 1;
            //    var p = e.MouseDevice.GetPosition((UIElement)this.Parent);
            //    VisualTreeHelper.HitTest((UIElement)this.Parent, null, resultCallback: (x) =>
            //    {
            //        if (x.VisualHit != null)
            //        {
            //            this.ValidateConnectEndPort(x.VisualHit);
            //        }

            //        return HitTestResultBehavior.Continue;

            //    }, new PointHitTestParameters(p));
            //};

           

            //this.triangle.MouseMove += (s, e) =>
            //{
            //    var p = e.MouseDevice.GetPosition((UIElement)this.Parent);
            //    if (e.MouseDevice.LeftButton==MouseButtonState.Pressed)
            //    {
            //            this.line.X2 = p.X+15;
            //            this.line.Y2 = p.Y+10;
            //            this.updateTriangle(this.line.X1, this.line.Y1, this.line.X2, this.line.Y2);
                   
            //    }
            //    else
            //    {
                   
            //    }
    
          
            //   // Debug.WriteLine($"x:{p.X}  y:{p.Y}");
            //};

            //this.tail.MouseUp += (s, e) =>
            //{
            //    this.tail.Data = new EllipseGeometry(new Point(this.line.X1, this.line.Y1), this._lineWidth, this._lineWidth);
            //    var p = e.MouseDevice.GetPosition((UIElement)this.Parent);
            //    VisualTreeHelper.HitTest((UIElement)this.Parent, null, resultCallback: (x) =>
            //    {
            //        if (x.VisualHit != null)
            //        {
            //            this.ValidateConnectStartPort(x.VisualHit);
            //        }

            //        return HitTestResultBehavior.Continue;

            //    }, new PointHitTestParameters(p));
            //};
            //this.tail.MouseMove += (s, e) =>
            //{
            //    if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
            //    {
            //        var p = e.MouseDevice.GetPosition((UIElement)this.Parent);
            //        this.line.X1 = p.X ;
            //        this.line.Y1 = p.Y;
            //        this.tail.Data = new EllipseGeometry(new Point(this.line.X1, this.line.Y1), this._lineWidth, this._lineWidth);
            //        this.updateTriangle(this.line.X1, this.line.Y1, this.line.X2, this.line.Y2);
            //    }
            
            //};

        }


        public override void UpdateEndPoint(Point point)
        {
           this.line.X2= point.X;
            this.line.Y2= point.Y;
         
            this.tail.Data = new EllipseGeometry(new Point(this.line.X1, this.line.Y1), this._lineWidth, this._lineWidth);
            this.updateTriangle(this.line.X1,this.line.Y1, this.line.X2, this.line.Y2);
        }

        public override void UpdateStartPoint(Point point)
        {
            this.line.X1 = point.X;
            this.line.Y1 = point.Y;
   
            this.tail.Data = new EllipseGeometry(new Point(this.line.X1, this.line.Y1), this._lineWidth,this._lineWidth);
            this.updateTriangle(this.line.X1, this.line.Y1, this.line.X2, this.line.Y2);
        }

        public override void UpdateColor(SolidColorBrush color)
        {
            this.line.StrokeThickness = 2;
           this.line.Stroke = color;
            this.triangle.StrokeThickness = 2;
            this.triangle.Stroke = color;
            this.triangle.Fill= color;
        }

        public override void CommonStyle()
        {
            this.tail.Stroke = this.Color;
            this.line.Stroke = this.Color;
            this.triangle.Stroke = this.Color;

        }

        public override void WhenTouchStyle()
        {
            this.tail.Stroke = new SolidColorBrush( Colors.Orange);
            this.line.Stroke = new SolidColorBrush(Colors.Orange);
            this.triangle.Stroke = new SolidColorBrush(Colors.Orange);
        }

        public override void UpdateLineWidth(int width)
        {
            this._lineWidth = width;
            this.line.StrokeThickness=width;
            this.tail.Data = new EllipseGeometry(new Point(this.line.X1, this.line.Y1), width,width);
        }
        private void updateTriangle(double x1, double y1, double x2, double y2)
        {
            double h = 5;
            double w = (double)5;

            //线段首位端点部分的(△x,△y)横纵坐标差
            double dx = x2 - x1;
            double dy = y2 - y1;
            //线段的长度
            double d = (double)Math.Sqrt(dx * dx + dy * dy);

            //(x,y)->(px,py)箭头底边和线段的交点
            double px = x2 - (h / d * dx); //(px ∈ R实数)
            double py = y2 - (h / d * dy); //(py ∈ R)

            //为防止线段末尾比箭头突出，此处线段的末尾端点坐标重新计算，末端坐标=箭头底边到箭头的一半位置
            //double lineEndX = (px + x2) / 2;
            //double lineEndY = (py + y2) / 2;

            Point trianlePoint1= new Point(px + (w / d * dy), py - (w / d * dx));
            Point trianlePoint2= new Point(px - (w / d * dy), py + (w / d * dx));
            this.triangle.Points = new PointCollection()
            {
                trianlePoint1 ,new Point(x2,y2), trianlePoint2
            };

        }
    }
}
