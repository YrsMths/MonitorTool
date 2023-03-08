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
    /// WPFControl_BaseInputBox.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_BaseInputBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public Color FocusColor { get; set; } = Color.FromRgb(54, 92, 245);
        public Color RestColor { get; set; } = Color.FromRgb(221, 221, 221);
        private Thickness _InputBoxPadding = new Thickness(9, 0, 9, 0);
        public Thickness InputBoxPadding
        {
            get
            {
                return _InputBoxPadding;
            }
            set
            {
                _InputBoxPadding = value;
                TranslateTransform.X = InputBoxPadding.Left;
                TranslateTransform.Y = InputBoxPadding.Top;
            }
        }
        public string Hint
        {
            get
            {
                return HintBlock.Text;
            }
            set
            {
                HintBlock.Text = value;


            }
        }

        private Brush _Background;
        public Brush CurrentBackground
        {
            get
            {
                return _Background;
            }
             set
            {
                _Background = value;
                OnPropertyChanged("CurrentBackground");
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

        public Color HintColor { get; set; } = Color.FromRgb(153, 153, 153);
        public TextBlock HintBlock { get; set; } = new TextBlock();
        public VisualBrush HintBackground { get; set; } = new VisualBrush() { Stretch=Stretch.None,AlignmentY=AlignmentY.Center,AlignmentX=AlignmentX.Left};
        public TransformGroup transformGroup { get; set; } = new TransformGroup();
        public TranslateTransform TranslateTransform = new TranslateTransform();
        public WPFControl_BaseInputBox()
        {
            InitializeComponent();
            InputBox.DataContext = this;

            HintBlock.Foreground = new SolidColorBrush(HintColor);
            HintBackground.Visual = HintBlock;
            TranslateTransform.X = InputBoxPadding.Left;
            TranslateTransform.Y = InputBoxPadding.Top;
            transformGroup.Children.Add(TranslateTransform);
            HintBackground.Transform = transformGroup;
            
            InputBox.GetBindingExpression(TextBox.BackgroundProperty).UpdateTarget();
        }
        bool isMasked = false;
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        
            //as TransformGroup).Children[0] as TranslateTransform).X = InputBoxPadding.Left;
            //((((InputBox.FindResource("HintText")) as VisualBrush).Transform as TransformGroup).Children[0] as TranslateTransform).Y = InputBoxPadding.Top;
            //(((InputBox.FindResource("HintText")) as VisualBrush).Visual as TextBlock).Text = Hint;
            //(((InputBox.FindResource("HintText")) as VisualBrush).Visual as TextBlock).Foreground = new SolidColorBrush(HintColor);
            //(((InputBox.FindResource("HintText") as VisualBrush).Transform as TransformGroup).Children[0] as TranslateTransform).X = InputBoxPadding.Left;
            
            //(((InputBox.FindResource("HintText") as VisualBrush).Transform as TransformGroup).Children[0] as TranslateTransform).Y = InputBoxPadding.Top;
         //((InputBox.FindResource("HintText") as VisualBrush).Transform as TransformGroup).Children.Add(new TranslateTransform() { X = InputBoxPadding.Left, Y = InputBoxPadding.Top} );
            this.InputBox.Padding = InputBoxPadding;
        }
        private void InputBoxGotFocus(object sender, RoutedEventArgs e)
        {
            BoxBorder.BorderBrush = new SolidColorBrush(FocusColor);
        }
        private void InputBoxLostFocus(object sender, RoutedEventArgs e)
        {
            BoxBorder.BorderBrush = new SolidColorBrush(RestColor);
        }
        public Brush InputBoxBackground
        {
            get
            {
                return BoxBorder.Background;
            }
            set
            {
                BoxBorder.Background = value;
            }
        }
        public string Mask
        {
            get
            {
                return InputBox.InputMask;
            }
            set
            {
                InputBox.InputMask = value;
  
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(WPFControl_BaseInputBox));
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                //InputBox.Text = value;
                SetValue(TextProperty, value);
            }
        }

        //public string Text
        //{
        //    get
        //    {
        //        return InputBox.Text;
        //    }
        //    set
        //    {
        //        InputBox.Text = value;
        //    }
        //}

        public bool HasImageButton
        {
            get
            {
                if (ImageButton.Visibility == Visibility.Visible)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    ImageButton.Visibility = Visibility.Visible;
                }
                else
                {
                    ImageButton.Visibility = Visibility.Collapsed;
                }
            }
        }
        public double ImageButtonWidth
        {
            get
            {
                return ImageButton.Width;
            }
            set
            {
                ImageButton.Width = value;
                ImageButton.ButtonWidth = value;
                ButtonWidth.Width = new GridLength(value);
            }
        }
        public double ImageButtonHeight
        {
            get
            {
                return ImageButton.Height;
            }
            set
            {
                ImageButton.Height = value;
                ImageButton.ButtonHeight = value;
            }
        }
        public double IconWidth
        {
            get
            {
                return ImageButton.ImageWidth;
            }
            set
            {
                ImageButton.ImageWidth = value;
       
            }
        }
        public double IconHeight
        {
            get
            {
                return ImageButton.ImageHeight;
            }
            set
            {
                ImageButton.ImageHeight = value;
            }
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
        public Brush RegularBackground
        {
            get
            {
                return ImageButton.RegularBackground;
            }
            set
            {
                ImageButton.RegularBackground = value;
            }
        }
        public Brush MouseOverBackground
        {
            get
            {
                return ImageButton.MouseOverBackground;
            }
            set
            {
                ImageButton.MouseOverBackground = value;
            }
        }
        public Brush MouseDownBackground
        {
            get
            {
                return ImageButton.MouseDownBackground;
            }
            set
            {
                ImageButton.MouseDownBackground = value;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return InputBox.IsReadOnly;
            }
            set
            {
                InputBox.IsReadOnly = value;
                InputBox.Focusable = value;
            }
        }
        public char RestChar
        {
            get
            {
                return InputBox.RestChar;
            }
            set
            {
                InputBox.RestChar = value;
            }
        }
        public event TextChangedEventHandler TextChanged;
        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged?.Invoke(sender,e);
        }
        public event MouseButtonEventHandler InputBoxButtonClick;
        private void ImageButton_ImageButtonClick(object sender, MouseButtonEventArgs e)
        {
            InputBoxButtonClick?.Invoke(this,e);
        }
        public CornerRadius CornerRadius
        {
            get
            {
                return BoxBorder.CornerRadius;
            }
            set
            {
                BoxBorder.CornerRadius = value;
            }
        }
        public new bool IsInputMethodEnabled
        {
            get
            {
                return InputBox.IsInputMethodEnabled;
            }
            set
            {
                InputMethod.SetIsInputMethodEnabled(InputBox, value);
            }
        }
    }
}
