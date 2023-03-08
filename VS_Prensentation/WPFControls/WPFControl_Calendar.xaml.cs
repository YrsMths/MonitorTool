using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// WPFControl_Calendar.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_Calendar : UserControl
    {
        public delegate void DateTimePickedHandler();
        public DateTimePickedHandler DateCheckedHandler;
        BindingList<DateModel> CurrentDisplay = new BindingList<DateModel>();
        BindingList<MonthModel> MonthPickerList = new BindingList<MonthModel>();

        public WPFControl_Calendar()
        {
            InitializeComponent();
            MainCalenderContent.ItemsSource = CurrentDisplay;
            MonthList.ItemsSource = MonthPickerList;
            LoadDate();
        }
      
        private void LoadDate()
        {
        
            CurrentDisplay.Clear();
            int DistanceOfStart = (int)(CheckDate.AddDays(1 - CheckDate.Day)).DayOfWeek;
            DistanceOfStart = DistanceOfStart== 0 ? 6 : DistanceOfStart - 1;
            DateTime start = CheckDate.AddDays(1 - CheckDate.Day - DistanceOfStart);
            int selectedIndex =(CheckDate-start).Days;
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
                    isCurrentMonth=isCurrentMonth,
                    year = year,
                    month = month,
                    date = date
                });
            }
            HeadTitle.TextContent = string.Format("{0}年{1}月", CheckDate.Year, CheckDate.Month);
            MainCalenderContent.SelectedIndex = selectedIndex;
        }
        private DateTime _CheckDate=DateTime.Now;
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
                timer.Elapsed += new System.Timers.ElapsedEventHandler((a, b) => {
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
                bool isCurrentMonth = false;
                if (month == i)
                {
                    isCurrentMonth = true;
                }
                MonthModel model = new MonthModel()
                {
                    isCurrentMonth = isCurrentMonth,
                    str = year + "年" + i + "月",
                    month = i
                };
                MonthPickerList.Add(model);
            }
        }
        private void PrevMonth_Click(object sender, MouseButtonEventArgs e)
        {
             _CheckDate = CheckDate.AddMonths(-1);
            LoadMonth();
            LoadDate();
        }
        private void NextMonth_Click(object sender, MouseButtonEventArgs e)
        {
             _CheckDate = CheckDate.AddMonths(1);
            LoadMonth();
            LoadDate();
        }

        private void DateCheck_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void MainCalenderContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainCalenderContent.SelectedItem != null)
            {
                DateModel date = MainCalenderContent.SelectedItem as DateModel;

               DateTime Date = new DateTime(int.Parse(date.year),int.Parse(date.month),int.Parse(date.date));
                if (Date.Month != CheckDate.Month)
                {
                     _CheckDate = Date;
                    LoadDate();
                }
                 _CheckDate = Date;
                DateCheckedHandler?.Invoke();
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
                timer.Elapsed += new System.Timers.ElapsedEventHandler((a, b) => {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        MonthPicker.Visibility = Visibility.Collapsed;
                        timer.Stop();
                    }));
                });
                MonthPicker.BeginAnimation(Grid.OpacityProperty, animation);
                MonthModel model = MonthList.SelectedItem as MonthModel;
                 _CheckDate = CheckDate.AddMonths(model.month - CheckDate.Month);
                LoadDate();
                timer.Start();
            }
        }

        private void UserControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                _CheckDate = CheckDate.AddMonths(-1);
                LoadMonth();
                LoadDate();
            }
            else
            {
                _CheckDate = CheckDate.AddMonths(1);
                LoadMonth();
                LoadDate();
            }
        }
    }
    class DateModel
    {
        public bool isCurrentMonth { get; set; }
        public string year { get; set; }
        public string month { get; set; }
        public string date { get; set; }
    }
    class MonthModel
    {
        public bool isCurrentMonth { get; set; }
        public string str{ get; set; }
        public int month { get; set; }
    }
}
