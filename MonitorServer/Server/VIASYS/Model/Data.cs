using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Server.VIASYS
{
    [XmlRoot("data")]
    public class Data : BaseMsg
    {
        public Data() { }
        [XmlAttribute("class")]
        public string @class{ get;set;}
        [XmlAttribute("crc")]
        public string crc { get; set; }
        [XmlText]
        public string content { get; set; }
        [XmlAttribute("msgID")]
        public override int msgID { get ; set; }
    }
}
