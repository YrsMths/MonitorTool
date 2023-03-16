using Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server.VIASYS
{
    public class VIASYSProtocol<T> where T : BaseMsg
    {
        public VIASYSProtocol(string msg)
        {
            try
            {
                this.msgObject = msg.XmlDeserialize<T>();
                if (null != msgObject) isAvilable = true;
                this.msg = msg;
                switch (typeof(T).Name)
                {
                    case "Link":
                        MsgType = VIASYS_MES_TYPE_ENUM.link;
                        break;
                    case "Config":
                        MsgType = VIASYS_MES_TYPE_ENUM.config;
                        break;
                    case "Profile":
                        MsgType = VIASYS_MES_TYPE_ENUM.profile;
                        break;
                    case "Data":
                        MsgType = VIASYS_MES_TYPE_ENUM.data;
                        break;
                }
            }
            catch (Exception ex)
            {
                this.isAvilable = false;
            }
        }
        /// <summary>
        /// 数据体
        /// </summary>
        public string msg { get; private set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public VIASYS_MES_TYPE_ENUM MsgType { get; set; }
        /// <summary>
        /// 数据实例
        /// </summary>
        public T msgObject { get; set; }
        /// <summary>
        /// 回复
        /// </summary>
        public object responObject { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        public int msgID { get { return null == msgObject ? -1 : msgObject.msgID; } }
        /// <summary>
        /// 数据是否有效
        /// </summary>
        public bool isAvilable { get; set; } = false;
        /// <summary>
        /// 组装数据包
        /// </summary>
        /// <returns></returns>
        public List<byte[]> GetResponse()
        {
            List<byte[]> bytesList = new List<byte[]>();
            if (!isAvilable) return null;
            switch (typeof(T).Name)
            {
                case "Link":
                    Link link = msgObject as Link;
                    switch (link.cmdEnum)
                    {
                        case VIASYS_LINK_COMMAND_ID_ENUM.ping:
                            Link sendprofilelink = new Link(VIASYS_LINK_COMMAND_ID_ENUM.sendprofile, link.msgID);
                            bytesList.Add(Encoding.Default.GetBytes(sendprofilelink.XmlSerializer<Link>()));
                            return bytesList;
                    }
                    break;
                case "Profile":
                    Profile profile = msgObject as Profile;
                    Config config = new Config();
                    config.msgID = profile.msgID;
                    config.mode = profile.model;
                    config.units = profile.units.ConvertAll(x => new Config_Unit(x.@class, x.ID));
                    this.responObject = config;
                    Link querylink = new Link(VIASYS_LINK_COMMAND_ID_ENUM.query, profile.msgID);
                    bytesList.Add(Encoding.Default.GetBytes(config.XmlSerializer<Config>()));
                    bytesList.Add(Encoding.Default.GetBytes(querylink.XmlSerializer<Link>()));
                    return bytesList;
            }
            return new List<byte[]>();
        }
        
        public byte[] GetAck()
        {
            if (!isAvilable) return null;
            Link link = new Link(VIASYS_LINK_COMMAND_ID_ENUM.ack, this.msgID);
            return Encoding.Default.GetBytes(link.XmlSerializer<Link>());
        }
    }
}
