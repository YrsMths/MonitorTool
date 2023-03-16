using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.VIASYS
{
    public enum VIASYS_DATA_TYPE_ENUM
    {
        [Description("bool"),Length(1)] BOOL = 1,
        [Description("byte"),Length(2)] BYTE = 2,
        [Description("ubyte"),Length(2)] UBYTE = 3,
        [Description("short"),Length(4)] WORD = 4,
        [Description("ushort"),Length(4)] UWORD = 5,
        [Description("int"),Length(8)] INT = 6,
        [Description("uint"),Length(8)] UINT = 7,
        [Description("double"),Length(8)] FLOAT = 8,
        [Description("string"),Length(8)] TEXT = 9,
        [Description("ushort"),Length(4)] ENUM = 10
    }
}
