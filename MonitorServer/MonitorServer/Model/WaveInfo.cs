using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorServer
{
    [Serializable]
    public class WaveInfo
    {
        [Description("波形")]
        public string LeadName { get; set; }
        
        [Description("编码")]
        public int Id { get; set; }
        
        [Description("采样率")]
        public object samp { get; set; }

        [Description("精度")]
        public object msmt { get; set; }

        public WaveInfo(string LeadName, int Id,object samp, object msmt)
        {
            this.LeadName = LeadName;
            this.Id = Id;
            this.samp = samp;
            this.msmt = msmt;
        }
    }
}
