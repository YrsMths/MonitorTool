using Server;
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

namespace MonitorServer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WPFControl_ImageButton_ImageButtonClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void brandDropDownContext_Event_ContextMenuItemSelected(KeyValuePair<string, string> keyValue)
        {
            brandPopup.IsOpen = false;
        }

        private void monitorDropDownContext_Event_ContextMenuItemSelected(KeyValuePair<string, string> keyValue)
        {
            monitorPopup.IsOpen = false;
        }

        private void deploymentDropDownContext_Event_ContextMenuItemSelected(KeyValuePair<string, string> keyValue)
        {
            deploymentPopup.IsOpen = false;
        }

        private void BrandBox_ImageButtonClick(object sender, EventArgs e)
        {
            brandPopup.IsOpen = true;
        }

        private void MonitorBox_ImageButtonClick(object sender, EventArgs e)
        {
            monitorPopup.IsOpen = true;
        }
        
        private void DeploymentBox_ImageButtonClick(object sender, EventArgs e)
        {
            deploymentPopup.IsOpen = true;
        }

        private void MaxButton_ImageButtonClick(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void MinButton_ImageButtonClick(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
