using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public enum PROD_BRAND_ENUM
    {
        [Description("--")]
        None = 0,
        [Description("迈瑞")]
        Mindray = 1,
        [Description("伟亚安")]
        Viaan = 2,
        [Description("理邦")]
        LiBang = 3
    }

    public enum DEPLOYMENT_MODE_ENUM
    {
        [Description("理邦直连")]
        理邦直连 = 1,
        [Description("迈瑞直连")]
        迈瑞直连 = 2,
        [Description("迈瑞eGateway")]
        迈瑞eGateway = 3,
        [Description("迈瑞Separate")]
        迈瑞Separate = 4,
        [Description("迈瑞eGateway(MV)")]
        迈瑞eGateway_呼吸机 = 5
    }

    public enum EQUIP
    {
        [Description("呼吸机")]
        MV = 1,
        [Description("监护仪")]
        ECG = 2
    }
}
