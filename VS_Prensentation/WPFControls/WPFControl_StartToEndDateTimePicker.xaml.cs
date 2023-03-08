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
    /// WPFControl_StartToEndDateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_StartToEndDateTimePicker : UserControl
    {
        public WPFControl_StartToEndDateTimePicker()
        {
            InitializeComponent();
            StartDateTimePicker.Event_DateTimeChecked += StartValueChanged;
            EndDateTimePicker.Event_DateTimeChecked += EndValueChanged;
            PickerPop.PlacementTarget = Box;
            AlarmPop.PlacementTarget = OK_Button;
            setText();
        }
        private void StartValueChanged()
        {

        }

        private void EndValueChanged()
        {

        }
        public delegate void PickDoneHandler(DateTime Start, DateTime End);
        public PickDoneHandler Event_PickDone;
        System.Timers.Timer AlarmTimer = new System.Timers.Timer();
        private void WPFControl_TextButton_TextButtonClick(object sender, MouseButtonEventArgs e)
        {
            if (EndDateTimePicker.CheckDateTime < StartDateTimePicker.CheckDateTime)
            {
                AlarmTimer.Interval = 2000;
                AlarmTimer.Elapsed -= AlarmTimer_Elapsed;
                AlarmTimer.Elapsed += AlarmTimer_Elapsed;
                AlarmTimer.Start();
                AlarmPop.IsOpen = true;
                EndDateTimePicker.CheckDateTime = EndDateTime;
            }
            else
            {
                _StartDateTime = StartDateTimePicker.CheckDateTime;
                _EndDateTime = EndDateTimePicker.CheckDateTime;
                Event_PickDone?.Invoke(StartDateTime, EndDateTime);
                setText();
                PickerPop.IsOpen = false;
            }
        }

        private void AlarmTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(new Action(() => {
                AlarmPop.IsOpen = false;
            }));

            AlarmTimer.Stop();
        }

        private DateTime _StartDateTime { get; set; } = DateTime.Now;
        private DateTime _EndDateTime { get; set; } = DateTime.Now;
        public DateTime StartDateTime
        {
            get
            {
                return _StartDateTime;
            }
            set
            {
                _StartDateTime = value;
                StartDateTimePicker.CheckDateTime = value;
                setText();
            }
        }
        public DateTime EndDateTime
        {
            get
            {
                return _EndDateTime;
            }
            set
            {
                _EndDateTime = value;
                EndDateTimePicker.CheckDateTime = value;
                setText();
            }
        }

        private void setText()
        {
            Box.Text = StartDateTime.ToString("yyyy-MM-dd HH:mm:ss") + " 至 " + EndDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void Box_InputBoxButtonClick(object sender, MouseButtonEventArgs e)
        {
            StartDateTimePicker.CheckDateTime = StartDateTime;
            EndDateTimePicker.CheckDateTime = EndDateTime;
            PickerPop.IsOpen = true;
        }
    }
}
