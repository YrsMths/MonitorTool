using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class LBProtocol
    {
        public FrameHeader header;
        public FrameBody body;
        private byte[] buff = new byte[8];

        public LBProtocol() { }
        public LBProtocol(byte[] bytes)
        {
            if (bytes.Length < 8) throw new Exception();
            int bodyLength = bytes.Length - 8;
            byte[] headerBuff = new byte[8];
            byte[] bodyBuff = new byte[bodyLength];
            Array.Copy(bytes, 0, headerBuff, 0, 8);
            Array.Copy(bytes, 8, bodyBuff, 0, bodyLength);
            header = new FrameHeader(headerBuff);
            body = new FrameBody(bodyBuff);
        }
        /// <summary>
        /// 获取帧头
        /// </summary>
        /// <returns></returns>
        public FrameHeader GetFrameHeader()
        {
            if (header == null) header = new FrameHeader();
            return header;
        }
        /// <summary>
        /// 获取帧体
        /// </summary>
        /// <returns></returns>
        public FrameBody GetFrameBody()
        {
            if (body == null) body = new FrameBody();
            return body;
        }
        /// <summary>
        /// 获取整帧数据
        /// </summary>
        /// <returns></returns>
        public byte[] GetFrame()
        {
            header.setLength(body.Length + 8);
            this.buff = header.buff.Concat(body.buff).ToArray();
            SumCheck(buff);
            return this.buff;
        }
        /// <summary>
        /// 校验和计算
        /// </summary>
        /// <param name="content"></param>
        private void SumCheck(byte[] content)
        {
            int len = content.Length;
            int size = (len % 2) == 0 ? len : len - 1;
            int i;
            ulong sum = 0;
            for (i = 0; i < size; i = i + 2)
            {
                byte[] bytes = { content[i], content[i + 1] };
                int num = BitConverter.ToInt16(bytes, 0);
                sum += (ulong)num;
            }
            if (len % 2 != 0)
            {
                sum += content[len - 1];
            }
            while (sum >> 16 != 0)
            {
                sum = (sum & 0xffffUL) + (sum >> 16);
            }
            ushort x = (ushort)~(sum & 0xffffUL);
            content[7] = (byte)(x >> 8);
            content[6] = (byte)(x & 255);
        }
    }
    /// <summary>
    /// 帧头
    /// </summary>
    public class FrameHeader
    {
        public byte[] buff = new byte[8];

        public FrameHeader() { }
        public FrameHeader(byte[] buff)
        {
            if (buff.Length == 8) this.buff = buff;
            else throw new Exception();
        }

        public void setSerialNO(int No)
        {
            buff[5] = (byte)((short)No >> 8);
            buff[4] = (byte)((short)No & 255);
        }

        public void setCommandId(LB_COMMAND_ID_ENUM commandId)
        {
            buff[3] = (byte)((short)commandId >> 8);
            buff[2] = (byte)((short)commandId & 255);
        }

        public void setLength(int length)
        {
            buff[1] = (byte)((short)length >> 8);
            buff[0] = (byte)((short)length & 255);
        }

        public LB_COMMAND_ID_ENUM GetCommandId()
        {
            int commandId = ((int)(buff[3]) << 8) | ((int)(buff[2]));
            if (Enum.IsDefined(typeof(LB_COMMAND_ID_ENUM), commandId)) return (LB_COMMAND_ID_ENUM)commandId;
            return (LB_COMMAND_ID_ENUM)(-1);
        }

        public int length
        {
            get
            {
                return buff.Length;
            }
        }
    }
    /// <summary>
    /// 帧体
    /// </summary>
    public class FrameBody
    {
        public byte[] buff;

        public FrameBody() { }
        public FrameBody(byte[] buff)
        {
            if (null == buff) throw new Exception();
            this.buff = buff;
        }

        public void setContent(byte[] buff)
        {
            this.buff = buff;
        }

        public int Length
        {
            get
            {
                return buff.Length;
            }
        }

        
    }
}
