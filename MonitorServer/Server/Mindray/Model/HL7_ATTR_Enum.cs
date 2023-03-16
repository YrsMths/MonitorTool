using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Mindray
{
    public enum HL7_ATTR_ENUM
    {
        [Description("采样率")]
        MDC_ATTR_SAMP_RATE = 1,
        [Description("精度")]
        MDC_ATTR_NU_MSMT_RES = 2
    }
}
