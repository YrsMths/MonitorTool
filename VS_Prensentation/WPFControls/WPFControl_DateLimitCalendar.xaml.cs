using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VS_Presentation.WPFControls
{
    /// <summary>
    /// WPFControl_DateLimitCalendar.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_DateLimitCalendar : UserControl
    {
        public delegate void DateTimePickedHandler(DateTime CheckDate);
        public DateTimePickedHandler DateCheckedHandler;
        BindingList<DateModel> CurrentDisplay = new BindingList<DateModel>();
        BindingList<MonthModel> MonthPickerList = new BindingList<MonthModel>();

        public WPFControl_DateLimitCalendar()
        {
            InitializeComponent();
            MainCalenderContent.ItemsSource = CurrentDisplay;
            MonthList.ItemsSource = MonthPickerList;
        }

        public WPFControl_DateLimitCalendar(DateTime starttime, DateTime endtime)
        {
            InitializeComponent();
            _CheckDate = starttime;
            this.StartTime = starttime;
            this.EndTime = endtime;

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
                _CheckDate = value;
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
                LoadDate();
            }
        }

        private void LoadDate()
        {
            CurrentDisplay.Clear();
            int DistanceOfStart = (int)(CheckDate.AddDays(1 - CheckDate.Day)).DayOfWeek;
            DistanceOfStart = DistanceOfStart == 0 ? 6 : DistanceOfStart - 1;
            DateTime start = CheckDate.AddDays(1 - CheckDate.Day - DistanceOfStart);
            int selectedIndex = (CheckDate - start).Days;
            for (int i = 0; i < 42; i++)
            {
                DateTime datetime = start.AddDays(i);
                bool isCurrentMonth = false;
                if (datetime.Month == CheckDate.Month)
                {
                    isCurrentMonth = true;
                }
                string date = datetime.Day.ToString();
                string year = datetime.Year.ToString();
                string month = datetime.Month.ToString();

                CurrentDisplay.Add(new DateModel()
                {
                    canSelected = datetime.Date >= StartTime.Date && datetime.Date <= EndTime.Date ? true : false,
                    isCurrentMonth = isCurrentMonth,
                    year = year,
                    month = month,
                    date = date
                });
            }
            HeadTitle.TextContent = string.Format("{0}年{1}月", CheckDate.Year, CheckDate.Month);
            MainCalenderContent.SelectedIndex = selectedIndex;
        }

        private DateTime _CheckDate;
        public DateTime CheckDate
        {
            get
            {
                return _CheckDate;
            }
            set
            {
                _CheckDate = value;
                LoadDate();
            }
        }
        private void Review_Click(object sender, MouseButtonEventArgs e)
        {
            if (MonthPicker.Visibility == Visibility.Collapsed)
            {
                MonthPicker.Visibility = Visibility.Visible;
                double from = MonthPicker.Opacity;
                double to = 1;
                DoubleAnimation animation = new DoubleAnimation(from, to, TimeSpan.FromMilliseconds(200));
                animation.AutoReverse = false;
                MonthPicker.BeginAnimation(Grid.OpacityProperty, animation);
                LoadMonth();
            }
            else
            {
                double from = 1;
                double to = 0;
                DoubleAnimation animation = new DoubleAnimation(from, to, TimeSpan.FromMilliseconds(200));
                animation.AutoReverse = false;
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 200;
                timer.Elapsed += new System.Timers.ElapsedEventHandler((a, b) =>
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        MonthPicker.Visibility = Visibility.Collapsed;
                        timer.Stop();
                    }));
                });
                MonthPicker.BeginAnimation(Grid.OpacityProperty, animation);
                timer.Start();
            }
        }
        private void LoadMonth()
        {
            MonthPickerList.Clear();
            string year = CheckDate.Year.ToString();
            int month = CheckDate.Month;
            for (int i = 1; i <= 12; i++)
            {
                bool canSelected = false;
                bool isCurrentMonth = false;
                if (month == i)
                {
                    isCurrentMonth = true;
                }
                if (CheckDate.Year >= StartTime.Year && CheckDate.Year <= EndTime.Year)
                {
                    if (i >= StartTime.Month && i <= EndTime.Month)
                    {
                        canSelected = true;
                    }
                }
                MonthModel model = new MonthModel()
                {
                    canSelected = canSelected,
                    isCurrentMonth = isCurrentMonth,
                    str = year + "年" + i + "月",
                    month = i
                };
                MonthPickerList.Add(model);
            }
        }
        private void PrevMonth_Click(object sender, MouseButtonEventArgs e)
        {
            if (_CheckDate.AddDays(1 - _CheckDate.Day) > StartTime.AddDays(1 - StartTime.Day))
            {
                _CheckDate = CheckDate.AddMonths(-1);
                LoadMonth();
                LoadDate();
            }
        }
        private void NextMonth_Click(object sender, MouseButtonEventArgs e)
        {
            if (_CheckDate.AddDays(1 - _CheckDate.Day) < EndTime.AddDays(1 - EndTime.Day))
            {
                _CheckDate = CheckDate.AddMonths(1);
                LoadMonth();
                LoadDate();
            }
        }

        private void DateCheck_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MainCalenderContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainCalenderContent.SelectedItem != null)
            {
                DateModel date = MainCalenderContent.SelectedItem as DateModel;

                DateTime Date = new DateTime(int.Parse(date.year), int.Parse(date.month), int.Parse(date.date));
                if (Date.Date >= StartTime.Date && Date.Date <= EndTime.Date)
                {
                    if (Date.Month != CheckDate.Month)
                    {
                        _CheckDate = Date;
                        LoadDate();
                    }
                    _CheckDate = Date;
                }
                else if (Date.Date <= StartTime.Date)
                {
                    _CheckDate = StartTime;
                    LoadDate();
                }
                else if (Date.Date >= EndTime.Date)
                {
                    _CheckDate = EndTime;
                    LoadDate();
                }
                DateCheckedHandler?.Invoke(CheckDate);
            }
        }

        private void MonthList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MonthList.SelectedItem != null)
            {
                double from = 1;
                double to = 0;
                DoubleAnimation animation = new DoubleAnimation(from, to, TimeSpan.FromMilliseconds(200));
                animation.AutoReverse = false;
                System.Timers.Timer timer = new System.Timers.Timer();
                timer.Interval = 200;
                timer.Elapsed += new System.Timers.ElapsedEventHandler((a, b) =>
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        MonthPicker.Visibility = Visibility.Collapsed;
                        timer.Stop();
                    }));
                });
                MonthPicker.BeginAnimation(Grid.OpacityProperty, animation);
                MonthModel model = MonthList.SelectedItem as MonthModel;
                if (model.canSelected)
                {
                    _CheckDate = CheckDate.AddMonths(model.month - CheckDate.Month);
                }
                LoadDate();
                timer.Start();
            }
        }

        private void UserControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && _CheckDate.AddDays(1 - _CheckDate.Day) > StartTime.AddDays(1 - StartTime.Day))
            {
                _CheckDate = CheckDate.AddMonths(-1);
                LoadMonth();
                LoadDate();
            }
            else if (e.Delta < 0 && _CheckDate.AddDays(1 - _CheckDate.Day) < EndTime.AddDays(1 - EndTime.Day))
            {
                _CheckDate = CheckDate.AddMonths(1);
                LoadMonth();
                LoadDate();
            }
        }

        class DateModel
        {
            public bool canSelected { get; set; }
            public bool isCurrentMonth { get; set; }
            public string year { get; set; }
            public string month { get; set; }
            public string date { get; set; }
        }

        class MonthModel
        {
            public bool canSelected { get; set; }
            public bool isCurrentMonth { get; set; }
            public string str { get; set; }
            public int month { get; set; }
        }
    }
}
