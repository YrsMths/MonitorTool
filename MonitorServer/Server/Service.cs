using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Service
    {
        //用于通信的Socket
        Socket socketSend;
        //用于监听的SOCKET
        Socket socketWatch;
        //将远程连接的客户端的IP地址和Socket存入集合中
        public Dictionary<IntPtr, Client> dicSocket = new Dictionary<IntPtr, Client>();

        //创建监听连接的线程
        Thread AcceptSocketThread;
        //接收客户端发送消息的线程
        Thread ReceiveThread;
        //监听节点
        private static IPEndPoint _point;
        //刷新客户端列表
        public Action RefreshClients;
        //监听状态
        public bool isListening = false;
        
        BaseHandler _handler;
        /// <summary>
        /// 数据处理类
        /// </summary>
        public BaseHandler handler
        {
            get
            {
                return _handler;
            }
            set
            {
                value.AddMsg_Action += AddMsg;
                _handler = value;
            }
        }
        /// <summary>
        /// 是否停止刷新数据
        /// </summary>
        public bool isStopRefreshMessage { get; set; } = false;
        /// <summary>
        /// 是否以HEX格式展示数据
        /// </summary>
        public bool isHexShow
        {
            get
            {
                return handler == null ? true : handler.isHexShow;
            }
            set
            {
                if(null != handler)
                {
                    handler.isHexShow = value;
                }
            }
        }
        /// <summary>
        /// 是否分包
        /// </summary>
        public bool isSplitPackage
        {
            get
            {
                return handler == null ? false : handler.isSplitPackage;
            }
            set
            {
                if (null != handler)
                {
                    handler.isSplitPackage = value;
                }
            }
        }
        /// <summary>
        /// 端口号
        /// </summary>
        public IPEndPoint point
        {
            get
            {
                return _point;
            }
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Start(IPAddress ip, int port)
        {
            //获取ip地址
            //IPAddress ip = IPAddress.Parse(xxx);
            socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //创建端口号
            _point = new IPEndPoint(ip, port);
            //绑定IP地址和端口号
            socketWatch.Bind(point);
            //this.txt_Log.AppendText("监听成功" + " \r \n");
            //开始监听:设置最大可以同时连接多少个请求
            socketWatch.Listen(10);

            //创建线程
            AcceptSocketThread = new Thread(new ParameterizedThreadStart(StartListen));
            AcceptSocketThread.IsBackground = true;
            AcceptSocketThread.Start(socketWatch);
            isListening = true;
        }
        /// <summary>
        /// 等待客户端的连接，并且创建与之通信用的Socket
        /// </summary>
        /// <param name="obj"></param>
        private void StartListen(object obj)
        {
            Socket socketWatch = obj as Socket;
            try
            {
                while (true)
                {
                    //等待客户端的连接，并且创建一个用于通信的Socket
                    socketSend = socketWatch.Accept();
                    //获取远程主机的ip地址和端口号
                    string strIp = socketSend.RemoteEndPoint.ToString();
                    dicSocket.Add(socketSend.Handle, new Client(socketSend, DateTime.Now));

                    handler.Connect(Send, socketSend.Handle);

                    RefreshClients?.Invoke();
                    //定义接收客户端消息的线程
                    ReceiveThread = new Thread(new ParameterizedThreadStart(Receive));
                    ReceiveThread.IsBackground = true;
                    ReceiveThread.Start(socketSend);
                }
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 服务器端不停的接收客户端发送的消息
        /// </summary>
        /// <param name="obj"></param>
        private void Receive(object obj)
        {
            Socket socketSend = obj as Socket;
            while (true)
            {
                //客户端连接成功后，服务器接收客户端发送的消息
                byte[] buffer = new byte[2048];
                //实际接收到的有效字节数
                int count = socketSend.Receive(buffer);
                if (count == 0)//count 表示客户端关闭，要退出循环
                {
                    break;
                }
                else
                {
                    if (handler != null)
                    {
                        handler.Handle(buffer, count, socketSend.Handle);
                    }
                }
            }
            dicSocket.Remove(socketSend.Handle);
            RefreshClients?.Invoke();
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="connId"></param>
        /// <param name="buffer"></param>
        private void Send(IntPtr connId, byte[] buffer)
        {
            try
            {
                dicSocket[connId].socket.Send(buffer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 追加接收信息
        /// </summary>
        /// <param name="connId"></param>
        /// <param name="msg"></param>
        private void AddMsg(IntPtr connId, string msg)
        {
            if (!isStopRefreshMessage)
                dicSocket[connId].msg = msg;
        }
        /// <summary>
        /// 停止监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Stop()
        {
            socketWatch.Close();
            isListening = false;
            //socketSend.Close();
            //终止线程
            AcceptSocketThread?.Abort();
            ReceiveThread?.Abort();
        }
    }
}
