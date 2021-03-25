using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fuel_calculator.Xml.Objects
{
    [XmlRoot("Car")]
    class Car
    {
        [XmlElement("Name")] public Int32 Name;
        [XmlElement("Max_fuel")] public Int32 MaxFuel;
    }
}
