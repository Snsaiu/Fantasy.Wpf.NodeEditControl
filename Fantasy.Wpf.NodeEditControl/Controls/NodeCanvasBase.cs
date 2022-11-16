using Fantasy.Wpf.NodeEditControl.Data;
using Fantasy.Wpf.NodeEditControl.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fantasy.Wpf.NodeEditControl.Controls
{ 

  public  delegate void RegistNodeDelegate(RegistNodeItem node);

    public abstract class NodeCanvasBase:UserControl
    {

    public event RegistNodeDelegate RegistNodeEvent;

        /// <summary>
        /// 被选中
        /// </summary>
        private ICanvasElementBase _selectElement = null;

        public abstract void AddNode(NodeBase node);

        public abstract void RemoveNode(NodeBase node);

        public abstract void CreateLine(LineBase line);

        public abstract void RemoveLine(LineBase line);

        protected List<RegistNodeItem> Nodes { get; private set; } = new List<RegistNodeItem>();

        public void RegistNode(Type node,string nodeName,ImageSource logo=null,string groupName="通用")
        {
        
            if(this.Nodes.Exists(x=>x.GroupName==groupName&&x.NodeName==nodeName)==false)
            {
                if(logo==null)
                    logo= Tools.LoadBitmapFromResource("Resouces\\Images\\nullLogo.png");
            var item = new RegistNodeItem { GroupName = groupName, NodeName = nodeName, NodeType = node, NodeLogo = logo };
                this.Nodes.Add(item);
            this.RegistNodeEvent?.Invoke(item);
            }

        }
        

        public void ClearSelectElement()
        {
            this._selectElement = null;
        }

        public ICanvasElementBase GetSelectElement()
        {
            return this._selectElement;
        }


        /// <summary>
        /// 临时
        /// </summary>
        private LineBase _newLine = null;

        /// <summary>
        /// 当鼠标点击的时候选择的控件
        /// </summary>
        /// <param name="element"></param>
        public bool  HitElement(FrameworkElement element,Point position)
        {

            while (true)
            {
                if (element != null)
                {
                    if (element is LineBase l)
                    {
                        this._selectElement = l;
                        break;
                    }
                    else if(element is PortBase p)
                    {
                        this._selectElement = p;

                        this._newLine = p.CreateLine();
                        if (this._newLine != null)
                        {
                            this._newLine.Canvas = this;
                            this.CreateLine(this._newLine);
                            p.AddLine(this._newLine);


                            if (p.PortType == Enums.PortType.Output)
                            {
                                this._newLine.UpdateEndPoint(position);
                                this._newLine.ValidateConnectStartPort(element);
                            }
                            else
                            {
                                this._newLine.UpdateStartPoint(position);
                            }
                        }

                        break;
                    }
                    else if(element is NodeBase n)
                    {
                        this._selectElement = n;
                        break;
                    }
                    else
                    {

                        element = element.Parent as FrameworkElement;
                    }
                }
                else
                {
                    break;
                }
            }
            if(this._selectElement==null)
            { return false; }
            return true;
        }


        public virtual void ReleaseMouse(Point point)
        {
            if(this._selectElement!=null)
            {


                if(this._selectElement is PortBase p)
                {
                     if(p.PortType==Enums.PortType.Output)
                    {
                        VisualTreeHelper.HitTest((UIElement)this, null, resultCallback: (x) =>
                        {
                            if (x.VisualHit != null)
                            {
                                this._newLine.ValidateConnectEndPort(x.VisualHit);
                            }

                            return HitTestResultBehavior.Continue;

                        }, new PointHitTestParameters(point));
                    

                    if (this._newLine.TailNode==null)
                        {
                            this.RemoveLine(this._newLine);
                            p.RemoveLine(this._newLine);
                          
                        }
                        this._newLine = null;

                    }
                }

            }
            this._selectElement = null;
        }
        

        /// <summary>
        /// 鼠标移动如果有控件被选择那么跟着鼠标移动
        /// </summary>
        /// <param name="point"></param>
        public virtual void Move(Point point) { 
        
            if(this._selectElement!=null)
            {
                if(this._selectElement is NodeBase nb)
                {

                   point.X=point.X-nb.Width/2;
                    point.Y=point.Y-nb.Height/2;
                    nb.Position = point;
                    nb.UpdateNodePortWithLinesPosition();
                }
                else if(this._selectElement is PortBase pb)
                {
                    if(this._newLine!=null)
                    {
                        if(pb.PortType==Enums.PortType.Output)
                        {
                            this._newLine.UpdateEndPoint(point);
                        }
                        else if(pb.PortType==Enums.PortType.Input)
                        {
                            this._newLine.UpdateStartPoint(point);
                        }
                    }


                }
            }

           
        }
    }
}
