using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace Fuel_calculator.Xml.Objects
{
    [XmlRoot("Car_Track_Combo")]
    public class CarTrackCombo
    {
        [XmlElement("Track")] public Track Track;
        [XmlElement("Car")] public Car Car;
    }

    [XmlRoot("SavedCombinations")]
    public class SavedCombinations
    {
        [XmlElement("CarTrackCombo")] public List<CarTrackCombo> CarTrackCombo;
        [XmlElement("FuelPerLap")] public Single FuelPerLap;


        public static SavedCombinations LoadCarTrackComboFromXml()
        {
            return Xml_deserializer.Xml.Deserialize<SavedCombinations>(Directory.GetCurrentDirectory() + "\\Xml\\CarTrackCombo.xml");
        }
    }
}