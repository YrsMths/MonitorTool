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
    /// WPFControl_TimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_TimePicker : UserControl, INotifyPropertyChanged
    {
        public delegate void DateTimePickedHandler();
        public DateTimePickedHandler TimeCheckedHandler;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public DateTime CheckTime
        {
            get
            {
                return new DateTime(1,1,1,(int)(HourOffset/30), (int)(MinuteOffset/30),(int)(SecondOffset/30));
            }
            set
            {
                HourOffset = value.Hour * 30;
                MinuteOffset = value.Minute * 30;
                SecondOffset = value.Second * 30;
                HourOffset = HourOffset > 690 ? 690 : HourOffset;
                MinuteOffset = MinuteOffset > 1770 ? 1770 : MinuteOffset;
                SecondOffset = SecondOffset > 1770 ? 1770 : SecondOffset;
                Hourpicker.ScrollToVerticalOffset(HourOffset);
                Minutepicer.ScrollToVerticalOffset(MinuteOffset);
                Secondpicker.ScrollToVerticalOffset(SecondOffset);
            }
        }
        public WPFControl_TimePicker()
        {
            InitializeComponent();
        }

        double HourOffset = 0;
        double MinuteOffset = 0;
        double SecondOffset = 0;
        private void Hourpicker_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                HourOffset-=30;
                HourOffset = HourOffset < 0 ? 0 : HourOffset;
            }
            else
            {
                HourOffset+=30;
                HourOffset = HourOffset > 690 ? 690 : HourOffset;             
            }
             Hourpicker.ScrollToVerticalOffset(HourOffset);
            TimeCheckedHandler?.Invoke();
        }

        private void Minutepicer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                MinuteOffset -= 30;
                MinuteOffset = MinuteOffset < 0 ? 0 : MinuteOffset;
            }
            else
            {
                MinuteOffset += 30;
                MinuteOffset=MinuteOffset> 1770 ? 1770 : MinuteOffset;
            }
            Minutepicer.ScrollToVerticalOffset(MinuteOffset);
            TimeCheckedHandler?.Invoke();
        }

        private void Secondpicker_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                SecondOffset -= 30;
                SecondOffset = SecondOffset < 0 ? 0 : SecondOffset;
            }
            else
            {
                SecondOffset += 30;
                SecondOffset = SecondOffset >1770 ? 1770 : SecondOffset;
            }
            Secondpicker.ScrollToVerticalOffset(SecondOffset);
            TimeCheckedHandler?.Invoke();
        }
    }
}
