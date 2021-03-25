using System;
using System.Xml.Serialization;

namespace Fuel_calculator.Xml.Objects
{
    [XmlRoot("Track")]
    class Track
    {
        [XmlElement("Name")] public Int32 Name;
    }
}
