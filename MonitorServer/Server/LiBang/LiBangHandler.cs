using Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class LiBangHandler : BaseHandler
    {
        public LiBangHandler() : base() { }

        /// <summary>
        /// 分包
        /// </summary>
        /// <param name="byteBuffer"></param>
        /// <param name="connId"></param>
        public override void SubPackage(ByteBuffer byteBuffer, IntPtr connId)
        {
            while (byteBuffer.ReadableBytes > 0)
            {
                if (byteBuffer.ReadableBytes < 8) return;
                int length = byteBuffer.ReadShort();
                byteBuffer.ReaderIndex -= 2;
                if (length - 2 > byteBuffer.ReadableBytes) return;
                byte[] buff = new byte[length];
                byteBuffer.ReadBytes(buff, 0, length);
                if (length > 8) HandlePackage(buff, connId);
                byteBuffer.DiscardReadBytes();
            }
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
                string line = Encoding(bytes, bytes.Length, isHexShow);
                AddMsg_Action?.Invoke(connId, line);
            }
            LBProtocol lBProtocol = new LBProtocol();
            try { lBProtocol = new LBProtocol(bytes); }
            catch (Exception ex) { return; }
            switch (lBProtocol.GetFrameHeader().GetCommandId())
            {
                case LB_COMMAND_ID_ENUM.NET_PATIENT_GUID:
                    byte[] guidBytes = new byte[12];
                    Array.Copy(lBProtocol.GetFrameBody().buff, 4, guidBytes, 0, 12);
                    string newGuid = BitConverter.ToString(guidBytes, 0).Replace("-", "");
                    string Guid;
                    if (!GUIDDic.TryGetValue(connId, out Guid))
                    {
                        Guid = newGuid;
                        GUIDDic.TryAdd(connId, Guid);
                    }
                    else if (Guid != newGuid)
                    {
                        GUIDDic.TryUpdate(connId, newGuid, Guid);
                    }
                    break;
                case LB_COMMAND_ID_ENUM.NET_WAVE_INFO:
                    byte[] typeBytes = new byte[2];//波形类型
                    Array.Copy(lBProtocol.GetFrameBody().buff, 0, typeBytes, 0, 2);
                    int waveType = ((int)(typeBytes[1]) << 8) | ((int)(typeBytes[0]));
                    if (Enum.IsDefined(typeof(LB_WAVE_ENUM), waveType))
                    {
                        Dictionary<string, LeadData> Leads = null;
                        if (!LeadDic.TryGetValue(connId, out Leads))
                        {
                            Leads = new Dictionary<string, LeadData>();
                            LeadDic.TryAdd(connId, Leads);
                        }
                        LB_WAVE_ENUM WAVE = (LB_WAVE_ENUM)waveType;
                        string leadName = WAVE.GetDescription();
                        if (!Leads.ContainsKey(leadName)) Leads.Add(leadName, new LeadData());
                        short samp = lBProtocol.GetFrameBody().buff[5];
                        Leads[leadName].samp = samp;
                        Leads[leadName].id = (int)WAVE;
                    }
                    break;
                default: break;
            }
        }
        /// <summary>
        /// 连接时需要发送连接包 包含时间戳
        /// </summary>
        /// <param name="SendAction"></param>
        /// <param name="connId"></param>
        public override void Connect(Action<IntPtr, byte[]> SendAction, IntPtr connId)
        {
            base.Connect(SendAction, connId);
            LBProtocol lBProtocol = new LBProtocol();
            FrameHeader frameHeader = lBProtocol.GetFrameHeader();
            frameHeader.setCommandId(LB_COMMAND_ID_ENUM.NET_CONNECT_SUCCESS);
            frameHeader.setSerialNO(0);
            DateTime now = DateTime.Now;
            ByteBuffer byteBuffer = new ByteBuffer(0);
            byteBuffer.WriteShort((short)now.Year);
            byteBuffer.WriteByte(now.Minute);
            byteBuffer.WriteByte(now.Hour);
            byteBuffer.WriteByte(now.Day);
            byteBuffer.WriteByte(now.Month);
            byteBuffer.WriteByte(now.Second);
            byteBuffer.WriteByte((byte)now.DayOfWeek);

            lBProtocol.GetFrameBody().setContent(byteBuffer.GetBuf());
            SendData_Action?.Invoke(connId, lBProtocol.GetFrame());
            byteBuffer.Dispose();
        }
    }
}
