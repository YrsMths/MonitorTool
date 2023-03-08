using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VS_Presentation.DialogControls
{
    public class BaseSplashscreen : Window, ISpalshscreen
    {
        public delegate void Delegate_SendCommand(string Command, object Value);
        public void SendCommand(string Command, object Value)
        {
            return;
        }
    }
}
