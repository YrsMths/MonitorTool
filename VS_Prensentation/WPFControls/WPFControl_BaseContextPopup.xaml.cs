using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace VS_Presentation.WPFControls
{
    /// <summary>
    /// WPFControl_BaseContextPopup.xaml 的交互逻辑
    /// </summary>
    public partial class WPFControl_BaseContextPopup : UserControl,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public delegate void Delegate_ContentToPlacementTrigger(object sender);
        public Delegate_ContentToPlacementTrigger Event_ContentToPlacementTrigger;
        public delegate void Delegate_PlacementToContentTrigger(object sender);
        public Delegate_PlacementToContentTrigger Event_PlacementToContentTrigger;
        private object _PlacementObjectInput;
        public object PlacementObjectInput
        {
            get
            {
                return _PlacementObjectInput;
            }
            set
            {
                _PlacementObjectInput = value;
                Event_PlacementToContentTrigger?.Invoke(value);
            }
        }
        private object _ContentObjectInput;
        public object ContentObjectInput
        {
            get
            {
                return _ContentObjectInput;
            }
            set
            {
                _ContentObjectInput = value;
                Event_ContentToPlacementTrigger?.Invoke(value);
            }
        }
        public WPFControl_BaseContextPopup()
        {
            InitializeComponent();
            OnPropertyChanged("PlacementTarget");
        }
        public static readonly DependencyProperty PlacementTargetProperty = DependencyProperty.Register("PlacementTarget", typeof(UIElement), typeof(WPFControl_BaseContextPopup), new FrameworkPropertyMetadata(null));
        public UIElement PlacementTarget
        {
            get
            {
                return (UIElement)GetValue(PlacementTargetProperty);
            }
            set
            {
                SetValue(PlacementTargetProperty,value);
            }
        }
        public PlacementMode Placement
        {
            get
            {
                return Popup_Menu.Placement;
            }
            set
            {
                Popup_Menu.Placement = value;
            }
        }
        public PopupAnimation PopupAnimation
        {
            get
            {
                return Popup_Menu.PopupAnimation;
            }
            set
            {
                Popup_Menu.PopupAnimation = value;
            }
        }
        private double _PopupWidth = 220;
        private double _PopupHeight = 60;

        public double PopupWidth
        {
            get
            {
                return _PopupWidth;
            }
            set
            {
                _PopupWidth = value;
                OnPropertyChanged("PopupWidth");
                OnPropertyChanged("ContentWidth");
            }
        }
        public double PopupHeight
        {
            get
            {
                return _PopupHeight;
            }
            set
            {
                _PopupHeight = value;
                OnPropertyChanged("PopupHeight");
                OnPropertyChanged("ContentHeight");
            }
        }
        public double ContentWidth
        {
            get
            {
                return _PopupWidth - 20;
            }
        }
        public double ContentHeight
        {
            get
            {
                return _PopupHeight - 20;
            }
        }
        private UIElement _PopupContentElement;
        public UIElement PopupContentElement
        {
            get
            {
                return _PopupContentElement;
            }
            set
            {
                _PopupContentElement = value;
                ContentGrid.Children.Clear();
                ContentGrid.Children.Add(value);
            }
        }
        public  bool isOpen
        {
            get
            {
                return Popup_Menu.IsOpen;
            }
            set
            {
                Popup_Menu.IsOpen = value;
            }
        }
    }
}
