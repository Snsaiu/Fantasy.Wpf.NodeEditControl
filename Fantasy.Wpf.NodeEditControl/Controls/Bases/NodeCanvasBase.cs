using Fantasy.Wpf.NodeEditControl.Data;
using Fantasy.Wpf.NodeEditControl.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Fantasy.Wpf.NodeEditControl.Controls.Bases
{

    public delegate void RegistNodeDelegate(RegistNodeItem node);

    public abstract class NodeCanvasBase : UserControl
    {

        public event RegistNodeDelegate RegistNodeEvent;

        /// <summary>
        /// 被选中
        /// </summary>
        private ICanvasElementBase _selectElement = null;

        public abstract void AddNode(NodeBase node, Point position);

        protected abstract void RemoveNodeElement(NodeBase node);

        public abstract void CreateLine(LineBase line);

        protected abstract void RemoveLineElement(LineBase line);

        protected List<RegistNodeItem> Nodes { get; private set; } = new List<RegistNodeItem>();

        public void RegistNode(Type node, string nodeName, ImageSource logo = null, string groupName = "通用")
        {

            if (Nodes.Exists(x => x.GroupName == groupName && x.NodeName == nodeName) == false)
            {
                if (logo == null)
                    logo = Tools.LoadBitmapFromResource("Resouces\\Images\\nullLogo.png");
                var item = new RegistNodeItem { GroupName = groupName, NodeName = nodeName, NodeType = node, NodeLogo = logo };
                Nodes.Add(item);
                RegistNodeEvent?.Invoke(item);
            }

        }


        public void ClearSelectElement()
        {
            _selectElement = null;
        }

        public ICanvasElementBase GetSelectElement()
        {
            return _selectElement;
        }


        /// <summary>
        /// 临时
        /// </summary>
        private LineBase _newLine = null;



        protected bool HitElement(FrameworkElement element, Point point)
        {
            VisualTreeHelper.HitTest(element, null, resultCallback: (x) =>
            {
                if (x.VisualHit != null)
                {
                    bool hitRes = hitTest((FrameworkElement)x.VisualHit, point);
                    if (hitRes == true)
                    {
                        return HitTestResultBehavior.Stop;
                    }
                }
                return HitTestResultBehavior.Continue;
            }, new PointHitTestParameters(point));

            return _selectElement != null ? true : false;
        }

        /// <summary>
        /// 当鼠标点击的时候选择的控件
        /// </summary>
        /// <param name="element"></param>
        private bool hitTest(FrameworkElement element, Point position)
        {

            while (true)
            {
                if (element != null)
                {
                    if (element is LineBase l)
                    {
                        _selectElement = l;
                        break;
                    }
                    else if (element is PortBase p)
                    {
                        _selectElement = p;

                        _newLine = p.CreateLine();
                        if (_newLine != null)
                        {
                            _newLine.Canvas = this;
                            CreateLine(_newLine);
                            p.AddLine(_newLine);


                            if (p.PortType == Enums.PortType.Output)
                            {
                                _newLine.UpdateEndPoint(position);
                                _newLine.ValidateConnectStartPort(element);
                            }
                            else
                            {
                                _newLine.UpdateStartPoint(position);
                            }
                        }

                        break;
                    }
                    else if (element is NodeBase n)
                    {
                        _selectElement = n;
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
            if (_selectElement == null)
            { return false; }
            return true;
        }


        public virtual void ReleaseMouse(Point point)
        {
            if (_selectElement != null)
            {


                if (_selectElement is PortBase p)
                {
                    if (p.PortType == Enums.PortType.Output)
                    {
                        VisualTreeHelper.HitTest(this, null, resultCallback: (x) =>
                        {
                            if (x.VisualHit != null)
                            {
                                _newLine.ValidateConnectEndPort(x.VisualHit);
                            }

                            return HitTestResultBehavior.Continue;

                        }, new PointHitTestParameters(point));


                        if (_newLine.TailNode == null)
                        {
                            RemoveLine(_newLine);
                            p.RemoveLine(_newLine);

                        }
                        _newLine = null;

                    }
                }

            }
            _selectElement = null;
        }


        /// <summary>
        /// 鼠标移动如果有控件被选择那么跟着鼠标移动
        /// </summary>
        /// <param name="point"></param>
        public virtual void Move(Point point)
        {

            if (_selectElement != null)
            {
                if (_selectElement is NodeBase nb)
                {

                    point.X = point.X - nb.Width / 2;
                    point.Y = point.Y - nb.Height / 2;
                    nb.Position = point;
                    nb.UpdateNodePortWithLinesPosition();
                }
                else if (_selectElement is PortBase pb)
                {
                    if (_newLine != null)
                    {
                        if (pb.PortType == Enums.PortType.Output)
                        {
                            _newLine.UpdateEndPoint(point);
                        }
                        else if (pb.PortType == Enums.PortType.Input)
                        {
                            _newLine.UpdateStartPoint(point);
                        }
                    }


                }
            }


        }

        /// <summary>
        /// 移除节点
        /// </summary>
        /// <param name="node"></param>
        public void RemoveNode(NodeBase node)
        {
            RemoveNodeElement(node);
            foreach (var item in node.GetPorts())
            {
                for (int i = 0; i < item.ConnectedLines.Count; i++)
                {
                    LineBase line = item.ConnectedLines[i];
                    foreach (var tailport in line.TailNode.GetPorts())
                    {
                        if (tailport.ConnectedLines.Contains(line))
                        {
                            tailport.RemoveLine(line);
                           
                        }
                    }
                    line.TailNode = null;

                    foreach (var headerport in line.HeaderNode.GetPorts())
                    {
                        if (headerport.ConnectedLines.Contains(line))
                        {
                            headerport.RemoveLine(line);
                           
                        }
                    }

                    line.HeaderNode = null;
                    RemoveLineElement(line);

                }
                item.ConnectedLines.Clear();
            }
            ClearSelectElement();
        }


        public void RemoveLine(LineBase line)
        {
            line.Disconnect();
            RemoveLineElement(line);
            ClearSelectElement();
        }
    }
}
