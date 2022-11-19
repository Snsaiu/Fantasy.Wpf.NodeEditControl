using Fantasy.Wpf.NodeEditControl.Data;
using Fantasy.Wpf.NodeEditControl.Helpers;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Fantasy.Wpf.NodeEditControl.Controls.Bases
{
    public abstract class NodeBase : UserControl, ICanvasElementBase
    {

        /// <summary>
        /// 当有属性发生更改的时候，可以通知所连接的节点进行计算
        /// </summary>
        public void NotifyCalculate()
        {
            if (this._ports .Count!= 0)
            {
               var outputPorts=  this._ports.Where(x => x.PortType == Enums.PortType.Output).ToList();
               foreach (var outputPort in outputPorts)
               {
                   var connectLines = outputPort.ConnectedLines;
                   if (connectLines.Count > 0)
                   {
                       foreach (var connectLine in connectLines)
                       {
                           var tailNode = connectLine.TailNode;
                           if (tailNode != null)
                           {
                                tailNode.NotifyCalculate();
                           }
                          
                       }
                   }
                   else
                   {
                       var data = this.Calculate();
                       var outputport = _ports.FirstOrDefault(x => x.PortType == Enums.PortType.Output);
                       if (outputport != null)
                       {
                           outputport.Data = data;
                       }
                       else
                       {
                           throw new NullReferenceException();
                       }
                    }
               }

            }
        }

        private List<PortBase> _ports;

        public virtual ImageSource GetLogo()
        {
            return Tools.LoadBitmapFromResource("Resouces\\Images\\nullLogo.png");
        }


        public abstract List<PortBase> GetPorts();

        public NodeCanvasBase Canvas { get; set; }

        /// <summary>
        /// setting panel saveed data
        /// </summary>
        private object _settingDataValue = null;

        public  OutputData Calculate()
        {
            if(this.FreezeCalculate)
            {
                if(this._freezeData == null)
                {
                    this._freezeData = this.CalculateImpl(this._settingDataValue);
                }
            }
            else
            {
                this._freezeData= this.CalculateImpl(this._settingDataValue);
            }
     
            return this._freezeData;    

        }

        /// <summary>
        /// 计算
        /// </summary>
        /// <returns></returns>
        protected abstract OutputData CalculateImpl(object data);

        protected abstract Size GetNodeSize();

        /// <summary>
        /// get node name
        /// </summary>
        /// <returns></returns>
        protected abstract string GetNodeName();

        /// <summary>
        /// get node summary document
        /// </summary>
        /// <returns></returns>
        public abstract string GetNodeSummary();


        protected virtual NodeContainerBase CreateNodeContainerStyle()
        {
            return  FantasyNodeGlobalSetting.ConfigFantasy.SetNodeContainerStyle(); 
        }


        /// <summary>
        /// get support type
        /// </summary>
        /// <returns></returns>
        public abstract List<Type> SupportInputTypes();


        public abstract List<Type> SupportOutputTypes();


        private OutputData _freezeData = null;


        public bool FreezeCalculate
        {
            get { return (bool)GetValue(FreezeCalculateProperty); }
            set { SetValue(FreezeCalculateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FreezeCalculate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FreezeCalculateProperty =
            DependencyProperty.Register("FreezeCalculate", typeof(bool), typeof(NodeBase), new PropertyMetadata(false));




        public NodeBase()
        {



            Loaded += (s, e) =>
            {


                _ports = GetPorts();

                if (_ports != null)
                {
                    foreach (PortBase port in _ports)
                    {
                        port.Node = this;
                        port.Canvas = Canvas;
                    }
                }

                var sonChild = Content as FrameworkElement;
                Content = null;
                NodeContainerBase nc = CreateNodeContainerStyle();
                if (nc == null)
                    throw new NullReferenceException();

                nc.SetNodeName(GetNodeName());
                nc.IsCalculateNode(IsCalculateNode);
                nc.SetNodeSize(GetNodeSize());

                nc.ShowSummaryEvent += () =>
                {

                    NodeInfoDialogBase nodeInfoDialogBase =
                        FantasyNodeGlobalSetting.ConfigFantasy.SetNodeInfoDialogStyle();
                    nodeInfoDialogBase.ShowInTaskbar = false;
                    nodeInfoDialogBase.InitShow(GetNodeName(),this.GetLogo(),this.GetNodeSummary());
                    nodeInfoDialogBase.ShowDialog();
                };
                nc.SetFreezeCalculateStateEvent += (state) =>
                {
                    this.FreezeCalculate = state;
                };
                
                nc.CalculateEvent += () =>
                {

                    var data = this.Calculate();
                    var outputport = _ports.FirstOrDefault(x => x.PortType == Enums.PortType.Output);
                    if (outputport != null)
                    {
                        outputport.Data = data;
                    }
                    else
                    {
                        throw new NullReferenceException();
                    }
                };

                nc.SetSettingPanelEvent += () =>
                {
                    SettingDialogBase settingDialog = FantasyNodeGlobalSetting.ConfigFantasy.SetSettingDialogStyle();
                    settingDialog.SetSettingBaseInfo(this.GetNodeName(),this.GetLogo());
                    var settingContent = this.SetSettingContent();
                    settingDialog.SetContent(settingContent);
                    settingContent.UpdateEvent += (data) =>
                    {
                        this._settingDataValue = data;
                        this.Calculate();

                    };
                    settingDialog.Show();

                };


                nc.SetContent(sonChild);
                Content = nc;

                MouseEnter += (s, e) =>
                {
                    nc.SelectedStyle();
                };
                MouseLeave += (s, e) =>
                {
                    nc.DefaultStyle();
                };

                if (double.IsNaN(Width))
                {
                    Width = GetNodeSize().Width;
                }
                if (double.IsNaN(Height))
                {
                    Height = GetNodeSize().Height;
                }

            };

        }


        public void UpdateNodePortWithLinesPosition()
        {
            if (_ports != null)
            {
                foreach (var item in _ports)
                {
                    item.UpdateLinesPosition();
                }
            }
        }

        /// <summary>
        ///  set setting panel content
        /// </summary>
        /// <returns></returns>
        public abstract SettingPanelBase SetSettingContent();


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
