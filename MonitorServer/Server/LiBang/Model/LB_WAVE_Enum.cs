using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.LiBang
{
    public enum LB_WAVE_ENUM
    {
        [Description("波形")] NET_PHYSIOLOGY_WAVE_ECG_I = 0, 
        [Description("ECG-II")] NET_PHYSIOLOGY_WAVE_ECG_II = 1,
        [Description("ECG-III")] NET_PHYSIOLOGY_WAYE_ECG_III = 2,
        [Description("ECG-AVR")] NET_PHYSIOLOGY_WAVEY_ECG_AVR = 3,
        [Description("ECG-AVL")] NET_PHYSIOLOGY_WAVE_ECG_AVL = 4,
        [Description("ECG-AVF")] NET_PHYSIOLOGY_WAVE_ECG_AVF = 5,
        [Description("ECG-V1")] NET_PHYSIOLOGY_WAVE_ECG_V1 = 6,
        [Description("ECG-V2")] NET_PHYSIOLOGY_WAVE_ECG_V2 = 7,
        [Description("ECG-V3")] NET_PHYSIOLOGY_WAVE_ECG_V3 = 8,
        [Description("ECG-V4")] NET_PHYSIOLOGY_WAVE_ECG_V4 = 9,
        [Description("ECG-V5")] NET_PHYSIOLOGY_WAVE_ECG_V5 = 10,
        [Description("ECG-V6")] NET_PHYSIOLOGY_WAVE_ECG_V6 = 11,
        [Description("RESP 波形")] NET_PHYSIOLOGY_WAVE_RESP = 12,
        [Description("血氧")] NET_PHYSIOLOGY_WAVE_PLETH = 13, 
        [Description("IBP 动脉压")] NET_PHYSIOLOGY_WAVE_IBP_ART = 14,
        [Description("IBP 肺动脉压")] NET_PHYSIOLOGY_WAVE_IBP_PA = 15,
        [Description("中心静脉压")] NET_PHYSIOLOGY_WAVE_IBP_CVP = 16, 
        [Description("右房压")] NET_PHYSIOLOGY_WAVE_IBP_RAP = 17, 
        [Description("左房压")] NET_PHYSIOLOGY_WAVE_IBP_LAP = 18, 
        [Description("颅内压")] NET_PHYSIOLOGY_WAVE_IBP_ICP = 19, 
        [Description("扩展压力")] NET_PHYSIOLOGY_WAVE_IBP_PI = 20, 
        [Description("扩展压力")] NET_PHYSIOLOGY_WAVE_IBP_P2 = 21, 
        [Description("CO2模块的CO2")] NET_PHYSIOLOGY_WAVE_CO2_CO2 = 22,
        [Description("AG模块的CO2")] NET_PHYSIOLOGY_WAVE_AG_CO2 = 23,
        [Description("氧气")] NET_PHYSIOLOGY_WAVE_AG_O2 = 24, 
        [Description("笑气")] NET_PHYSIOLOGY_WAVE_AG_N20 = 25, 
        [Description("麻醉气体")] NET_PHYSIOLOGY_WAVE_AG_HAL = 26,
        [Description("AG-ENF")] NET_PHYSIOLOGY_WAVE_AG_ENF = 27,
        [Description("AG-ISO")] NET_PHYSIOLOGY_WAVE_AG_ISO = 28,
        [Description("AG-SEV")] NET_PHYSIOLOGY_WAVE_AG_SEV = 29,
        [Description("AG-DES")] NET_PHYSIOLOGY_WAVE_AG_DES = 30,
        [Description("AG-AX")] NET_PHYSIOLOGY_WAVE_AG_AX = 31, //当麻醉模块还没有识别麻醉剂的时候，单独使用AA
        [Description("RM-CO2")] NET_PHYSIOLOGY_WAVE_RM_CO2 = 32,
        [Description("RM-PAW")] NET_PHYSIOLOGY_WAVE_RM_PAW = 33,
        [Description("RM-FLOW")] NET_PHYSIOLOGY_WAVE_RM_FLOW = 34, 
        [Description("RM-VOL")] NET_PHYSIOLOGY_WAVE_RM_VOL = 35, 
        [Description("BIS_EEG")] NET_PHYSIOLOGY_WAVE_BIS_EEG = 36,
        [Description("ICG")] NET_PHYSIOLOGY_WAVE_ICG = 37,
        [Description("心电波起搏标记")] NET_PHYSIOLOGY_WAVE_ECG_PACE = 100,
        [Description("波形最大数量")] NET_PHYSIOLOGY_WAVE_MAX, 
    }
}
