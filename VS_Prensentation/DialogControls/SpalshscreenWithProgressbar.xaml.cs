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
    /// SpalshscreenWithProgressbar.xaml 的交互逻辑
    /// </summary>
    public partial class SpalshscreenWithProgressbar : Window, INotifyPropertyChanged, ISpalshscreen
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public SpalshscreenWithProgressbar()
        {
            InitializeComponent();
        }
        public string Caption
        {
            get; set;
        } = "";
        public double ProgressValue
        {
            get;
            set;
        }
        public double ActualProgressWidth
        {
            get
            {
                return ProgressValue * 1.8d;
            }
        }
        public void SetCaption(string Caption)
        {
            this.Caption = Caption;
            OnPropertyChanged("Caption");
        }
        public void SetProgress(double percent)
        {
            ProgressValue = percent;
            OnPropertyChanged("ActualProgressWidth");
        }

        public void SendCommand(string Command, object Value)
        {
            switch (Command)
            {
                case "SetCaption":
                    SetCaption((string)Value);
                    break;
                case "SetProgress":
                    SetProgress((double)Value);
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
