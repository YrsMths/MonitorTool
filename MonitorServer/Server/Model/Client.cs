using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Client
    {
        public Socket socket { get; set; }
        
        public IntPtr Handle { get { return socket.Handle; } }

        [Description("远程终结点")]
        public string EndPoint { get { return socket.RemoteEndPoint.ToString(); } }
        public DateTime dateTime { get; set; }

        [Description("连接时间")]
        public string dateTimeStr { get { return dateTime.ToString("HH:mm:ss"); } }

        string _data = "";
        public string data
        {
            get
            {
                return _data;
            }
            set
            {
                if (_data.Length + value.Length > 99999)
                {
                    _data = _data.Remove(0, _data.Length + value.Length - 99999) + value;
                }
                else
                {
                    _data += value;
                }
            }
        }

        string _msg;
        public string msg
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = value;
                data = String.Format("[{0}][{1}]：{2}\n", DateTime.Now.ToString("HH:mm:ss"), EndPoint, value);
            }
        }

        public Client(Socket socket, DateTime dateTime)
        {
            this.socket = socket;
            this.dateTime = dateTime;
        }
    }
}
