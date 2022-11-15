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
using System.Xml.Linq;

namespace Fantasy.Wpf.NodeEditControl.Controls
{
    /// <summary>
    /// NodeInkCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class NodeInkCanvas : NodeCanvasBase
    {
        public NodeInkCanvas()
        {
            InitializeComponent();

            this.canvas.PreviewMouseDown += (s, e) =>
            {
                if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
                {

                    var p = e.MouseDevice.GetPosition((UIElement)this.canvas);
                    VisualTreeHelper.HitTest(this.canvas, null, resultCallback: (x) =>
                    {
                        if (x.VisualHit != null)
                        {
                            bool hitRes = this.HitElement((FrameworkElement)x.VisualHit, p);
                            if (hitRes == true)
                            {
                                return HitTestResultBehavior.Stop;
                            }
                        }
                        return HitTestResultBehavior.Continue;
                    }, new PointHitTestParameters(p));
                }
                else if(e.MouseDevice.RightButton==MouseButtonState.Pressed)
                {
                    var p = e.MouseDevice.GetPosition((UIElement)this.canvas);
                    VisualTreeHelper.HitTest(this.canvas, null, resultCallback: (x) =>
                    {
                        if (x.VisualHit != null)
                        {
                            bool hitRes = this.HitElement((FrameworkElement)x.VisualHit, p);
                            if (hitRes == true)
                            {
                                if(this.GetSelectElement()!=null)
                                {
                                    if(this.GetSelectElement() is LineBase line)
                                    {
                                        ContextMenu cm = new ContextMenu();
                                        MenuItem deleteItem = new MenuItem();
                                        deleteItem.Header = "断开";
                                        deleteItem.Click += (x, xx) =>
                                        {
                                            line.Disconnect();
                                            this.RemoveLine(line);
                                            this.ClearSelectElement();

                                        };
                                        cm.Items.Add(deleteItem);
                                       line.ContextMenu = cm;
                                    }
                                }

                                return HitTestResultBehavior.Stop;
                            }
                        }
                        return HitTestResultBehavior.Continue;
                    }, new PointHitTestParameters(p));

                }
            };
            this.canvas.MouseMove += (s, e) =>
            {
                if(e.MouseDevice.LeftButton==MouseButtonState.Pressed)
                {
                    var p = e.MouseDevice.GetPosition((UIElement)this.canvas);
                    this.Move(p);
                }
            };

            this.canvas.MouseUp += (s, e) =>
            {
                if (e.MouseDevice.LeftButton == MouseButtonState.Released)
                {
                    var p = e.MouseDevice.GetPosition((UIElement)this.canvas);
                    this.RealseMouse(p);
                }
            };
        }

        public override void AddNode(NodeBase node)
        {
           this.canvas.Children.Add(node);
        }

        public override void CreateLine(LineBase line)
        {
            this.canvas.Children.Add(line);
        }

        public override void RemoveLine(LineBase line)
        {
            this.canvas.Children.Remove(line);
        }

        public override void RemoveNode(NodeBase node)
        {
            this.canvas.Children.Remove(node);
        }
    }
}
