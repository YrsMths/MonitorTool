using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.VIASYS
{
    public enum VIASYS_LINK_COMMAND_ID_ENUM
    {
        [Description("ping")] ping = 0,
        [Description("ack")] ack = 1,
        [Description("send-profile")] sendprofile = 2,
        [Description("query")] query = 3
    }
}
