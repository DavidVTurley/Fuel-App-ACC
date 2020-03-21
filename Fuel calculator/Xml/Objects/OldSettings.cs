using System;
using System.Xml.Serialization;

namespace Fuel_calculator.Xml
{
    namespace Fuel_calculator.Xml
    {
        [XmlRoot("settings")]
        public class OldSettings
        {
            [XmlElement("TotalRaceTime")] public Int32 TotalRaceTime;
            [XmlElement("AverageLapTime")] public Int32 AverageLapTime;
            [XmlElement("FuelPerLap")] public Decimal FuelPerLap;
            [XmlElement("Car")] public Int32 Car;

        }
    }
}