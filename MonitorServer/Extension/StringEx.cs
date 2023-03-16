using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    public static class StringEx
    {
        /// <summary>
        /// HEX字符串解码
        /// </summary>
        /// <param name="encoding">编码方式</param>
        /// <param name="hexString">Hex字符串</param>
        /// <param name="IsBigEndian">Hex是否是大端格式</param>
        /// <returns></returns>
        public static string HexDecode(this Encoding encoding, string hexString)
        {
            byte[] bytes = hexString.HexStrToBytes();
            return encoding.GetString(bytes);
        }
        
        /// <summary>
        /// HEX字符串转byte数组
        /// </summary>
        /// <param name="hexString">HEX字符串</param>
        /// <returns></returns>
        public static byte[] HexStrToBytes(this string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException("参数长度不正确,必须是偶数位。");
            }
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return returnBytes;
        }
    }
}
