using Server.VIASYS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class EnumEx
    {
        public static int GetLength(this VIASYS_DATA_TYPE_ENUM @enum)
        {
            Type type = @enum.GetType();
            FieldInfo fd = type.GetField(@enum.ToString());
            int length = 0;
            if (fd != null)
            {
                object[] attrs = fd.GetCustomAttributes(typeof(LengthAttribute), false);
                foreach (LengthAttribute attr in attrs)
                {
                    length = attr.Length;
                }
            }
            return length;
        }
    }
}
