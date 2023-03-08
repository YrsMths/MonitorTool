using System;
using System.Collections;
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
    /// WPFControl_ContextMenuList.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_ContextMenuList : UserControl, INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public WPFControl_ContextMenuList()
        {
            InitializeComponent();
            
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(BindingList<KeyValuePair<string, string>>), typeof(WPFControl_ContextMenuList));
        public BindingList<KeyValuePair<string,string>> ItemsSource
        {
            get
            {
                return (BindingList<KeyValuePair<string, string>>)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public double ItemsWidth
        {
            get
            {
                if (this.ActualWidth < 50)
                {
                    return 50;
                }
                return this.ActualWidth - 4;
            }
        }
        private void PopupItem_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            TextBlock textBlock = border.Child as TextBlock;
            border.Background = new SolidColorBrush(Color.FromRgb(44, 113, 244));
            textBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            Event_ContextMenuItemEnter?.Invoke((KeyValuePair<string,string>)((sender as Border).DataContext));

        }
        private void PopupItem_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            TextBlock textBlock = border.Child as TextBlock;
            border.Background = new SolidColorBrush();
            textBlock.Foreground = new SolidColorBrush(Color.FromRgb(51, 51, 51));
            Event_ContextMenuItemLeave?.Invoke((KeyValuePair<string, string>)((sender as Border).DataContext));
        }
        public delegate void ContextMenuItemHandler(KeyValuePair<string, string> keyValue);
        public event ContextMenuItemHandler Event_ContextMenuItemSelected;
        private void PopupItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            Event_ContextMenuItemSelected?.Invoke(((KeyValuePair<string, string>)((sender as Border).DataContext)));
            RoutedEventArgs args = new RoutedEventArgs(MenuItemSelectedEvent, this);
            //引用自定义路由事件
            RaiseEvent(args);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnPropertyChanged("ItemsWidth");
        }
        public int SelectedIndex
        {
            get
            {
                return ListContent.SelectedIndex;
            }
            set
            {
                ListContent.SelectedIndex = value;
            }
        }

        public KeyValuePair<string, string> SelectedItem
        {
            get
            {
                if (SelectedIndex == -1) return new KeyValuePair<string, string>(null, null);
                return (KeyValuePair<string, string>)ListContent.SelectedItem;
            }
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock tb = (sender as TextBlock);
            if (tb.ActualWidth > this.ActualWidth - 30)
            {
                tb.ToolTip = tb.Text;
            }
        }

        /// <summary>
        /// 声明路由事件
        /// 参数:要注册的路由事件名称，路由事件的路由策略，事件处理程序的委托类型(可自定义)，路由事件的所有者类类型
        /// </summary>
        public static readonly RoutedEvent MenuItemSelectedEvent = EventManager.RegisterRoutedEvent("MenuItemSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WPFControl_ContextMenuList));
        /// <summary>
        /// 处理各种路由事件的方法
        /// </summary>
        public event RoutedEventHandler MenuItemSelected
        {
            //将路由事件添加路由事件处理程序
            add { AddHandler(MenuItemSelectedEvent, value); }
            //从路由事件处理程序中移除路由事件
            remove { RemoveHandler(MenuItemSelectedEvent, value); }
        }


        public event ContextMenuItemHandler Event_ContextMenuItemEnter;
        public event ContextMenuItemHandler Event_ContextMenuItemLeave;
    }
}
