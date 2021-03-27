using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Fuel_calculator.Xml.Objects
{
    [XmlRoot("Car_Track_Combo")]
    public class CarTrackCombo
    {
        [XmlElement("Track")] public Track Track;
        [XmlElement("Car")] public Car Car;
    }

    [XmlRoot("SavedSetups")]
    public class SavedSetups
    {
        [XmlElement("CarTrackCombo")] public List<CarTrackCombo> CarTrackCombo;
        [XmlElement("FuelPerLap")] public Single FuelPerLap;


        public static SavedSetups LoadCarTrackComboFromXml()
        {
            return Xml_deserializer.Xml.Deserialize<SavedSetups>(Directory.GetCurrentDirectory() + "\\Xml\\CarTrackCombo.xml");
        }
    }
}