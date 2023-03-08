using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace VS_Presentation.ResourceDictionary
{
    /// <summary>
    /// 基础窗口样式类，控制基础窗口的基本操作逻辑及显示效果
    /// </summary>
    public partial class Dictionary_WindowBaseStyle
    {

        #region 调节窗口尺寸
        public enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }
        public const int WM_SYSCOMMAND = 0x112;
        public HwndSource _HwndSource;

        public Dictionary<ResizeDirection, Cursor> cursors = new Dictionary<ResizeDirection, Cursor>
        {
            {ResizeDirection.Top, Cursors.SizeNS},
            {ResizeDirection.Bottom, Cursors.SizeNS},
            {ResizeDirection.Left, Cursors.SizeWE},
            {ResizeDirection.Right, Cursors.SizeWE},
            {ResizeDirection.TopLeft, Cursors.SizeNWSE},
            {ResizeDirection.BottomRight, Cursors.SizeNWSE},
            {ResizeDirection.TopRight, Cursors.SizeNESW},
            {ResizeDirection.BottomLeft, Cursors.SizeNESW}
        };


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                FrameworkElement element = e.OriginalSource as FrameworkElement;
                if (element != null && !element.Name.Contains("Resize"))
                    (sender as Window).Cursor = Cursors.Arrow;
            }
        }

        public void ResizePressed(object sender, MouseEventArgs e)
        {
            if (Window.GetWindow(sender as Rectangle).ResizeMode == ResizeMode.NoResize) return;
            FrameworkElement element = sender as FrameworkElement;
            ResizeDirection direction = (ResizeDirection)Enum.Parse(typeof(ResizeDirection), element.Name.Replace("Resize", ""));
            Window.GetWindow(sender as Rectangle).Cursor = cursors[direction];
            if (e.LeftButton == MouseButtonState.Pressed)
                ResizeWindow(direction);
        }

        public void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_HwndSource.Handle, WM_SYSCOMMAND, (IntPtr)(61440 + direction), IntPtr.Zero);
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this._HwndSource = PresentationSource.FromVisual((Visual)sender) as HwndSource;
            (sender as Window).MouseMove += new MouseEventHandler(Window_MouseMove);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
             
               (sender as Window).DragMove();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if ((sender as PasswordBox).Password.Length == 0)
            {
                (sender as PasswordBox).Background =(sender as PasswordBox).Resources["HintText"] as VisualBrush;
            }
            else
            {
                (sender as PasswordBox).Background = null;
            }

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            ScaleTransform rtf = new ScaleTransform();
             (sender as Window).RenderTransform = rtf;
            DoubleAnimation animation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(200)));
            animation.AutoReverse = false;

            rtf.BeginAnimation(ScaleTransform.ScaleXProperty,animation);
            rtf.BeginAnimation(ScaleTransform.ScaleYProperty, animation);

        }
    }
}
