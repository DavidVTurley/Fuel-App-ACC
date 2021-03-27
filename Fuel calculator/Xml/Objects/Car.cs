using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fuel_calculator.Xml.Objects
{
    [XmlRoot("Car")]
    public class Car
    {
        [XmlElement("Id")] public Int32 Id;
        [XmlElement("Name")] public String Name;
        [XmlElement("MaxFuel")] public Int32 MaxFuel;
    }

    [XmlRoot("Cars")]
    public class Cars
    {
        [XmlElement("Car")] public List<Car> Name;


        public static Cars LoadCarsFromXml()
        {
            return Xml_deserializer.Xml.Deserialize<Cars>(Directory.GetCurrentDirectory() + "\\Xml\\Cars.xml");
        }
    }

}
