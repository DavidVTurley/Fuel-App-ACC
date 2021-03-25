using System;
using System.Xml.Serialization;

namespace Xml_deserializer.Xml_Objects
{
    [XmlRoot("catalog")]
    public class Catalog
    {
        [XmlElement("book")] public Book[] Books;
    }

    public class Book
    {
        [XmlAttribute("id")]public String ID;

        [XmlElement("author")] public String Author;
        [XmlElement("title")] public String Title;
        [XmlElement("genre")] public String Genre;
        [XmlElement("price")] public String Price;
        [XmlElement("publish_date")] public String PublishDate;
        [XmlElement("description")] public String Description;
    }
}