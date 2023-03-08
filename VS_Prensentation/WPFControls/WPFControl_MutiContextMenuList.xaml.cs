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
using VS_Presentation.ViewModel;

namespace VS_Presentation.WPFControls
{
    /// <summary>
    /// WPFControl_MutiContextMenuList.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_MutiContextMenuList : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public WPFControl_MutiContextMenuList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// VM
        /// </summary>
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register("ViewModel", typeof(ViewModel_MutiContextMenuList), typeof(WPFControl_MutiContextMenuList));
        public ViewModel_MutiContextMenuList ViewModel
        {
            get
            {
                return (ViewModel_MutiContextMenuList)GetValue(ViewModelProperty);
            }
            set
            {
                SetValue(ViewModelProperty, value);
            }
        }
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnPropertyChanged("ItemsWidth");
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
    }
}
