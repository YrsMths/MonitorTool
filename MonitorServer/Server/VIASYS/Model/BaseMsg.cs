using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Server.VIASYS
{
    public abstract class BaseMsg
    {
        [XmlAttribute("msgID")]
        public abstract int msgID { get; set; }
    }
}
