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
    /// WPFControl_TextBoxWithButton.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_TextBoxWithButton : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public WPFControl_TextBoxWithButton()
        {
            InitializeComponent();
        }
        
        public ImageSource RegularSource
        {
            get
            {
                return ImageButton.RegularSource;
            }
            set
            {
                ImageButton.RegularSource = value;
            }
        }

        public Brush BoxBorderBrush
        {
            get
            {
                return Border.BorderBrush;
            }
            set
            {
                Border.BorderBrush = value;
            }
        }

        public Thickness BoxBorderThickness
        {
            get
            {
                return Border.BorderThickness;
            }
            set
            {
                Border.BorderThickness = value;
            }
        }

        Cursor _ButtonCursor;
        public Cursor ButtonCursor
        {
            get
            {
                return _ButtonCursor;
            }
            set
            {
                _ButtonCursor = value;
                OnPropertyChanged("ButtonCursor");
            }
        }

        //定义依赖属性
        public static readonly DependencyProperty TextBoxTextProperty = DependencyProperty.Register("TextBoxText", typeof(string), typeof(WPFControl_TextBoxWithButton));
        
        public string TextBoxText
        {
            get
            {
                return (string)GetValue(TextBoxTextProperty);
            }
            set
            {
                SetValue(TextBoxTextProperty, value);
            }
        }

        string hintText;
        public string Hint
        {
            get { return hintText; }
            set { hintText = value; }
        }
        public event EventHandler ImageButtonClick;
        private void ImageButton_ImageButtonClick(object sender, MouseButtonEventArgs e)
        {
            ImageButtonClick?.Invoke(this, e);
        }
    }
}
