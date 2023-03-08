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
    /// WPFControl_TextButtonWithImage.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_TextButtonWithImage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public WPFControl_TextButtonWithImage()
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

        private Brush _TextBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        public Brush TextBrush
        {
            get
            {
                return _TextBrush;
            }
            set
            {
                _TextBrush = value;
                OnPropertyChanged("TextBrush");
            }
        }
        #endregion

        #region 图标
        private ImageSource _Source;
        public ImageSource Source
        {
            get
            {
                return _Source;
            }
            private set
            {
                _Source = value;
                OnPropertyChanged("Source");
            }
        }
        private ImageSource _RegularSource;
        public ImageSource RegularSource
        {
            get
            {
                return _RegularSource;
            }
            set
            {
                _RegularSource = value;
                Source = value;
            }
        }
        bool _MouseOverSourceSet = false;
        bool _MouseDownSourceSet = false;
        private ImageSource _MouseOverSource;
        private ImageSource _MouseDownSource;
        public ImageSource MouseOverSource
        {
            get
            {
                return _MouseOverSource;
            }
            set
            {
                _MouseOverSource = value;
                _MouseOverSourceSet = true;
            }
        }
        public ImageSource MouseDownSource
        {
            get
            {
                return _MouseDownSource;
            }
            set
            {
                _MouseDownSource = value;
                _MouseDownSourceSet = true;
            }
        }
        #endregion

        #region 尺寸
        private double _ImageWidth = 32;
        private double _ImageHeight = 32;
        private double _ButtonHeight = 60;
        private double _ButtonWidth = 60;
        public double ImageWidth
        {
            get
            {
                return _ImageWidth;
            }
            set
            {
                _ImageWidth = value;
                OnPropertyChanged("ImageWidth");
            }
        }
        public double ImageHeight
        {
            get
            {
                return _ImageHeight;
            }
            set
            {
                _ImageHeight = value;
                OnPropertyChanged("ImageHeight");
            }
        }
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
                _ButtonHeight = value;
                OnPropertyChanged("ButtonHeight");
            }
        }
        #endregion

        #region 背景
        bool _RegularBackgroundSet = false;
        bool _MouseOverBackgroundSet = false;
        bool _MouseDownBackgroundSet = false;
        private Brush _Background;
        public Brush CurrentBackground
        {
            get
            {
                return _Background;
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
        public Brush MouseDownBackground
        {
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

        #region
        /// <summary>
        /// 图片位置
        /// </summary>
        HorizontalAlignment _ImageHorizontal = HorizontalAlignment.Right;
        public HorizontalAlignment ImageHorizontal
        {
            get
            {
                return _ImageHorizontal;
            }
            set
            {
                _ImageHorizontal = value;
                switch (value)
                {
                    case HorizontalAlignment.Left:
                        Grid.SetColumn(image, 0);
                        Grid.SetColumn(text, 1);
                        break;
                    case HorizontalAlignment.Right:
                        Grid.SetColumn(image, 1);
                        Grid.SetColumn(text, 0);
                        break;
                    default:
                        break;
                }
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
            if (_MouseDownSourceSet)
            {
                Source = MouseDownSource;
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
                if (_MouseDownSourceSet)
                {
                    Source = RegularSource;
                }
                TextButtonClick?.Invoke(this, e);

            }
        }

        public event MouseButtonEventHandler TextButtonClick;
    }
}
