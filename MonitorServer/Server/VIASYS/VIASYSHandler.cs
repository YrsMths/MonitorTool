using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Extension;
using Server.VIASYS;

namespace Server
{
    public class VIASYSHandler : BaseHandler
    {
        public VIASYSHandler() : base() { }

        /// <summary>
        /// 分包
        /// </summary>
        /// <param name="byteBuffer"></param>
        /// <param name="connId"></param>
        public override void SubPackage(ByteBuffer byteBuffer, IntPtr connId)
        {
            bool hasStart = false;
            bool hasEnd = false;
            bool? single = null;
            byteBuffer.ReaderIndex = 0;
            //string label;
            List<byte> labelList = new List<byte>();
            while (byteBuffer.ReadableBytes > 0)
            {
                if (byteBuffer.ReadableBytes < 2) return;
                //匹配xml
                if (!hasStart)
                {
                    //查找标签起始位置
                    byte first = byteBuffer.ReadByte();
                    if (first != 0x3c) continue; 
                    byte second = byteBuffer.ReadByte();
                    if (second == 0x2f) continue;
                    //找到起始"<"
                    byteBuffer.ReaderIndex = byteBuffer.ReaderIndex - 2;
                    byteBuffer.DiscardReadBytes();
                    byteBuffer.ReadByte();
                    byte space = byteBuffer.ReadByte();
                    //找到标签内容
                    while (space != 0x20)
                    {
                        if (byteBuffer.ReadableBytes == 0) return;
                        labelList.Add(space);
                        space = byteBuffer.ReadByte();
                    }
                    hasStart = true;
                }
                else
                {
                    switch (single)
                    {
                        case null:
                            switch (byteBuffer.ReadByte())
                            {
                                case 0x2f:  //"/"
                                    if (byteBuffer.ReadByte() == 0x3e) single = hasEnd = true;
                                    break;
                                case 0x3e:  //">"
                                    single = false;
                                    continue;
                                default: continue;
                            }
                            break;
                        case false://双标签
                            bool match = true;
                            if (byteBuffer.ReadByte() != labelList[0]) continue;
                            for (int i = 1; i < labelList.Count(); i++)
                            {
                                if (byteBuffer.ReadableBytes < 2) return;
                                if (byteBuffer.ReadByte() != labelList[i])
                                {
                                    byteBuffer.ReaderIndex = byteBuffer.ReaderIndex - 1;
                                    match = false;
                                    break;
                                }
                            }
                            if (!match) continue;
                            byteBuffer.ReadByte();
                            hasEnd = true;
                            break;
                    }
                }

                if (hasEnd)
                {
                    byte[] res = new byte[byteBuffer.ReaderIndex];
                    byteBuffer.ReaderIndex = 0;
                    byteBuffer.ReadBytes(res, 0, res.Length);
                    try
                    {
                        HandlePackage(res, connId);
                    }
                    catch (Exception ex) { }
                    byteBuffer.DiscardReadBytes();
                }
            }
            
        }

        /// <summary>
        /// 处理整个包
        /// </summary>
        /// <param name="res"></param>
        /// <param name="connId"></param>
        public override void HandlePackage(byte[] res, IntPtr connId)
        {
            string msg = Encoding(res, res.Length);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(msg);
            XmlElement root = doc.DocumentElement;
            VIASYS_MES_TYPE_ENUM msgType;
            List<byte[]> bytesList = new List<byte[]>();
            if (Enum.TryParse(root.Name, out msgType))
            {
                switch (msgType)
                {
                    case VIASYS_MES_TYPE_ENUM.link:
                        VIASYSProtocol<Link> protocol_Link = new VIASYSProtocol<Link>(msg);
                        bytesList.Add(protocol_Link.GetAck());
                        bytesList.AddRange(protocol_Link.GetResponse());
                        break;
                    case VIASYS_MES_TYPE_ENUM.profile:
                        VIASYSProtocol<Profile> protocol_Profile = new VIASYSProtocol<Profile>(msg);
                        this.profile = protocol_Profile.msgObject;
                        bytesList.Add(protocol_Profile.GetAck());
                        bytesList.AddRange(protocol_Profile.GetResponse());
                        break;
                    case VIASYS_MES_TYPE_ENUM.data:
                        VIASYSProtocol<Data> protocol_Data = new VIASYSProtocol<Data>(msg);
                        HandleData(protocol_Data.msgObject, connId);
                        bytesList.Add(protocol_Data.GetAck());
                        break;
                    default: break;
                }
            }

            if (isSplitPackage)
            {
                ReceiveMsg_Action?.Invoke(connId, ShowMsg(res, res.Length));
            }
            foreach (byte[] data in bytesList)
            {
                if (null != data) SendData_Action?.Invoke(connId, data, ShowMsg(data, data.Length));
            }
        }

        public Profile profile { get; set; }
        /// <summary>
        /// 处理数据包
        /// </summary>
        /// <param name="data"></param>
        public void HandleData(Data data, IntPtr connId)
        {
            string dataContent = data.content;
            if (profile == null) return;
            foreach (var unit in profile.GetUnits(data.@class))
            {
                string temp = string.Empty;
                switch (unit.TypeEnum)
                {
                    case VIASYS_DATA_TYPE_ENUM.TEXT:
                        StringBuilder value = new StringBuilder();
                        int textLength = profile.textEncoding == "UTF-16" ? VIASYS_DATA_TYPE_ENUM.TEXT.GetLength() / 2 : VIASYS_DATA_TYPE_ENUM.TEXT.GetLength();
                        temp = dataContent.Substring(0, textLength);
                        dataContent = dataContent.Substring(textLength);
                        while (temp != "0000")
                        {
                            value.Append(profile.encoding.GetString(temp.HexStrToBytes()));
                            temp = dataContent.Substring(0, textLength);
                            dataContent = dataContent.Substring(textLength);
                        }
                        switch (unit.ID)
                        {
                            case "SysInfoSerial":
                                string newGuid = value.ToString();
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
                        }
                        unit.value = value.ToString();
                        break;
                    case VIASYS_DATA_TYPE_ENUM.BOOL:
                        temp = dataContent.Substring(0, unit.TypeEnum.GetLength());
                        unit.value = temp == "0" ? false : true;
                        break;
                    case VIASYS_DATA_TYPE_ENUM.UWORD:
                        temp = dataContent.Substring(0, unit.TypeEnum.GetLength());
                        unit.value = BitConverter.ToUInt16(temp.HexStrToBytes().Reverse().ToArray(), 0);
                        dataContent = dataContent.Substring(unit.TypeEnum.GetLength());
                        break;
                    case VIASYS_DATA_TYPE_ENUM.WORD:
                        temp = dataContent.Substring(0, unit.TypeEnum.GetLength());
                        unit.value = BitConverter.ToInt16(temp.HexStrToBytes().Reverse().ToArray(), 0);
                        dataContent = dataContent.Substring(unit.TypeEnum.GetLength());
                        break;
                    case VIASYS_DATA_TYPE_ENUM.UBYTE:
                    case VIASYS_DATA_TYPE_ENUM.BYTE:
                        temp = dataContent.Substring(0, unit.TypeEnum.GetLength());
                        unit.value = temp.HexStrToBytes();
                        dataContent = dataContent.Substring(unit.TypeEnum.GetLength());
                        break;
                    case VIASYS_DATA_TYPE_ENUM.ENUM:
                        temp = dataContent.Substring(0, unit.TypeEnum.GetLength());
                        unit.value = BitConverter.ToUInt16(temp.HexStrToBytes().Reverse().ToArray(), 0);
                        dataContent = dataContent.Substring(unit.TypeEnum.GetLength());
                        break;
                    case VIASYS_DATA_TYPE_ENUM.FLOAT:
                        temp = dataContent.Substring(0, unit.TypeEnum.GetLength());
                        unit.value = BitConverter.ToDouble(temp.HexStrToBytes().Reverse().ToArray(), 0);
                        dataContent = dataContent.Substring(unit.TypeEnum.GetLength());
                        break;
                    case VIASYS_DATA_TYPE_ENUM.UINT:
                        temp = dataContent.Substring(0, unit.TypeEnum.GetLength());
                        unit.value = BitConverter.ToUInt32(temp.HexStrToBytes().Reverse().ToArray(), 0);
                        dataContent = dataContent.Substring(unit.TypeEnum.GetLength());
                        break;
                    case VIASYS_DATA_TYPE_ENUM.INT:
                        temp = dataContent.Substring(0, unit.TypeEnum.GetLength());
                        unit.value = BitConverter.ToInt32(temp.HexStrToBytes().Reverse().ToArray(), 0);
                        dataContent = dataContent.Substring(unit.TypeEnum.GetLength());
                        break;
                }
            }
        }
        
        /// <summary>
        /// 连接时需要发送连接包 包含时间戳
        /// </summary>
        /// <param name="SendAction"></param>
        /// <param name="connId"></param>
        public override void Connect(Action<IntPtr, byte[], string> SendAction, IntPtr connId)
        {
            this.SendData_Action = SendAction;
        }
    }
}
