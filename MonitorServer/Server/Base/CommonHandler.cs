using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class CommonHandler : BaseHandler
    {
        public CommonHandler() : base() { }

        /// <summary>
        /// 分包
        /// </summary>
        /// <param name="byteBuffer"></param>
        /// <param name="connId"></param>
        public override void SubPackage(ByteBuffer byteBuffer, IntPtr connId)
        {
            int length = byteBuffer.WriterIndex;
            byte[] buff = new byte[length];
            byteBuffer.ReadBytes(buff, 0, length);
            HandlePackage(buff, connId);
            byteBuffer.DiscardReadBytes();
        }
        /// <summary>
        /// 处理数据包
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="connId"></param>
        public override void HandlePackage(byte[] bytes, IntPtr connId)
        {
            if (isSplitPackage)
            {
                string line = ShowMsg(bytes, bytes.Length);
                ReceiveMsg_Action?.Invoke(connId, line);
            }
        }

        /// <summary>
        /// 连接时需要发送连接包 包含时间戳
        /// </summary>
        /// <param name="SendAction"></param>
        /// <param name="connId"></param>
        //public override void Connect(Action<IntPtr, byte[]> SendAction, IntPtr connId)
        //{

        //}
    }
}
