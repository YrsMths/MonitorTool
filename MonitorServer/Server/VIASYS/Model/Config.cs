using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Server.VIASYS
{
    [XmlRoot("config")]
    public class Config: BaseMsg
    {
        [XmlAttribute("msgID")]
        public override int msgID { get; set; }
        [XmlAttribute("model")]
        public string mode;
        [XmlElement("unit")]
        public List<Config_Unit> units;
    }

    public class Config_Unit
    {
        public Config_Unit() { }
        public Config_Unit(string @class, string ID)
        {
            this.@class = @class;
            this.ID = ID;
        }
        [XmlAttribute("class")]
        public string @class;
        [XmlAttribute("ID")]
        public string ID;
        
    }
}
