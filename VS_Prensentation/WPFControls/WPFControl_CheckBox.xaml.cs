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
    /// WPFControl_CheckBox.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_CheckBox : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    
        public WPFControl_CheckBox()
        {
            InitializeComponent();
        }
       
        public Dictionary<string,bool> ItemsSource
        {
            get
            {
                List<CheckBoxItemDataModel> list = CheckboxList.ItemsSource as List<CheckBoxItemDataModel>;
                Dictionary<string, bool> dic = new Dictionary<string, bool>();
                foreach (var i in list)
                {
                    dic[i.Content] = i.Checked;
                }
                return dic;
            }
            set
            {
                List<CheckBoxItemDataModel> list = new List<CheckBoxItemDataModel>();
                foreach (var i in value)
                {
                    CheckBoxItemDataModel model = new CheckBoxItemDataModel() {
                        Content=i.Key,
                        Checked=i.Value
                    };
                    list.Add(model);
                }
                CheckboxList.ItemsSource = list;
            }
        }
        public List<CheckBoxItemDataModel> Items
        {
            get
            {
                return CheckboxList.ItemsSource as List<CheckBoxItemDataModel>;
            }
        }
        public List<CheckBoxItemDataModel> CheckedItems
        {
            get
            {
                return (CheckboxList.ItemsSource as List<CheckBoxItemDataModel>).FindAll(a=>a.Checked).ToList();
            }
        }
        public delegate void CheckBoxItemCheckedStateChanged(CheckBoxItemDataModel item);
        public event CheckBoxItemCheckedStateChanged Event_CheckedStateChanged;
        private void Grid_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ((sender as Grid).DataContext as CheckBoxItemDataModel).Checked = !((sender as Grid).DataContext as CheckBoxItemDataModel).Checked;
            Event_CheckedStateChanged?.Invoke((sender as Grid).DataContext as CheckBoxItemDataModel);
        }
    }
    public class CheckBoxItemDataModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _Content { get; set; }
        public string Content
        {
            get
            {
                return _Content;
            }
            set
            {
                _Content = value;
                OnPropertyChanged("Content");
            }
        }
        private bool _Checked { get; set; }
        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                _Checked = value;
                OnPropertyChanged("Checked");
            }
        }
    }
}
