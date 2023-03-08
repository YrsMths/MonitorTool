using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    public static class EnumEx
    {
        public static string GetDescription(this Enum @enum)
        {
            Type type = @enum.GetType();
            FieldInfo fd = type.GetField(@enum.ToString());
            string des = "";
            if (fd != null)
            {
                des = fd.GetDescription();
            }
            return des;
        }
    }
}
