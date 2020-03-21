using System;
using Xml_deserializer.Xml_Objects;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            String fileToDeserialize = @"C:\Users\David\source\repos\Xml deserializer\Xml deserializer\XML Samples\Catalog.xml";

            Object x = Xml_deserializer.Xml.TestDeserialize();

            Catalog catalog = (Catalog)Xml_deserializer.Xml.Deserialize<Catalog>(fileToDeserialize);
        }
    }
}
