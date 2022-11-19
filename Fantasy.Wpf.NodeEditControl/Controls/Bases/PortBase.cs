using Fantasy.Wpf.NodeEditControl.Data;
using Fantasy.Wpf.NodeEditControl.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Fantasy.Wpf.NodeEditControl.Controls.Bases
{
    public abstract class PortBase : UserControl, ICanvasElementBase
    {

        /// <summary>
        /// when click port ,this field will save line instance ,when line connect other port,this field will be cleand
        /// 
        /// </summary>
        private LineBase _createNewLine = null;

        public PortBase()
        {
            ConnectedLines = new List<LineBase>();


            Loaded += (s, e) =>
            {

                GetPortMark().MouseDown += (s, e) =>
                {
                    if (e.MiddleButton == MouseButtonState.Pressed)
                        CreateLine();
                };

            };

        }




        /// <summary>
        /// get port mask,when line connect usercontrol,the mark will be connected
        /// </summary>
        /// <returns></returns>
        protected abstract FrameworkElement GetPortMark();


        public Point GetSelfPoint()
        {

            FrameworkElement fe = this;
            while (true)
            {
                if (fe.Parent == null)
                {

                    break;
                }

                else if (fe.Parent is InkCanvas c)
                {

                    var mark = GetPortMark();
                    var p = mark.TransformToAncestor(c).Transform(new Point(0, 0));
                    p.Y = p.Y + mark.Height / 2;
                    p.X = p.X + mark.Width / 2;

                    return p;
                }

                fe = fe.Parent as FrameworkElement;


            }

            return new Point(0, 0);

        }

        /// <summary>
        ///  port data
        /// </summary>
        public OutputData Data { get; set; }

        public void UpdateLinesPosition()
        {
            foreach (var item in ConnectedLines)
            {

                if (PortType == PortType.Input)
                {
                    item.EndPoint = GetSelfPoint();
                }
                else
                {
                    item.StartPiont = GetSelfPoint();
                }

            }
        }

        public NodeBase Node { get; set; }

        public PortType PortType
        {
            get { return (PortType)GetValue(PortTypeProperty); }
            set { SetValue(PortTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PortType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PortTypeProperty =
            DependencyProperty.Register("PortType", typeof(PortType), typeof(PortBase), new PropertyMetadata(PortType.Input));

        public void AddLine(LineBase line)
        {
            if (line == null) throw new ArgumentNullException();
            if (ConnectedLines.Contains(line)) return;
            ConnectedLines.Add(line);
        }

        public void ClearLines()
        {
            ConnectedLines.Clear();
        }
        public void RemoveLine(LineBase line)
        {
            if (ConnectedLines.Contains(line))
            {
                ConnectedLines.Remove(line);
            }
        }

        /// <summary>
        /// when click ,port will create a line 
        /// </summary>
        public LineBase CreateLine()
        {
            // determined the port is input type or output type

            if (PortType == PortType.Input)
            {


            }
            else if (PortType == PortType.Output)
            {
                //if (this._createNewLine == null)
                //{
                var line = CreateOutPutLine();
                _createNewLine = line;
                if (_createNewLine != null)
                {
                    _createNewLine.UpdateStartPoint(GetSelfPoint());
                    // var endp = this.GetSelfPoint();
                    //endp.X = endp.X + 15;
                    // endp.Y= endp.Y + 10;
                    // this._createNewLine.UpdateEndPoint(endp);
                    // this._createNewLine.MouseMove += createNewLineMove;
                }
                // this.Canvas.CreateLine(this._createNewLine);
                // this.AddLine(line);
                //}
            }
            return _createNewLine;


        }

        private void createNewLineMove(object sender, MouseEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                var endp = e.MouseDevice.GetPosition(Canvas);

                endp.X = endp.X + 15;
                endp.Y = endp.Y + 10;
                if (_createNewLine != null)
                {

                    _createNewLine.UpdateEndPoint(endp);
                }
            }
            else if (e.MiddleButton == MouseButtonState.Released)
            {
                _createNewLine.MouseMove -= createNewLineMove;
                if (_createNewLine.TailNode == null)
                {
                    Canvas.RemoveLine(_createNewLine);
                    ConnectedLines.Remove(_createNewLine);
                    _createNewLine = null;
                }
            }

        }



        /// <summary>
        /// set the port show line style 
        /// </summary>
        /// <returns></returns>
        protected virtual LineBase CreateOutPutLine()
        {
            return FantasyNodeGlobalSetting.ConfigFantasy.SetLineStyle();
        }


        public List<LineBase> ConnectedLines { get; private set; }
        public NodeCanvasBase Canvas { get; set; }
    }

}
