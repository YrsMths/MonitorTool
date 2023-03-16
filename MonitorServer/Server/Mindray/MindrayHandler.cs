using Extension;
using HL7.Dotnetcore;
using Server.Mindray;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class MindrayHandler : BaseHandler
    {
        public MindrayHandler() : base() { }
        
        static byte head = 0x0B;
        static byte tail1 = 0x1c;
        static byte tail2 = 0x0d;

        /// <summary>
        /// 分包
        /// </summary>
        /// <param name="byteBuffer"></param>
        /// <param name="connId"></param>
        public override void SubPackage(ByteBuffer byteBuffer, IntPtr connId)
        {
            bool hasHead = false;
            int headIndex = 0;
            int tailIndex = 0;
            while (byteBuffer.ReadableBytes > 0)
            {
                if (!hasHead)
                {
                    if (byteBuffer.ReadByte() == head)
                    {
                        headIndex = byteBuffer.ReaderIndex - 1;
                        hasHead = true;
                    }
                    else continue;
                }
                
                if (byteBuffer.ReadableBytes < 2)
                {
                    byteBuffer.ReaderIndex = headIndex;
                    return;
                }
                
                if (byteBuffer.ReadByte() == tail1 && byteBuffer.ReadByte() == tail2)
                {
                    tailIndex = byteBuffer.ReaderIndex ;
                    //headIndex = headIndex + 1;
                    byteBuffer.ReaderIndex = headIndex;
                    byte[] res = new byte[tailIndex - headIndex];
                    byteBuffer.ReadBytes(res, 0, tailIndex - headIndex);
                    HandlePackage(res, connId);
                    byteBuffer.DiscardReadBytes();
                    hasHead = false;
                }
            }
            if (!hasHead)
            {
                byteBuffer.DiscardReadBytes();
            }
        }

        public override void HandlePackage(byte[] res, IntPtr connId)
        {
            string line = Encoding(res, res.Length);
            string[] messages = MessageHelper.ExtractMessages(line);
            Message message = new Message(messages[0]);
            message.ParseMessage(true);

            switch (deploymentMode)
            {
                case DEPLOYMENT_MODE_ENUM.迈瑞直连:
                    if (message.Segments("MSH").Count != 0)
                    {
                        List<Field> fields = message.Segments("MSH")[0].GetAllFields();
                        string guidstr = fields[2].Value.Split('^')[1];
                        string Guid;
                        if (!GUIDDic.TryGetValue(connId, out Guid))
                        {
                            Guid = guidstr;
                            GUIDDic.TryAdd(connId, Guid);
                        }
                        else if (Guid != guidstr)
                        {
                            GUIDDic.TryUpdate(connId, guidstr, Guid);
                        }
                    }
                    break;
                case DEPLOYMENT_MODE_ENUM.迈瑞Separate:
                    if (message.Segments("PV1").Count != 0)
                    {
                        List<Field> fields = message.Segments("PV1")[0].GetAllFields();
                        string guidstr = fields[3].Value.Split('^')[8].Replace("-", "");
                        string Guid;
                        if (!GUIDDic.TryGetValue(connId, out Guid))
                        {
                            Guid = guidstr;
                            GUIDDic.TryAdd(connId, Guid);
                        }
                        else if (Guid != guidstr)
                        {
                            GUIDDic.TryUpdate(connId, guidstr, Guid);
                        }
                    }
                    break;
                case DEPLOYMENT_MODE_ENUM.迈瑞eGateway:
                    if (message.Segments("PV1").Count != 0)
                    {
                        List<Field> fields = message.Segments("PV1")[0].GetAllFields();
                        var list = fields[3].Value.Split('^');
                        string guidstr = list[0] + list[2];
                        string Guid;
                        if (!GUIDDic.TryGetValue(connId, out Guid))
                        {
                            Guid = guidstr;
                            GUIDDic.TryAdd(connId, Guid);
                        }
                        else if (Guid != guidstr)
                        {
                            GUIDDic.TryUpdate(connId, guidstr, Guid);
                        }
                    }
                    break;
                case DEPLOYMENT_MODE_ENUM.迈瑞eGateway_呼吸机:
                    if (message.Segments("PV1").Count != 0)
                    {
                        List<Field> fields = message.Segments("PV1")[0].GetAllFields();
                        string guidstr = fields[3].Value.Split('^')[8];
                        string Guid;
                        if (!GUIDDic.TryGetValue(connId, out Guid))
                        {
                            Guid = guidstr;
                            GUIDDic.TryAdd(connId, Guid);
                        }
                        else if (Guid != guidstr)
                        {
                            GUIDDic.TryUpdate(connId, guidstr, Guid);
                        }
                    }
                    break;
                default: break;
            }
            
            if (message.Segments("OBX").Count > 0)
            {
                for(int i = 0; i < message.Segments("OBX").Count; i ++)
                {
                    List<Field> fields = message.Segments("OBX")[i].GetAllFields();
                    Dictionary<string, LeadData> Leads = null;
                    if (!LeadDic.TryGetValue(connId, out Leads))
                    {
                        Leads = new Dictionary<string, LeadData>();
                        LeadDic.TryAdd(connId, Leads);
                    }
                    try
                    {
                        string wavestr = fields[3].Value.Split('.')[3];
                        //导联
                        int waveid;
                        Int32.TryParse(wavestr, out waveid);
                        if (Enum.IsDefined(typeof(HL7_WAVE_ENUM), waveid))
                        {
                            HL7_WAVE_ENUM mdm = (HL7_WAVE_ENUM)waveid;
                            string leadName = mdm.GetDescription();
                            if (!Leads.ContainsKey(leadName)) Leads.Add(mdm.ToString(), new LeadData());
                            Leads[leadName].id = (int)mdm;
                            HL7_VALUE_TYPE_ENUM valueType = (HL7_VALUE_TYPE_ENUM)Enum.Parse(typeof(HL7_VALUE_TYPE_ENUM), fields[1].Value);//数据类型
                            HL7_ATTR_ENUM attr;
                            if (Enum.TryParse(fields[2].Value.Split('^')[1], out attr))
                            {
                                switch (attr)
                                {
                                    case HL7_ATTR_ENUM.MDC_ATTR_NU_MSMT_RES:
                                        Leads[leadName].msmtOrigion = line;
                                        Leads[leadName].msmt = fields[4].Value.HL7TypeConverter(valueType);
                                        break;
                                    case HL7_ATTR_ENUM.MDC_ATTR_SAMP_RATE:
                                        Leads[leadName].sampOrigion = line;
                                        Leads[leadName].samp = fields[4].Value.HL7TypeConverter(valueType);
                                        break;
                                }
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
                
            }
            if (isSplitPackage)
            {
                ReceiveMsg_Action?.Invoke(connId, ShowMsg(res, res.Length));
            }
        }
    }
}
