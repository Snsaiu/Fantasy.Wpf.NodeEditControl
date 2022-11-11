using Fantasy.Wpf.NodeEditControl.Data;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace Fantasy.Wpf.NodeEditControl.Controls
{
    public abstract class NodeBase : UserControl
    {


        private List<PortBase> _ports;

        protected abstract List<PortBase> GetPorts();


        public  virtual OutputData Calculate()
        {

            return new OutputData();
        }

        protected abstract Size GetNodeSize();

        /// <summary>
        /// get node name
        /// </summary>
        /// <returns></returns>
        protected abstract string GetNodeName();


        /// <summary>
        /// get support type
        /// </summary>
        /// <returns></returns>
        public abstract List<Type> SupportInputTypes();


        public abstract List<Type> SupportOutputTypes();


        public NodeBase()
        {

            

            this.Loaded += (s, e) =>
            {

              
                this._ports = this.GetPorts();

                if(this._ports!=null)
                {
                    foreach(PortBase port in this._ports)
                    {
                        port.Node = this;
                    }
                }

                var sonChild = this.Content as FrameworkElement;
                this.Content = null;
                NodeContainerBase nc = new NodeContainer(this.GetNodeSize(),this.IsCalculateNode,this.GetNodeName());
                nc.CalculateEvent += () =>
                {
                    var data= this.Calculate();
                    var outputport=this._ports.FirstOrDefault(x=>x.PortType==Enums.PortType.Output);
                    if(outputport!=null)
                    {
                        outputport.Data = data;
                    }
                    else
                    {
                        throw new NullReferenceException(); 
                    }
                };
                nc.SetContent(sonChild);
                this.Content = nc;
               
                if (double.IsNaN(this.Width))
                {
                    this.Width = this.GetNodeSize().Width;
                }
                if (double.IsNaN(this.Height))
                {
                    this.Height = this.GetNodeSize().Height;
                }

            };
         
            this.MouseMove += (s, e) =>
            {
                if(e.MouseDevice.LeftButton==System.Windows.Input.MouseButtonState.Pressed)
                {
        

                    var p = e.MouseDevice.GetPosition((UIElement)this.Parent);
                    InkCanvas.SetLeft(this,p.X-this.Width/2);
                    InkCanvas.SetTop(this, p.Y-this.Height/2);
                }

                if(this._ports!=null)
                {
                    foreach (var item in this._ports)
                    {
                        item.UpdateLinesPosition();
                    }
                }
          
                

            };
        }



        public bool IsCalculateNode
        {
            get { return (bool)GetValue(IsCalculateNodeProperty); }
            set { SetValue(IsCalculateNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCalculateNode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCalculateNodeProperty =
            DependencyProperty.Register("IsCalculateNode", typeof(bool), typeof(NodeBase), new PropertyMetadata(true));




        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register("Position", typeof(Point), typeof(NodeBase), new PropertyMetadata(new Point(0, 0), propertyChangedCallback: PositionChanged));

        private static void PositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var node = d as NodeBase;
            var p = (Point)e.NewValue;
            InkCanvas.SetLeft(node, p.X);
            InkCanvas.SetTop(node, p.Y);
        }
    }
}
