using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class Helper
    {
        public static object HL7TypeConverter(this string value, HL7_VALUE_TYPE_ENUM valueType)
        {
            try
            {
                switch (valueType)
                {
                    case HL7_VALUE_TYPE_ENUM.CNE://枚举
                        return value;
                    case HL7_VALUE_TYPE_ENUM.NM://数值
                        return Double.Parse(value);
                    case HL7_VALUE_TYPE_ENUM.SN://比率
                        return string.Format("{0}%", value);
                    case HL7_VALUE_TYPE_ENUM.ST://字符串
                        return value;
                    case HL7_VALUE_TYPE_ENUM.NA://无效
                        return null;
                }
            }
            catch (Exception ex) { }
            return null;
        }
    }
}
