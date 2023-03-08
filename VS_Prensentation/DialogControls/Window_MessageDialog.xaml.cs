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
using System.Windows.Shapes;

namespace VS_Presentation.DialogControls
{
    /// <summary>
    /// Window_MessageDialog.xaml 的交互逻辑
    /// </summary>
    public partial class Window_MessageDialog : Window, INotifyPropertyChanged
    {
        public delegate void Delegate_CheckEvent();
        public event EventHandler Event_Yes;
        public event EventHandler Event_No;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public Window_MessageDialog(string Title, string Content)
        {
            this.MsgTitle = Title;
            this.MsgContent = Content;
            InitializeComponent();
            this.Style = Resources["WindowBaseStyleWithScaleAnimation"] as Style;
            OnPropertyChanged("MsgTitle");
            OnPropertyChanged("MsgContent");
        }
        public string MsgTitle { get; set; }
        public string MsgContent { get; set; }
        private void WPFControl_TextButton_TextButtonClick(object sender, MouseButtonEventArgs e)
        {
            //Event_No?.Invoke(sender,  e);
            this.DialogResult = false;
            this.Close();
        }

        private void WPFControl_TextButton_TextButtonClick_1(object sender, MouseButtonEventArgs e)
        {
            //Event_Yes?.Invoke(sender, e);
            this.DialogResult = true;
            this.Close();
        }

        private void WPFControl_TextButton_TextButtonClick(object sender, RoutedEventArgs e)
        {
            Event_No?.Invoke(sender, e);
        }

        private void WPFControl_TextButton_TextButtonClick_1(object sender, RoutedEventArgs e)
        {
            Event_Yes?.Invoke(sender, e);
        }
    }
}
