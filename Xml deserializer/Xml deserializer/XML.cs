using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Xml_deserializer.Xml_Objects;

namespace Xml_deserializer
{
    public static class Xml
    {
        public static Boolean Serialize<T>(T objectToSerialize, String fileName)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (Stream stream = new FileStream(fileName, FileMode.Create))
                {
                    serializer.Serialize(stream, objectToSerialize);

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static T Deserialize<T>(String fileName) where T : class
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (Stream reader = new FileStream(fileName, FileMode.Open))
                {
                    return (T) serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
