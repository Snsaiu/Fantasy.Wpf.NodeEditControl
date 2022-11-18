using Fantasy.Wpf.NodeEditControl.Helpers;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fantasy.Wpf.NodeEditControl.Controls
{
    public abstract class LineBase : UserControl, ICanvasElementBase
    {

        public NodeCanvasBase Canvas { get; set; }
        public abstract void UpdateStartPoint(Point point);

        public abstract void UpdateEndPoint(Point point);

        public abstract void UpdateColor(SolidColorBrush color);

        public abstract void UpdateLineWidth(int width);


        public LineBase()
        {
            this.Loaded += (s, e) =>
            {
                this.CommonStyle();

                this.MouseEnter += (ss, ee) =>
                {
                    this.WhenTouchStyle();
                };
                this.MouseLeave += (ss, ee) =>
                {
                    this.CommonStyle();
                };

            };
        }

        /// <summary>
        /// 默认线条风格
        /// </summary>
        public virtual void CommonStyle()
        {

        }

        /// <summary>
        /// 当触碰到线条
        /// </summary>
        public virtual void WhenTouchStyle()
        {
           
        }


        /// <summary>
        /// header node ,without triangle port
        /// </summary>
        public NodeBase HeaderNode { get; set; }

        /// <summary>
        /// tail node ,with triangle port
        /// </summary>
        public NodeBase TailNode { get; set; }

        /// <summary>
        /// start point
        /// </summary>
        public Point StartPiont
        {
            get { return (Point)GetValue(StartPiontProperty); }
            set { SetValue(StartPiontProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartPiont.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartPiontProperty =
            DependencyProperty.Register("StartPiont", typeof(Point), typeof(LineBase), new PropertyMetadata(new Point(0, 0), propertyChangedCallback: StartpointChanged));

        private static void StartpointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as LineBase;
            control.UpdateStartPoint((Point)e.NewValue);

        }


        public void Disconnect()
        {
            if (this.TailNode == null)
                return;

            var tailPorts= this.TailNode.GetPorts();
            if(tailPorts != null )
            {
                foreach (var item in tailPorts)
                {
                    if(item.ConnectedLines.Contains(this))
                        item.ConnectedLines.Remove(this);
                }
            }
            var headerPorts=this.HeaderNode.GetPorts();
            if(headerPorts != null)
            {
                foreach(var item in headerPorts)
                {
                    if(item.ConnectedLines.Contains(this))
                        item.ConnectedLines.Remove(this);
                }
            }

            this.TailNode = null;
            this.HeaderNode = null;
        }

        public void ValidateConnectEndPort(DependencyObject control)
        {
         
                FrameworkElement uc =control as FrameworkElement;
                InputPort port = null;
                while (true)
                {
                    if(uc != null)
                     {
                        if(uc is InputPort)
                        {
                            port=uc as InputPort;
                            break;
                        }
                        else
                        {
                            
                            uc = uc.Parent as FrameworkElement;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if(port != null)
            {

                // validate type is ok

                if(this.HeaderNode != null)
                {
                   if( this.HeaderNode.SupportOutputTypes!=null )
                    {
                       var res= Tools.IsArrayIntersection<Type>(port.Node.SupportInputTypes(), this.HeaderNode.SupportOutputTypes());
                        if(res)
                        {

                            var ports=this.HeaderNode.GetPorts();
                            if(ports==null)
                            {
                                throw new NullReferenceException();
                            }
                            var outputPorts=ports.Where(x=>x.PortType==Enums.PortType.Output).ToList();

                            for (int i = 0; i < port.ConnectedLines.Count; i++)
                            {
                               
                                if(outputPorts!=null&&outputPorts.Count!=0)
                                {
                                    for (int j = 0; j < outputPorts.Count; j++)
                                    {
                                        var find= outputPorts[j].ConnectedLines.Where(x => x == port.ConnectedLines[i]).FirstOrDefault();
                                        if(find!=null)
                                        {
                                            outputPorts[j].RemoveLine(find);
                                        }    
                                    }
                                }

                                this.Canvas.RemoveLine(port.ConnectedLines[i]);
                                port.RemoveLine(port.ConnectedLines[i]);
                                i--;
                            }

                          
                            
                            port.AddLine(this);
                            this.TailNode = port.Node;
                        }
                        else
                        {
                            Tools.ShowWarning("警告", $"节点类型不匹配，不能连接");
                        }
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                }
                else
                {
                    port.AddLine(this);
                    this.TailNode = port.Node;
                }

      
            }
            
        }

        public void ValidateConnectStartPort(DependencyObject control)
        {
            FrameworkElement uc = control as FrameworkElement;
            OutputPort port = null;
            while (true)
            {
                if (uc != null)
                {
                    if (uc is OutputPort)
                    {
                        port = uc as OutputPort;
                        break;
                    }
                    else
                    {

                        uc = uc.Parent as FrameworkElement;
                    }
                }
                else
                {
                    break;
                }
            }
            if (port != null)
            {
                // validate type is ok

                if (this.TailNode != null)
                {
                    if (this.TailNode.SupportInputTypes != null)
                    {
                        var res = Tools.IsArrayIntersection<Type>(port.Node.SupportOutputTypes(), this.TailNode.SupportInputTypes());
                        if (res)
                        {
                            port.AddLine(this);
                            this.HeaderNode = port.Node;
                        }
                        else
                        {
                            Tools.ShowWarning("警告", $"节点类型不匹配，不能连接");
                        }
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                }
                else
                {
                    port.AddLine(this);
                    this.HeaderNode = port.Node;
                }
            }
        }

        /// <summary>
        /// end point
        /// </summary>
        public Point EndPoint
        {
            get { return (Point)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndPointProperty =
            DependencyProperty.Register("EndPoint", typeof(Point), typeof(LineBase), new PropertyMetadata(new Point(0, 10), propertyChangedCallback: EndPointChanged));

        private static void EndPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as LineBase;
            control.UpdateEndPoint((Point)e.NewValue);
        }




        public SolidColorBrush Color
        {
            get { return (SolidColorBrush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(SolidColorBrush), typeof(LineBase), new PropertyMetadata(new SolidColorBrush(Colors.Red), propertyChangedCallback: ColorChanged));

        private static void ColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as LineBase;
            control.UpdateColor((SolidColorBrush)e.NewValue);
        }



        public int LineWidth
        {
            get { return (int)GetValue(LineWidthProperty); }
            set { SetValue(LineWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineWidthProperty =
            DependencyProperty.Register("LineWidth", typeof(int), typeof(LineBase), new PropertyMetadata(1, propertyChangedCallback: LineWidthChanged));

        private static void LineWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as LineBase;
            control.UpdateLineWidth((int)e.NewValue);
        }
    }
}
