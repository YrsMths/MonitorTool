using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Extension;

namespace Server.VIASYS
{
    [XmlRoot("profile")]
    public class Profile : BaseMsg
    {
        public Profile(){}
        [XmlAttribute("model")]
        public string model { get; set; }
        [XmlAttribute("profileVersion")]
        public string profileVersion { get; set; }
        [XmlAttribute("voxpVersion")]
        public string voxpVersion { get; set; }
        [XmlAttribute("textEncoding")]
        public string textEncoding { get; set; }
        [XmlAttribute("msgID")]
        public override int msgID { get; set; }
        [XmlElement("unit")]
        public List<Profile_Unit> units { get; set; } = new List<Profile_Unit>();

        [XmlIgnore]
        public Encoding encoding
        {
            get
            {
                switch (textEncoding)
                {
                    case "UTF-16":
                        return Encoding.GetEncoding("UTF-16BE");
                    default:
                        return Encoding.Default;
                }
            }
        }

        public List<Profile_Unit> GetUnits(string @class)
        {
            return units.Where(x => x.@class == @class).ToList();
        }
    }
    
    public class Profile_Unit
    {
        public Profile_Unit() { }
        [XmlAttribute("class")]
        public string @class { get; set; }
        [XmlAttribute("ID")]
        public string ID { get; set; }
        [XmlIgnore]
        string _Type;
        [XmlAttribute("type")]
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (Enum.TryParse(value, out _TypeEnum))
                {
                    _Type = value;
                }
            }
        }
        [XmlIgnore]
        VIASYS_DATA_TYPE_ENUM _TypeEnum;
        [XmlIgnore]
        public VIASYS_DATA_TYPE_ENUM TypeEnum
        {
            get
            {
                return _TypeEnum;
            }
            set
            {
                _Type = value.ToString();
                TypeEnum = value;
            }
        }
        [XmlAttribute("scale")]
        public string scale { get; set; }
        [XmlAttribute("resolution")]
        public string resolution { get; set; }
        [XmlAttribute("range")]
        public string range { get; set; }
        [XmlAttribute("units")]
        public string units { get; set; }
        [XmlAttribute("label")]
        public string label { get; set; }
        [XmlElement("enum")]
        public List<Profile_Enum> profile_Enums { get; set; }

        [XmlIgnore]
        public object value { get; set; }
    }

    public class Profile_Enum
    {
        [XmlAttribute("value")]
        public string value { get; set; }
        [XmlAttribute("label")]
        public string label { get; set; }
    }
}
