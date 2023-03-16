using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Extension;

namespace Server.VIASYS
{
    [XmlRoot("link")]
    public class Link :BaseMsg
    {
        public Link() { }
        public Link(VIASYS_LINK_COMMAND_ID_ENUM cmd, int msgID)
        {
            this.cmdEnum = cmd;
            this.msgID = msgID;
        }

        [XmlIgnore]
        VIASYS_LINK_COMMAND_ID_ENUM _cmdEnum;
        [XmlIgnore]
        public VIASYS_LINK_COMMAND_ID_ENUM cmdEnum
        {
            get
            {
                return _cmdEnum;
            }
            set
            {
                _cmdEnum = value;
                _cmd = value.GetDescription();
            }
        }
        [XmlIgnore]
        string _cmd;
        [XmlAttribute("cmd")]
        public string cmd
        {
            get
            {
                return _cmd;
            }
            set
            {
                string temp = value.Replace("-", "");
                if(Enum.TryParse(temp,out _cmdEnum))
                {
                    _cmd = value;
                }
            }
        }

        [XmlAttribute("class")]
        public string @class{ get;set;}

        [XmlAttribute("msgID")]
        public override int msgID { get; set; }
    }
}
