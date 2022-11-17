using Fantasy.Wpf.NodeEditControl.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

using static System.Formats.Asn1.AsnWriter;

namespace Fantasy.Wpf.NodeEditControl.Controls
{
    /// <summary>
    /// NodeInkCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class NodeInkCanvas : NodeCanvasBase
    {

        private Point _dragStartPoint;



        private bool _treeItemIsDrag = false;


        private Point _canvasStartPoint;

        private List<TreeItem> _nodes = new List<TreeItem>();
        public NodeInkCanvas()
        {
            InitializeComponent();
            
            this.canvas.RenderTransform = new TransformGroup();

            this.Loaded += (s, e) =>
            {
                TransformGroup tg = this.canvas.RenderTransform as TransformGroup;
                if (tg != null)
                {
                    Window window = Window.GetWindow(this);
                    Point p = this.canvasBorder.TransformToAncestor(window).Transform(new Point(0, 0));
                    tg.Children.Add(new ScaleTransform(100, 100, p.X + this.canvasBorder.ActualWidth / 2, p.Y + this.canvasBorder.ActualHeight / 2));

                }
            };
  


            this.RegistNodeEvent += (node) =>
            {

                var existGroup = this._nodes.Where(x => x.GroupName == node.GroupName).FirstOrDefault();
                if (existGroup != null)
                {
                    existGroup.Nodes.Add(new NodeDisplayItem { Logo = node.NodeLogo, NodeName = node.NodeName,GroupName=node.GroupName });
                }
                else
                {
                    var newGroup = new TreeItem
                    {
                        GroupName = node.GroupName,
                        Nodes = new List<NodeDisplayItem>(){ new NodeDisplayItem{
                     Logo=node.NodeLogo,
                     NodeName = node.NodeName,
                     GroupName=node.GroupName
                    } }
                    };
                    this._nodes.Add(newGroup);
                }

                this.treeview.ItemsSource = this._nodes;
            };

                this.treeview.MouseDown += (s, e) =>
                {
                    this._dragStartPoint = e.GetPosition(null);
                };
                this.treeview.MouseMove += (s, e) =>
                {
                    if (e.LeftButton == MouseButtonState.Pressed && !_treeItemIsDrag)
                    {
                        Point position = e.GetPosition(null);
                    }
                    if (e.LeftButton == MouseButtonState.Pressed && !_treeItemIsDrag)
                    {
                        Point position = e.GetPosition(null);


                        if (Math.Abs(position.X - _dragStartPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                           Math.Abs(position.Y - _dragStartPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                        {
                            _treeItemIsDrag = true;

                            if (this.treeview.SelectedItem is NodeDisplayItem selectNode)
                            {
                                var select= this.Nodes.Where(x=>x.GroupName==selectNode.GroupName&&x.NodeName==selectNode.NodeName).FirstOrDefault();
                                if(select!=null)
                                {
                                    DataObject data=new DataObject(typeof(RegistNodeItem),select);
                                    DragDropEffects de=DragDrop.DoDragDrop(this.treeview,data, DragDropEffects.Copy);
                                }
                            }
                      
                                _treeItemIsDrag = false;
                        }
                    }
                };


            this.canvas.Drop += (s, e) =>
            {
                var data = e.Data;
                if(data.GetDataPresent(typeof(RegistNodeItem))) {
                RegistNodeItem info=data.GetData(typeof(RegistNodeItem)) as RegistNodeItem;
                    var instance= Activator.CreateInstance(info.NodeType) as NodeBase;
                    this.AddNode(instance, e.GetPosition(this.canvas));
                  
                }

            };
           

            this.treeview.ItemsSource = _nodes;

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
                                    else if(this.GetSelectElement() is NodeBase node)
                                    {
                                        ContextMenu cm = new ContextMenu();
                                        MenuItem deleteItem = new MenuItem();
                                        deleteItem.Header = "删除";
                                        deleteItem.Click += (x, xx) =>
                                        {

                                            this.RemoveNode(node);

                                            //line.Disconnect();
                                            //this.RemoveLine(line);
                                            //this.ClearSelectElement();

                                        };
                                        cm.Items.Add(deleteItem);
                                        node.ContextMenu = cm;
                                    }
                                }

                                return HitTestResultBehavior.Stop;
                            }
                        }
                        return HitTestResultBehavior.Continue;
                    }, new PointHitTestParameters(p));

                }
                else if(e.MouseDevice.MiddleButton==MouseButtonState.Pressed)
                {
                    this._canvasStartPoint = e.GetPosition(this.canvasBorder);
                }
            
            };
            this.canvas.MouseMove += (s, e) =>
            {
                if(e.MouseDevice.LeftButton==MouseButtonState.Pressed)
                {
                    var p = e.MouseDevice.GetPosition((UIElement)this.canvas);
                    this.Move(p);
                }
                else if(e.MouseDevice.MiddleButton==MouseButtonState.Pressed)
                {
                    Point currentPoint = e.GetPosition(canvasBorder);
                    Vector calculatePoint = currentPoint - this._canvasStartPoint;
                    TransformGroup tg = this.canvas.RenderTransform as TransformGroup;
                    if(tg!=null)
                    {
                        tg.Children.Add(new TranslateTransform(calculatePoint.X,calculatePoint.Y));
                        this._canvasStartPoint=currentPoint;
                    }
                }
            };

            this.canvas.MouseUp += (s, e) =>
            {
                if (e.MouseDevice.LeftButton == MouseButtonState.Released)
                {
                    var p = e.GetPosition((UIElement)this);
                    this.ReleaseMouse(p);
                }
            };

            this.canvas.MouseWheel += (s, e) =>
            {

                Point p=e.GetPosition(this.canvasBorder);

                double scale = ((double)e.Delta) / 1000.0 + 1.0;
                TransformGroup tg=this.canvas.RenderTransform as TransformGroup; 
                 if(tg!=null)
                {
                    tg.Children.Add(new ScaleTransform(scale, scale, p.X,p.Y));
                }

             

            };



        }



        public override void AddNode(NodeBase node,Point position)
        {
            node.Canvas = this;
            node.Position =position;
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
            foreach (var item in node.GetPorts())
            {
                for (int i=0; i<item.ConnectedLines.Count;i++)
                {
                    

              
                    foreach (var tailport in item.ConnectedLines[i].TailNode.GetPorts())
                    {
                        if(tailport.ConnectedLines.Contains(item.ConnectedLines[i]))
                        {
                            tailport.RemoveLine(item.ConnectedLines[i]);
                        }
                    }
                    item.ConnectedLines[i].TailNode = null;
                    
                    foreach (var headerport in item.ConnectedLines[i].HeaderNode.GetPorts())
                    {
                        if (headerport.ConnectedLines.Contains(item.ConnectedLines[i]))
                        {
                            headerport.RemoveLine(item.ConnectedLines[i]);
                        }
                    }

                    item.ConnectedLines[i].HeaderNode = null;
                    this.canvas.Children.Remove(item.ConnectedLines[i]);
                   
                }
                item.ConnectedLines.Clear();
            }
        }
    }
}
