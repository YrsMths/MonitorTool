using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Mindray
{
    public enum HL7_VALUE_TYPE_ENUM
    {
        [Description("数值")] NM = 1,
        [Description("字符串")] ST = 2,
        [Description("比率")] SN = 3,
        [Description("枚举")] CNE = 4,
        [Description("无效")] NA = 5
    }
}
