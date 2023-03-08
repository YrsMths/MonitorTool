using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace VS_Presentation.WPFControls
{
    /// <summary>
    /// WPFControl_ImageButton.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_TextButton : UserControl,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public WPFControl_TextButton()
        {
            InitializeComponent();
        }
        #region 文字

        public static readonly DependencyProperty TextContentProperty = DependencyProperty.Register("TextContent", typeof(string), typeof(WPFControl_TextButton));
        public string TextContent
        {
            get
            {
                return (string)GetValue(TextContentProperty);
            }
            set
            {
                SetValue(TextContentProperty, value);
            }
        }

        bool _UnabledTextBrushSet = false;

        private Brush _TextBrush = new SolidColorBrush(Color.FromArgb(255, 0 , 0 ,0));
        public Brush TextBrush
        {
            get
            {
                return _UnabledTextBrushSet ? (IsEnabled ? _TextBrush : __UnabledTextBrush) : _TextBrush;
            }
            set
            {
                _TextBrush = value;
                OnPropertyChanged("TextBrush");
            }
        }

        private Brush __UnabledTextBrush;
        public Brush _UnabledTextBrush
        {
            get
            {
                return __UnabledTextBrush;
            }
            set
            {
                _UnabledTextBrush = value;
                _UnabledTextBrushSet = true;
                this.IsEnabledChanged += (obj, e) => { OnPropertyChanged("TextBrush"); };
            }
        }

        #endregion
        #region 尺寸
        private double _ButtonHeight=60;
        private double _ButtonWidth=60;
        public double ButtonWidth
        {
            get
            {
                return _ButtonWidth;
            }
            set
            {
                _ButtonWidth = value;
                OnPropertyChanged("ButtonWidth");
            }
        }
        public double ButtonHeight
        {
            get
            {
                return _ButtonHeight;
            }
            set
            {
                _ButtonHeight= value;
                OnPropertyChanged("ButtonHeight");
            }
        }
#endregion
        #region 背景
        bool _RegularBackgroundSet = false;
        bool _MouseOverBackgroundSet = false;
        bool _MouseDownBackgroundSet = false;
        bool _UnabledBackgroundSet = false;
        private Brush _Background;
        public Brush CurrentBackground
        {
            get
            {
                return _UnabledBackgroundSet ? (IsEnabled ? _Background : _UnabledBackground) : _Background;
            }
            private set
            {
                _Background = value;
                OnPropertyChanged("CurrentBackground");
            }
        }

        private Brush _RegularBackground;
        private Brush _MouseOverBackground;
        private Brush _MouseDownBackground;
        private Brush _UnabledBackground;
        public Brush RegularBackground
        {
            get
            {
                return _RegularBackground;
            }
            set
            {
                _RegularBackground = value;
                CurrentBackground = value;
                _RegularBackgroundSet = true;
            }
        }
        public Brush MouseOverBackground
        {
            get
            {
                return _MouseOverBackground;
            }
            set
            {
                _MouseOverBackground = value;
                _MouseOverBackgroundSet = true;
            }
        }
        public Brush MouseDownBackground {
            get
            {
                return _MouseDownBackground;
            }
            set
            {
                _MouseDownBackground = value;
                _MouseDownBackgroundSet = true;
            }
        }

        public Brush UnabledBackground
        {
            get
            {
                return _UnabledBackground;
            }
            set
            {
                _UnabledBackground = value;
                _UnabledBackgroundSet = true;
                this.IsEnabledChanged += (obj, e) => { OnPropertyChanged("CurrentBackground"); };
            }
        }
        
        #endregion
        #region 边框
        private double _BorderRadius = 0;
        public double BorderRadius
        {
            get
            {
                return _BorderRadius;
            }
            set
            {
                _BorderRadius = value;
                OnPropertyChanged("BorderRadius");
            }
        }
        private Brush _BorderBrush = new SolidColorBrush(Color.FromArgb(40, 0, 0, 0));
        public Brush BorderBrush
        {
            get
            {
                    return _BorderBrush;
            }
            set
            {
                _BorderBrush = value;
                OnPropertyChanged("BorderBrush");
            }
        }
        private Thickness _BorderThickness = new Thickness(1);
        public Thickness ButtonBorderThickness
        {
            get
            {
                    return _BorderThickness;
            }
            set
            {
                _BorderThickness = value;
                OnPropertyChanged("BorderThickness");
            }
        }
        #endregion
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Focus();
            if (_MouseOverBackgroundSet)
            {
                CurrentBackground = MouseOverBackground;
            }
        }
        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {

            if (_MouseOverBackgroundSet)
            {
                if (_RegularBackgroundSet)
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        CurrentBackground = MouseDownBackground;
                    }
                    else
                    {
                        CurrentBackground = RegularBackground;
                    }
                }
                else
                {
                    CurrentBackground = new SolidColorBrush();
                }
            }
        }
       
        private void Border_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.CaptureMouse();
            if (_MouseDownBackgroundSet)
            {
                CurrentBackground = MouseDownBackground;
            }
        }

        private void Border_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                this.ReleaseMouseCapture();
                if (_MouseDownBackgroundSet)
                {
                    if (_RegularBackgroundSet)
                    {
                        CurrentBackground = MouseOverBackground;
                    }
                    else
                    {
                        CurrentBackground = new SolidColorBrush();
                    }
                }
                
                RoutedEventArgs args = new RoutedEventArgs(TextButtonClickEvent, this);
                //引用自定义路由事件
                RaiseEvent(args);
            }
        }

        //public event MouseButtonEventHandler TextButtonClick;

        /// <summary>
        /// 声明路由事件
        /// 参数:要注册的路由事件名称，路由事件的路由策略，事件处理程序的委托类型(可自定义)，路由事件的所有者类类型
        /// </summary>
        public static readonly RoutedEvent TextButtonClickEvent = EventManager.RegisterRoutedEvent("TextButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WPFControl_TextButton));

        /// <summary>
        /// 处理各种路由事件的方法
        /// </summary>
        public event RoutedEventHandler TextButtonClick
        {
            //将路由事件添加路由事件处理程序
            add { AddHandler(TextButtonClickEvent, value); }
            //从路由事件处理程序中移除路由事件
            remove { RemoveHandler(TextButtonClickEvent, value); }
        }
    }
}
