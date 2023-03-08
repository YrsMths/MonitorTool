using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public abstract class BaseHandler : IDisposable
    {
        /// <summary>
        /// 向客户端发送数据委托
        /// </summary>
        protected Action<IntPtr, byte[]> SendData_Action;
        /// <summary>
        /// 追加显示信息委托
        /// </summary>
        public Action<IntPtr, string> AddMsg_Action;
        /// <summary>
        /// 部署方式
        /// </summary>
        public DEPLOYMENT_MODE_ENUM deploymentMode;

        public BaseHandler()
        {
            this.AddMsg_Action += AddMsg_Action;
        }

        /// <summary>
        /// 是否分包展示
        /// </summary>
        public bool isSplitPackage = true;
        /// <summary>
        /// 是否以HEX形式展示
        /// </summary>
        public bool isHexShow = true;
        /// <summary>
        /// 设备波形信息
        /// </summary>
        public ConcurrentDictionary<IntPtr, Dictionary<string, LeadData>> LeadDic = new ConcurrentDictionary<IntPtr, Dictionary<string, LeadData>>();
        /// <summary>
        /// GUID信息
        /// </summary>
        public ConcurrentDictionary<IntPtr, string> GUIDDic = new ConcurrentDictionary<IntPtr, string>();
        /// <summary>
        /// 线程安全的字典 缓存数据
        /// </summary>
        protected ConcurrentDictionary<IntPtr, ByteBuffer> dic = new ConcurrentDictionary<IntPtr, ByteBuffer>();
        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        public void Handle(byte[] bytes, int size, IntPtr connId)
        {
            if (!isSplitPackage) AddMsg_Action?.Invoke(connId, Encoding(bytes, size, isHexShow));
            ByteBuffer byteBuffer = null;
            if (!dic.TryGetValue(connId, out byteBuffer))
            {
                byteBuffer = new ByteBuffer(size);
                dic.TryAdd(connId, byteBuffer);
            }
            byteBuffer.WriteBytes(bytes, size);
            SubPackage(byteBuffer, connId);
        }
        /// <summary>
        /// 分包
        /// </summary>
        /// <param name="byteBuffer"></param>
        /// <param name="connId"></param>
        public abstract void SubPackage(ByteBuffer byteBuffer, IntPtr connId);
        /// <summary>
        /// 处理包
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="connId"></param>
        public abstract void HandlePackage(byte[] bytes, IntPtr connId);
        /// <summary>
        /// 连接操作
        /// </summary>
        /// <param name="SendAction"></param>
        /// <param name="connId"></param>
        public virtual void Connect(Action<IntPtr, byte[]> SendAction, IntPtr connId)
        {
            this.SendData_Action += SendAction;
        }
        /// <summary>
        /// 数据转码
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="size"></param>
        /// <param name="Hex"></param>
        /// <returns></returns>
        public string Encoding(byte[] bytes, int size, bool Hex = false)
        {
            if (Hex)
            {
                byte[] buff = new byte[size];
                Array.Copy(bytes, buff, size);
                StringBuilder sb = new StringBuilder(buff.Length * 3);
                foreach (byte b in buff)
                {
                    sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
                }
                return sb.ToString().ToUpper();
            }
            else return System.Text.Encoding.Default.GetString(bytes, 0, size);
        }
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.SendData_Action -= this.SendData_Action;
            this.AddMsg_Action -= this.AddMsg_Action;
        }
    }

}
