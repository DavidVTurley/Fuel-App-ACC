using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace Fuel_calculator.Xml.Objects
{
    [XmlRoot("Car_Track_Combo")]
    public class CarTrackCombo
    {
        [XmlElement("Track")] public Track Track;
        [XmlElement("Car")] public Car Car;



        [XmlElement("FuelPerLap")] public Decimal FuelPerLap;

        [XmlElement("TotalRaceTimeInMinutes")] public UInt32 TotalRaceTimeInMinutes
        {
            get => (UInt32)TotalRaceTime.TotalMinutes;
            set
            {
                if (value == TotalRaceTimeInMinutes) return;
                if (value <= 0)
                {
                    Logger.WriteToLog("TotalRaceTimeInMinutes is less than 0");
                    MessageBox.Show("SavedSetups.xml has an invalid TotalRaceTimeInMinutes. It cannot be negative.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                TotalRaceTime = TimeSpan.FromMinutes(value);
                TotalRaceTimeInMinutes = value;
            }
        }
        [XmlElement("AverageLapTimeInSeconds")] public UInt32 AverageLapTimeInSeconds
        {
            get => (UInt32)AverageLapTime.TotalSeconds;
            set
            {
                if(value == AverageLapTimeInSeconds) return;
                if (value <= 0)
                {
                    Logger.WriteToLog("AverageLapTimeInSeconds is less than 0");
                    MessageBox.Show("SavedSetups.xml has an invalid AverageLapTimeInSeconds. It cannot be negative.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                AverageLapTime = TimeSpan.FromSeconds(value);
                AverageLapTimeInSeconds = value;
            }
        }


        [XmlIgnore] public TimeSpan TotalRaceTime;
        [XmlIgnore] public TimeSpan AverageLapTime;
    }

    [XmlRoot("SavedSetups")]
    public class SavedSetups
    {
        [XmlElement("CarTrackCombo")] public List<CarTrackCombo> CarTrackCombo;
        



        public static SavedSetups LoadCarTrackComboFromXml()
        {
            return Xml_deserializer.Xml.Deserialize<SavedSetups>(Directory.GetCurrentDirectory() + "..\\Xml\\CarTrackCombo.xml");
        }
    }
}