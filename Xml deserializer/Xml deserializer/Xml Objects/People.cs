using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace Xml_deserializer.Xml_Objects
{ 
    [XmlRoot("people")]
    public class People
    {
         [XmlElement("person")]public List<Person> Person;
    }

    public class Person
    {
        [XmlElement("first name")] public String FirstName;
        [XmlElement("last name")] public String LastName;
        [XmlElement("dateOfBirth")] public DateTime DateOfBirth;

        public Int32 Age => Int32.Parse((DateTime.UtcNow - DateOfBirth).TotalDays.ToString(CultureInfo.InvariantCulture)) / 365;
    }

}