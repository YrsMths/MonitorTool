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
        LiBang = 3,
        [Description("鸟牌")]
        VIASYS = 4
    }

    public enum DEPLOYMENT_MODE_ENUM
    {
        [Description("理邦直连")]
        LiBang_理邦直连 = 1,
        [Description("迈瑞直连")]
        Mindray_迈瑞直连 = 2,
        [Description("迈瑞eGateway")]
        Mindray_迈瑞eGateway = 3,
        [Description("迈瑞Separate")]
        Mindray_迈瑞Separate = 4,
        [Description("迈瑞eGateway(MV)")]
        Mindray_迈瑞eGateway_呼吸机 = 5,
        [Description("伟亚安直连")]
        VIASYS_伟亚安直连 = 6,
        [Description("科惠直连")]
        科惠直连 = 7,
        [Description("X9")]
        X9 = 8,
        [Description("迈瑞CCMS")]
        迈瑞CCMS = 9,
        [Description("迈瑞CMS")]
        迈瑞CMS = 10,
        [Description("飞利浦直连")]
        飞利浦直连 = 11,
        [Description("科曼Separate")]
        科曼Separate = 12,
        [Description("好络维直连")]
        好络维直连 = 13,
        [Description("迈柯维直连")]
        迈柯维直连 = 14,
        [Description("灵智eGetway")]
        灵智eGetway = 15,
        [Description("德尔格直连")]
        德尔格直连 = 16
    }

    public enum EQUIP
    {
        [Description("呼吸机")]
        MV = 1,
        [Description("监护仪")]
        ECG = 2
    }
}
