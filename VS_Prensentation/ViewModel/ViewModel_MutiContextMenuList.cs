using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VS_Presentation.Command;
using VS_Presentation.WPFControls;

namespace VS_Presentation.ViewModel
{
    public class ViewModel_MutiContextMenuList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #region 属性
        /// <summary>
        /// 数据源
        /// </summary>
        BindingList<KeyValuePair<string, RadioBoxItem>> _ItemSource;
        public BindingList<KeyValuePair<string, RadioBoxItem>> ItemSource
        {
            get
            {
                return _ItemSource;
            }
            set
            {
                _ItemSource = value;
                OnPropertyChanged("ItemSource");
            }
        }
        
        /// <summary>
        /// 选择模式
        /// </summary>
        SelectionMode selectionMode;
        public SelectionMode SelectionMode
        {
            get
            {
                return selectionMode;
            }
            set
            {
                selectionMode = value;
                OnPropertyChanged("SelectionMode");
            }
        }
        #endregion

        #region 构造
        public ViewModel_MutiContextMenuList() { }
        #endregion

        #region 命令
        public event EventHandler SelectionChanged;
        public ICommand SelectionChangedCommand => new RelayCommand(obj =>
        {
            SelectionChanged?.Invoke(obj, null);
        });
        #endregion

        #region 方法

        #endregion
    }

    public class RadioBoxItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// 状态
        /// </summary>
        bool _State;
        public bool State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
                OnPropertyChanged("State");
            }
        }
        /// <summary>
        /// 项
        /// </summary>
        string _Item;
        public string Item
        {
            get
            {
                return _Item;
            }
            set
            {
                _Item = value;
                OnPropertyChanged("Item");
            }
        }
        #region 构造
        public RadioBoxItem(bool state, string item)
        {
            this.State = state;
            this.Item = item;
        }
        public RadioBoxItem() { }
        #endregion
        
    }

}
