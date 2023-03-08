using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VS_Presentation.ValueConverter;

namespace VS_Presentation.DialogControls
{
   public static class SplashscreenManager
    {
        static BackgroundWorker background;
        static ISpalshscreen CurrentDisplaySplashScreen;
        static Thread thread;
        static Window CurrentParentWindow;
        public static bool isShow
        {
            get
            {
                if (CurrentParentWindow == null & CurrentDisplaySplashScreen == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public static void Show(Window ParentWindow, Type SplashWindowType)
        {
            ISpalshscreen SplashWindow = (ISpalshscreen)Activator.CreateInstance(SplashWindowType);
            CurrentDisplaySplashScreen = SplashWindow;
            CurrentParentWindow = ParentWindow;
            if (null != ParentWindow) ParentWindow.IsEnabled = false;
            (CurrentDisplaySplashScreen as Window).Show();
        }

        public static void SendCommand(string Command, object Value)
        {
            CurrentDisplaySplashScreen.SendCommand(Command, Value);
        }

        public static void Close()
        {
            (CurrentDisplaySplashScreen as Window).Close();
            CurrentDisplaySplashScreen = null;
            if (null != CurrentParentWindow) CurrentParentWindow.IsEnabled = true;
            CurrentParentWindow = null;
        }
    }
}
