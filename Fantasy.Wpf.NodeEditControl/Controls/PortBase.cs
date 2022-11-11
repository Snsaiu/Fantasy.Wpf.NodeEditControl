﻿using Fantasy.Wpf.NodeEditControl.Data;
using Fantasy.Wpf.NodeEditControl.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fantasy.Wpf.NodeEditControl.Controls
{
    public abstract class PortBase:UserControl
    {

        public PortBase()
        {
            this.ConnectedLines = new List<LineBase>();
        }

        /// <summary>
        /// get port mask,when line connect usercontrol,the mark will be connected
        /// </summary>
        /// <returns></returns>
        protected abstract FrameworkElement GetPortMark();
        

        public Point GetSelfPoint() {

            FrameworkElement fe = this;
            while (true)
            {    if (fe.Parent == null)
                {

                    break;
                }

                else if (fe.Parent is InkCanvas c)
                    {
                    
                    var mark=this.GetPortMark();
                   var p=  mark.TransformToAncestor(c).Transform(new Point(0,0));
                    p.Y = p.Y + mark.Height / 2;
                    p.X = p.X + mark.Width / 2;
                   
                        return p;
                }
         
                    fe = fe.Parent as FrameworkElement;

                
            }

            return new Point(0,0);
           
        }

        /// <summary>
        ///  port data
        /// </summary>
        public OutputData Data { get; set; }

        public void UpdateLinesPosition()
        {
            foreach (var item in this.ConnectedLines)
            {

                if(this.PortType==PortType.Input)
                {
                    item.EndPoint=this.GetSelfPoint();
                }
                else
                {
                    item.StartPiont=this.GetSelfPoint();
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
            if(this.ConnectedLines.Contains(line)) return;
            this.ConnectedLines.Add(line);
        }
        public void RemoveLine(LineBase line)
        {
            if(this.ConnectedLines.Contains(line))
            {
                this.ConnectedLines.Remove(line);
            }
        }

        public List<LineBase> ConnectedLines { get; private set; }
    }

}
