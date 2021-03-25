using System;
using System.Xml.Serialization;

namespace Fuel_calculator.Xml.Objects
{
    public class TrackCarCombo
    {
        [XmlRoot("Car_Track_Combo")]
        class CarTrackCombo
        {
            [XmlElement("Track")] public Track Track;
            [XmlElement("Car")] public Car Car;

            [XmlElement("Avarage_Lap_Time")] public Car AverageLapTime;
            [XmlElement("Fuel_Per_Lap")] public Car FuelPerLap;
        }
    }
}