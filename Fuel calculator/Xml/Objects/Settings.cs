using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fuel_calculator.Xml
{
    [XmlRoot("Settings")]
    public class Settings
    {
        [XmlElement("CarTrack")] public List<CarTrackCombo> CarTrackCombo;
    }

    public class CarTrackCombo
    {
        [XmlElement("Track")] public Int32 Track;
        [XmlElement("Car")] public Int32 Car;

        [XmlElement("TotalRaceTime")] public Int32 TotalRaceTime;
        [XmlElement("AverageLapTime")] public Int32 AverageLapTime;
        [XmlElement("FuelPerLap")] public Single FuelPerLap;
    }
}