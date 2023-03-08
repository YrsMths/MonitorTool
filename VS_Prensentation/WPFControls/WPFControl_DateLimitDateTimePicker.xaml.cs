using System;
using System.Collections.Generic;
using System.Linq;
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
    /// WPFControl_DateLimitDateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_DateLimitDateTimePicker : UserControl
    {
        public delegate void DateTimeCheckedHandler();
        public DateTimeCheckedHandler Event_DateTimeChecked;
        public DateTime CheckDateTime
        {
            get
            {
                DateTime date = new DateTime(DatePicker.CheckDate.Year, DatePicker.CheckDate.Month, DatePicker.CheckDate.Day);
                DateTime time = TimePicker.CheckTime;
                date = date.AddHours(time.Hour);
                date = date.AddMinutes(time.Minute);
                date = date.AddSeconds(time.Second);
                return date;
            }
            set
            {
                this.DatePicker.CheckDate = new DateTime(value.Year, value.Month, value.Day);
                this.TimePicker.CheckTime = value;
            }
        }

        DateTime _StartTime;
        public DateTime StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                _StartTime = value;
                DatePicker.StartTime = value;
                TimePicker.StartTime = value;
            }
        }

        DateTime _EndTime;
        public DateTime EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
                DatePicker.EndTime = value;
                TimePicker.EndTime = value;
            }
        }

        public WPFControl_DateLimitDateTimePicker()
        {
            InitializeComponent(); DatePicker.DateCheckedHandler += DateTimeChecked;
            TimePicker.TimeCheckedHandler += DateTimeChecked;
        }
        public void DateTimeChecked(DateTime CheckTime)
        {
            //CheckDateTime = CheckTime;
            this.TimePicker.CheckTime = CheckTime;
            Event_DateTimeChecked?.Invoke();
        }

        public void DateTimeChecked()
        {
            Event_DateTimeChecked?.Invoke();
        }
    }
}
