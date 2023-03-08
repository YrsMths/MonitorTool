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
    /// SplashscreenWithCaption.xaml 的交互逻辑
    /// </summary>
    public partial class SplashscreenWithCaption : Window, INotifyPropertyChanged, ISpalshscreen
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public delegate void Delegate_SendCommand(string Command, object Value);
        public SplashscreenWithCaption()
        {
            InitializeComponent();
        }
        public string Caption
        {
            get; set;
        } = "请稍候";
        public void SetCaption(string Caption)
        {
            this.Caption = Caption;
            OnPropertyChanged("Caption");
        }

        public void SendCommand(string Command, object Value)
        {
            switch (Command)
            {
                case "SetCaption":
                    SetCaption((string)Value);
                    break;
                case "Close":
                    this.Close();
                    break;
                default:
                    break;
            }
        }
    }
}
