using System;
using System.Windows.Controls;

namespace VS_Presentation.WPFControls
{
    /// <summary>
    /// WPFControl_DateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_DateTimePicker : UserControl
    {
        public delegate void DateTimeCheckedHandler();
        public DateTimeCheckedHandler Event_DateTimeChecked;
        public DateTime CheckDateTime
        {
            get
            {
                DateTime date = new DateTime(DatePicker.CheckDate.Year,DatePicker.CheckDate.Month,DatePicker.CheckDate.Day);
                DateTime time = TimePicker.CheckTime;
                date = date.AddHours(time.Hour);
                date = date.AddMinutes(time.Minute);
                date = date.AddSeconds(time.Second);
                return date;
            }
            set
            {
                this.DatePicker.CheckDate = new DateTime(value.Year, value.Month, value.Day);
                this.TimePicker.CheckTime = new DateTime(1970,1,1,value.Hour,value.Minute,value.Second);
            }
        }
        public WPFControl_DateTimePicker()
        {
            InitializeComponent();
            DatePicker.DateCheckedHandler += DateTimeChecked;
            TimePicker.TimeCheckedHandler += DateTimeChecked;
        }
        public void DateTimeChecked()
        {
            Event_DateTimeChecked?.Invoke();
        }
    }
}
