using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Timer = System.Timers.Timer;

namespace VS_Presentation.DialogControls
{
        /// <summary>
        /// ToastBox.xaml 的交互逻辑
        /// </summary>
    public partial class ToastBox : Window
    {
        int ReleaseTime = 3000;
        Timer MainTimer = new Timer();
        double left_offset=0;
        double top_offset=0;

        #region Window styles



        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            int error = 0;
            IntPtr result = IntPtr.Zero;
            // Win32 SetWindowLong doesn't clear error on success
            SetLastError(0);

            if (IntPtr.Size == 4)
            {
                // use SetWindowLong
                Int32 tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
                error = Marshal.GetLastWin32Error();
                result = new IntPtr(tempResult);
            }
            else
            {
                // use SetWindowLongPtr
                result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }

            if ((result == IntPtr.Zero) && (error != 0))
            {
                throw new System.ComponentModel.Win32Exception(error);
            }

            return result;
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern Int32 IntSetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return unchecked((int)intPtr.ToInt64());
        }

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(int dwErrorCode);
        #endregion
        private void HideAltTab()
        {
            var windowInterop = new WindowInteropHelper(this);
            int exStyle =(int) GetWindowLong(windowInterop.Handle, -20);
            exStyle |= 0x80;
            SetWindowLong(windowInterop.Handle, -20, (IntPtr)exStyle);
        }
        private void MainTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => { this.Close(); }));
        }
        public ToastBox(string ToastContent)
        {

            InitializeComponent();
            this.Style = Resources["WindowBaseStyleWithLoadedAnimation"] as Style;
            ContentBlock.Text = ToastContent;

        }
        public ToastBox(string ToastContent,int ReleaseTime):this(ToastContent)
        {
            this.ReleaseTime = ReleaseTime;
        }
        public ToastBox(string ToastContent,int ReleaseTime,double X_Offset,double Y_Offset):this(ToastContent,ReleaseTime)
        {
            this.left_offset = X_Offset;
            this.top_offset = Y_Offset;
        }
        bool canClose = false;
        protected override void OnClosing(CancelEventArgs e)
        {

            if (canClose)
            {
                //CloseAnimation中调用Close()时可正常关闭
                base.OnClosing(e);
            }
            else
            {          
                //第一次OnClosing取消，启动关闭动画
                e.Cancel = true;
                CloseAnimation(this, this.GetVisualChild(0) as Grid);
                canClose = true;
            }
        }
        /// <summary>
        /// 关闭动画效果，完成后关闭窗口
        /// </summary>
        /// <param name="window"></param>
        /// <param name="grid"></param>
        public void CloseAnimation(Window window, Grid grid)
        {
            ScaleTransform rtf = new ScaleTransform();
            rtf.CenterX = 0.5;
            rtf.CenterY = 0.5;
            rtf.ScaleX = 1;
            rtf.ScaleY = 1;
            Storyboard sb = new Storyboard();
            DependencyProperty[] propertyChainx = new DependencyProperty[]
          {
                Grid.RenderTransformProperty,
                ScaleTransform.ScaleXProperty
          };
            DependencyProperty[] propertyChainy = new DependencyProperty[]
       {
                Grid.RenderTransformProperty,
                ScaleTransform.ScaleYProperty
       };
            grid.RenderTransform = rtf;


            DoubleAnimation animationx = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(200)));
            DoubleAnimation animationy = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(200)));
            DoubleAnimation OpacityAnimation = new DoubleAnimation(1,0, new Duration(TimeSpan.FromMilliseconds(200)));
            animationx.AutoReverse = false;
            animationy.AutoReverse = false;
            OpacityAnimation.AutoReverse = false;
            Storyboard.SetTarget(animationx, grid);
            Storyboard.SetTarget(animationy, grid);
            Storyboard.SetTarget(OpacityAnimation, grid);
            Storyboard.SetTargetProperty(animationx, new PropertyPath("(0).(1)", propertyChainx));
            Storyboard.SetTargetProperty(animationy, new PropertyPath("(0).(1)", propertyChainy));
            Storyboard.SetTargetProperty(OpacityAnimation,new PropertyPath(Grid.OpacityProperty));
            //Storyboard.SetTargetProperty(animation, new PropertyPath(ScaleTransform.ScaleYProperty));
            sb.Children.Add(animationx);
            sb.Children.Add(animationy);
            sb.Children.Add(OpacityAnimation);
            sb.Completed += new EventHandler((a, b) =>
            {
                Timer timer;
                timer = new Timer();
                timer.Interval = 200;
                timer.Elapsed += new ElapsedEventHandler((c, d) =>
                {
                    window.Dispatcher.Invoke(new Action(() =>
                    {
                        window.Close();
                    }));
                });
                timer.Start();
            });
            sb.Begin();
        }
        public void ResetContent(string content)
        {
            ContentBlock.Text = content;
            MainTimer.Stop();
            MainTimer.Start();
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HideAltTab();
            this.Width = ContentBlock.ActualWidth + 50;
            if (this.Width >= 350) { ContentBlock.TextWrapping = TextWrapping.Wrap;
                this.Width = 350;
            }
            if (this.Width < 120)
            {
                this.Width = 120;
            }
            this.Height = ContentBlock.ActualHeight + 50;
            this.Left = (SystemParameters.WorkArea.Width - this.Width) / 2 + left_offset;
            this.Top = (SystemParameters.WorkArea.Height * 5 / 6 - this.Height) + top_offset;
            MainTimer.Interval = ReleaseTime;
            MainTimer.Elapsed += MainTimer_Elapsed;
            MainTimer.Start();
            this.WindowState = WindowState.Normal;
        }
    }
}
