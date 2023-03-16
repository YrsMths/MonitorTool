using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Extension
{
    public static class XmlEx
    {
        public static string XmlSerializer<T>(this object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;  // 不生成声明头
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.NewLineChars = @"
";
            
            try
            {
                //序列化对象
                using (XmlWriter xmlWriter = XmlWriter.Create(Stream, settings))
                {
                    XmlSerializerNamespaces _namespaces = new XmlSerializerNamespaces();
                    _namespaces.Add("", "");
                    xml.Serialize(xmlWriter, obj, _namespaces);
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();
            sr.Dispose();
            Stream.Dispose();
            return str;
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="stream">xml数据流</param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(this Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(typeof(T));
            return (T)(xmldes.Deserialize(stream));
        }
        
        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml">xml字符串</param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(this string xml) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(T));
                    return (T)(xmldes.Deserialize(sr));
                }
            }
            catch (Exception e)
            {
                return default(T);
            }
        }
    }
}
