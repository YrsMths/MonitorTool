using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Extension
{
    public static class TypeEx
    {
        /// <summary>
        /// 获取属性的描述信息
        /// </summary>
        public static string GetDescription(this Type type, string proName)
        {
            PropertyInfo pro = type.GetProperty(proName);
            string des = "";
            if (pro != null)
            {
                des = pro.GetDescription();
            }
            return des;
        }

        /// <summary>
        /// 获取属性的描述信息
        /// </summary>
        public static string GetDescription(this MemberInfo info)
        {
            var attrs = (DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);
            string des = "";
            foreach (DescriptionAttribute attr in attrs)
            {
                des = attr.Description;
            }
            return des;
        }

        
    }
}
