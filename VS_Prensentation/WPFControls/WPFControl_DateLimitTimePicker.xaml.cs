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
    /// WPFControl_DateLimitTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_DateLimitTimePicker : UserControl, INotifyPropertyChanged
    {
        public delegate void DateTimePickedHandler();
        public DateTimePickedHandler TimeCheckedHandler;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        DateTime _CheckTime;
        public DateTime CheckTime
        {
            get
            {
                return new DateTime(_CheckTime.Year, _CheckTime.Month, _CheckTime.Day, Hour, Minute, Second);
            }
            set
            {
                _CheckTime = value;
                OnPropertyChanged("HoursList");
                OnPropertyChanged("MinutesList");
                OnPropertyChanged("SecondsList");

                HourOffset = 0;
                HourOffset = HourOffset > (HoursList.Count - 1) * 30 ? (HoursList.Count - 1) * 30 : HourOffset;
                Hourpicker.ScrollToVerticalOffset(HourOffset);

                MinuteOffset = 0;
                MinuteOffset = MinuteOffset > (MinutesList.Count - 1) * 30 ? (MinutesList.Count - 1) * 30 : MinuteOffset;
                Minutepicker.ScrollToVerticalOffset(MinuteOffset);

                SecondOffset = 0;
                SecondOffset = SecondOffset > (SecondsList.Count - 1) * 30 ? (SecondsList.Count - 1) * 30 : SecondOffset;
                Secondpicker.ScrollToVerticalOffset(SecondOffset);

                //Secondpicker.ScrollToTop();
                //SecondOffset = 0;
            }
        }


        int Hour
        {
            get
            {
                return HoursList[(int)(HourOffset / 30) + 1].Key;
            }
        }
        int Minute
        {
            get
            {
                return MinutesList[(int)(MinuteOffset / 30) + 1].Key;
            }
        }
        int Second
        {
            get
            {
                return SecondsList[(int)(SecondOffset / 30) + 1].Key;
            }
        }

        public BindingList<KeyValuePair<int, string>> HoursList
        {
            get
            {
                BindingList<KeyValuePair<int, string>> hoursList = new BindingList<KeyValuePair<int, string>>();
                hoursList.Add(new KeyValuePair<int, string>(-1, ""));
                if (_CheckTime.Date == StartTime.Date && StartTime.Date == EndTime.Date)
                {
                    for (int i = StartTime.Hour; i < EndTime.Hour + 1; i++)
                    {
                        hoursList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }
                else if (_CheckTime.Date == StartTime.Date)
                {
                    for (int i = StartTime.Hour; i < 24; i++)
                    {
                        hoursList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }
                else if (_CheckTime.Date == EndTime.Date)
                {
                    for (int i = 0; i < EndTime.Hour + 1; i++)
                    {
                        hoursList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }
                else
                {
                    for (int i = 0; i < 24; i++)
                    {
                        hoursList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }
                hoursList.Add(new KeyValuePair<int, string>(-1, ""));
                return hoursList;
            }
        }
        public BindingList<KeyValuePair<int, string>> MinutesList
        {
            get
            {
                BindingList<KeyValuePair<int, string>> minutesList = new BindingList<KeyValuePair<int, string>>();
                minutesList.Add(new KeyValuePair<int, string>(-1, ""));
                if (StartTime.Date == EndTime.Date && StartTime.Hour == EndTime.Hour)
                {
                    for (int i = StartTime.Minute; i <= EndTime.Minute; i++)
                    {
                        minutesList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }
                else if (_CheckTime.Date == StartTime.Date && Hour == StartTime.Hour)
                {
                    for (int i = StartTime.Minute; i < 60; i++)
                    {
                        minutesList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }
                else if (_CheckTime.Date == EndTime.Date && Hour == EndTime.Hour)
                {
                    for (int i = 0; i <= EndTime.Minute; i++)
                    {
                        minutesList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }
                else
                {
                    for (int i = 0; i < 60; i++)
                    {
                        minutesList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }

                minutesList.Add(new KeyValuePair<int, string>(-1, ""));
                return minutesList;
            }
        }
        public BindingList<KeyValuePair<int, string>> SecondsList
        {
            get
            {
                BindingList<KeyValuePair<int, string>> secondsList = new BindingList<KeyValuePair<int, string>>();
                secondsList.Add(new KeyValuePair<int, string>(-1, ""));
                if (StartTime.Date == EndTime.Date && StartTime.Hour == EndTime.Hour && StartTime.Minute == EndTime.Minute)
                {
                    for (int i = StartTime.Second; i <= EndTime.Second; i++)
                    {
                        secondsList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }
                else if (_CheckTime.Date == StartTime.Date && Hour == StartTime.Hour && Minute == StartTime.Minute)
                {
                    for (int i = StartTime.Second; i < 60; i++)
                    {
                        secondsList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }
                else if (_CheckTime.Date == EndTime.Date && Hour == EndTime.Hour && Minute == EndTime.Minute)
                {
                    for (int i = 0; i <= EndTime.Second; i++)
                    {
                        secondsList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }
                else
                {
                    for (int i = 0; i < 60; i++)
                    {
                        secondsList.Add(new KeyValuePair<int, string>(i, i.ToString()));
                    }
                }
                secondsList.Add(new KeyValuePair<int, string>(-1, ""));
                return secondsList;
            }
        }

        public DateTime StartTime;
        public DateTime EndTime;

        public WPFControl_DateLimitTimePicker()
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
                HourOffset -= 30;
                HourOffset = HourOffset < 0 ? 0 : HourOffset;
            }
            else
            {
                HourOffset += 30;
                HourOffset = HourOffset > (HoursList.Count - 3) * 30 ? (HoursList.Count - 3) * 30 : HourOffset;
                HourOffset = HourOffset < 0 ? 0 : HourOffset;
            }
            Hourpicker.ScrollToVerticalOffset(HourOffset);
            Minutepicker.ScrollToTop();
            MinuteOffset = 0;
            Secondpicker.ScrollToTop();
            SecondOffset = 0;
            OnPropertyChanged("MinutesList");
            OnPropertyChanged("SecondsList");
            TimeCheckedHandler?.Invoke();
        }

        private void Minutepicker_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                MinuteOffset -= 30;
                MinuteOffset = MinuteOffset < 0 ? 0 : MinuteOffset;
            }
            else
            {
                MinuteOffset += 30;
                MinuteOffset = MinuteOffset > (MinutesList.Count - 3) * 30 ? (MinutesList.Count - 3) * 30 : MinuteOffset;
                MinuteOffset = MinuteOffset < 0 ? 0 : MinuteOffset;
            }
            Minutepicker.ScrollToVerticalOffset(MinuteOffset);
            Secondpicker.ScrollToTop();
            SecondOffset = 0;
            OnPropertyChanged("SecondsList");
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
                SecondOffset = SecondOffset > (SecondsList.Count - 3) * 30 ? (SecondsList.Count - 3) * 30 : SecondOffset;
                SecondOffset = SecondOffset < 0 ? 0 : SecondOffset;
            }
            Secondpicker.ScrollToVerticalOffset(SecondOffset);

            TimeCheckedHandler?.Invoke();
        }
    }
}
