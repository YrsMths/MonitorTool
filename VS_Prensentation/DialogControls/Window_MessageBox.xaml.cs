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
    /// Window_MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class Window_MessageBox : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public string MsgTitle { get; set; }
        public string MsgContent { get; set; }
        public Window_MessageBox(string Title, string Content)
        {
            this.MsgTitle = Title;
            this.MsgContent = Content;
            InitializeComponent();
            this.Style = Resources["WindowBaseStyleWithScaleAnimation"] as Style;
            OnPropertyChanged("MsgTitle");
            OnPropertyChanged("MsgContent");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Close();
            }
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
