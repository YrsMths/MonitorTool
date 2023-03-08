using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Threading.Tasks;
using VS_Presentation.DialogControls;

namespace VS_Presentation.ValueConverter
{
   public class SetupRoutine
    {
        public delegate void Delegate_UpdateStatus(string str);
        public static Delegate_UpdateStatus Event_UpdateStatus;
        public static void Start(string Username)
        {
            Thread thread = new Thread(() =>
            {
                Event_UpdateStatus?.Invoke(Username);
                Thread.Sleep(1000);
                Event_UpdateStatus?.Invoke("LoginComplete");
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
