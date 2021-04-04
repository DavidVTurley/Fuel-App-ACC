using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;
using Xml_deserializer;

namespace Fuel_calculator.Model
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class SavedSetups
    {
        private CarTrackCombo[] _carTrackComboField;

        /// <remarks/>
        [XmlElement("CarTrackCombo")]
        public CarTrackCombo[] CarTrackCombo
        {
            get => _carTrackComboField;
            set => _carTrackComboField = value;
        }




        [XmlIgnore] public static readonly String SavedSetupsFileLocation = Directory.GetCurrentDirectory() + "\\Xml\\SavedSetups.xml";

        public static SavedSetups LoadCarTrackComboFromXml()
        {
            return Xml_deserializer.Xml.Deserialize<SavedSetups>(SavedSetupsFileLocation);
        }

        public static Boolean SaveCarTrackComboToXml(SavedSetups savedSetups)
        {
            return Xml_deserializer.Xml.Serialize(savedSetups, SavedSetupsFileLocation);
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class CarTrackCombo
    {
        private UInt32 _carIdField;
        private UInt32 _trackIdField;
        private Decimal _fuelPerLapField;

        /// <remarks/>
        public UInt32 Car
        {
            get => _carIdField;
            set => _carIdField = value;
        }
        /// <remarks/>
        public UInt32 Track
        {
            get => _trackIdField;
            set => _trackIdField = value;
        }
        /// <remarks/>
        public Decimal FuelPerLap
        {
            get => _fuelPerLapField;
            set => _fuelPerLapField = value;
        }

        /// <remarks/>
        public UInt32 AverageLapTimeInSeconds
        {
            get => (UInt32) AverageLapTime.TotalSeconds;
            set
            {
                if (value == AverageLapTimeInSeconds) return;
                if (value <= 0)
                {
                    Logger.WriteToLog("AverageLapTimeInSeconds is less than 0");
                    MessageBox.Show("SavedSetups.xml has an invalid AverageLapTimeInSeconds. It cannot be negative.",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                AverageLapTime = TimeSpan.FromSeconds(value);
                AverageLapTimeInSeconds = value;
            }
        }

        /// <remarks/>
        public UInt32 TotalRaceTimeInMinutes
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


        [XmlIgnore] public TimeSpan TotalRaceTime;
        [XmlIgnore] public TimeSpan AverageLapTime;
    }
}