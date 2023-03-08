using Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    /// WPFControl_DataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_DataGrid : INotifyPropertyChanged
    {
        public virtual event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            if (propertyName == "ItemsSource")
            {
                type = ItemsSource.GetType().GetInterface("System.Collections.Generic.IEnumerable`1").GetGenericArguments()[0];
            }
        }

        public WPFControl_DataGrid()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(WPFControl_DataGrid));

        IEnumerable _ItemsSource;
        public IEnumerable ItemsSource
        {
            get
            {
                return _ItemsSource;
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        Type _type;
        public Type type
        {
            get
            {
                return _type;
            }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    LoadTable();
                }
                else
                {
                    _type = value;
                }
            }
        }

        public object SelectedItem
        {
            get
            {
                return dataGrid.SelectedItem;
            }
        }

        //public static void OnItemsSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        //{
        //    LoadTable();
        //    Type t = value.GetType().GetInterface("System.Collections.Generic.IEnumerable`1").GetGenericArguments()[0];
        //}

        /// <summary>
        /// 加载表格数据列
        /// </summary>
        public void LoadTable()
        {
            //foreach (var column in Table.Columns)
            //{
            //    if (column.Header.ToString() != "序号") Table.Columns.Remove(column);
            //}
            if (null == type) return;
            dataGrid.AutoGenerateColumns = false;
            foreach (PropertyInfo property in type.GetProperties())
            {
                if (type.GetDescription(property.Name) != "")
                {
                    DataGridTemplateColumn TemplateColum = new DataGridTemplateColumn();
                    TemplateColum.Header = type.GetDescription(property.Name);
                    TemplateColum.MinWidth = 50;
                    Binding binding = new Binding()
                    {
                        Path = new PropertyPath(property.Name),
                        Mode = BindingMode.OneWay
                    };
                    DataTemplate MyDataTemplate = new DataTemplate();
                    var Item = new FrameworkElementFactory();
                    Item.Type = typeof(TextBlock);
                    Item.SetBinding(TextBlock.TextProperty, binding);
                    Item.SetValue(VerticalAlignmentProperty, VerticalAlignment.Center);
                    Item.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Left);
                    Item.SetValue(MarginProperty, new Thickness(10, 6, 10, 6));
                    MyDataTemplate.VisualTree = Item;
                    TemplateColum.CellTemplate = MyDataTemplate;//单元格模板注册
                    dataGrid.Columns.Add(TemplateColum);
                }
            }
        }

        public event MouseButtonEventHandler Event_RowRightButtonUp;
        public event MouseButtonEventHandler Event_RowMouseDown;

        private void DGR_Border_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            object obj = Convert.ChangeType((sender as Border).DataContext, type);
            RoutedEventArgs args = new RoutedEventArgs(RowRightButtonUpEvent, dataGrid);
            //引用自定义路由事件
            RaiseEvent(args);
        }

        private void DGR_Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
        
        private void DGR_Border_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        int _SelectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                if (value < 0)
                {
                    _SelectedIndex = 0;
                    OnPropertyChanged("SelectedIndex");
                }
            }
        }
        
        /// <summary>
        /// 声明路由事件
        /// 参数:要注册的路由事件名称，路由事件的路由策略，事件处理程序的委托类型(可自定义)，路由事件的所有者类类型
        /// </summary>
        public static readonly RoutedEvent RowRightButtonUpEvent = EventManager.RegisterRoutedEvent("RowRightButtonUp", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WPFControl_DataGrid));

        /// <summary>
        /// 处理各种路由事件的方法
        /// </summary>
        public event RoutedEventHandler RowRightButtonUp
        {
            //将路由事件添加路由事件处理程序
            add { AddHandler(RowRightButtonUpEvent, value); }
            //从路由事件处理程序中移除路由事件
            remove { RemoveHandler(RowRightButtonUpEvent, value); }
        }


        /// <summary>
        /// 声明路由事件
        /// 参数:要注册的路由事件名称，路由事件的路由策略，事件处理程序的委托类型(可自定义)，路由事件的所有者类类型
        /// </summary>
        public static readonly RoutedEvent RowMouseDownEvent = EventManager.RegisterRoutedEvent("RowMouseDown", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WPFControl_DataGrid));

        /// <summary>
        /// 处理各种路由事件的方法
        /// </summary>
        public event RoutedEventHandler RowMouseDown
        {
            //将路由事件添加路由事件处理程序
            add { AddHandler(RowMouseDownEvent, value); }
            //从路由事件处理程序中移除路由事件
            remove { RemoveHandler(RowMouseDownEvent, value); }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //object obj = Convert.ChangeType((sender as Border).DataContext, type);
            //dataGrid.SelectedItem = obj;
            RoutedEventArgs args = new RoutedEventArgs(RowMouseDownEvent, dataGrid);
            //引用自定义路由事件
            RaiseEvent(args);
        }
    }
}
