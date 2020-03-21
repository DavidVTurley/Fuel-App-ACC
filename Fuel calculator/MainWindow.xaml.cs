using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using Fuel_calculator.Annotations;
using Fuel_calculator.Xml;
using Fuel_calculator.Xml.Fuel_calculator.Xml;

namespace Fuel_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///  CarInfo make and fuel load
        /// </summary>
        private Int32 _totalRaceTime;
        private Int32 _averageLapTime;
        private Decimal _fuelPerLap;
        private Int32 _fuelTankCapacity;
        private Enums.Car _car;
        private Enums.Track _track;

        private static String _settingsSaveFilePath;

        public MainWindow()
        {
            InitializeComponent();
            foreach (KeyValuePair<Enums.Car, (String car, Int32 fuel)> carFuelAmount in Enums.CarFuelAmounts.Where(carFuelAmount => carFuelAmount.Key != Enums.Car.Empty))
            {
                CarSelector.Items.Add(carFuelAmount.Value.car);
            }

            foreach (KeyValuePair<Enums.Track, String> value in Enums.TrackNames.Where(trackNames => trackNames.Key != Enums.Track.Empty))
            {
                TrackSelector.Items.Add(value.Value);
            }

            _settingsSaveFilePath = Directory.GetCurrentDirectory() + "\\SavedSettings.txt";

            //Load the data from the save file
            if (Xml_deserializer.Xml.Deserialize<OldSettings>(_settingsSaveFilePath) != null)
            {
                File.Delete(_settingsSaveFilePath);
            }

            Settings setting = Xml_deserializer.Xml.Deserialize<Settings>(_settingsSaveFilePath);

            if (setting != null)
            {
                // Total RaceTime
                SetTotalRaceTime(setting.CarTrackCombo[0].TotalRaceTime);
                // AverageLapTime
                SetAverageLap(setting.CarTrackCombo[0].AverageLapTime);
                // Fuel Per Lap
                SetFuelPerLap(setting.CarTrackCombo[0].FuelPerLap);
                // Set Selectors
                SetCarSelected((Enums.Car)setting.CarTrackCombo[0].Car);
                SetTrackSelected((Enums.Track)setting.CarTrackCombo[0].Track);
            }
            else
            {
                // Total RaceTime
                SetTotalRaceTime(0);
                // AverageLapTime
                SetAverageLap(0);
                // Fuel Per Lap
                SetFuelPerLap(0);
                // Set Selectors
                SetCarSelected((Enums.Car)0);
                SetTrackSelected((Enums.Track)0);
            }
        }
        private void Window_Loaded(Object sender, RoutedEventArgs e)
        {
            if (File.Exists(_settingsSaveFilePath))
            {
                Settings settings = Xml_deserializer.Xml.Deserialize<Settings>(_settingsSaveFilePath);
            }
        }

        //Calculations
        private void SetTotalRaceTime(Int32 totalRaceTime)
        {
            TotalRaceTimeHours.Text = Math.DivRem(totalRaceTime, 3600, out Int32 raceMinutes).ToString();
            TotalRaceTimeMinutes.Text = raceMinutes.ToString();
        }
        private void SetAverageLap(Int32 averageRaceTime)
        {
            AverageLapTimeMinutes.Text = Math.DivRem(averageRaceTime, 60, out Int32 averageLapTimeSeconds).ToString();
            AverageLapTimeSeconds.Text = averageLapTimeSeconds.ToString();
        }
        private void SetFuelPerLap(Decimal fuelPerLap)
        {
            FuelPerLap.Text = fuelPerLap.ToString();
        }
        private void SetCarSelected(Enums.Car car)
        {
            CarSelector.SelectedIndex = (Int32)car;
        }
        private void SetCarSelected(Int32 car)
        {
            CarSelector.SelectedIndex = car;
        }
        private void SetTrackSelected(Enums.Track track)
        {
            TrackSelector.SelectedIndex = (Int32)track;
        }
        private void SetTrackSelected(Int32 track)
        {
            TrackSelector.SelectedIndex = track;
        }

        private void UpdateElements()
        {
            UpdateTotalLaps();
            UpdateLapsTillPitStop();
            UpdateTotalFuelNeeded();
        }

        private void UpdateTotalLaps()
        {
            if (_totalRaceTime <= 0 || _averageLapTime <= 0) return;
            if (_totalRaceTime == 0 || _averageLapTime == 0)
            {
                TotalLaps.Content = "0";
            }

            // ReSharper disable once PossibleLossOfFraction
            Decimal laps = (Decimal) _totalRaceTime / _averageLapTime;
            TotalLaps.Content = ((Int32)Math.Ceiling(laps)).ToString();
        }
        private void UpdateLapsTillPitStop()
        {
            if (_fuelTankCapacity > 0 && _fuelPerLap > 0)
            {
                LapsTillPitstop.Content = Math.Ceiling(_fuelTankCapacity / _fuelPerLap);
            }
        }
        private void UpdateTotalFuelNeeded()
        {
            if (_totalRaceTime > 0 && _averageLapTime > 0 && _fuelPerLap > 0 )
            {
                TotalFeulNeeded.Content = Math.DivRem(_totalRaceTime, _averageLapTime, out _) * _fuelPerLap;
            }
        }

        // UI Update events
        private void TotalRaceTime_TextChanged(Object sender, TextChangedEventArgs e)
        {
            Int32 totalSeconds = 0;
            if (IsNumber(TotalRaceTimeHours.Text, out Int32 number) && number > 0)
            {
                totalSeconds += number * 3600;
            }
            if (IsNumber(TotalRaceTimeMinutes.Text, out Int32 number1))
            {
                totalSeconds += number1 * 60;
            }

            SetTotalRaceTime(totalSeconds);
            UpdateElements();
        }
        private void AverageLapTime_TextChanged(Object sender, TextChangedEventArgs e)
        {
            Int32 totalSeconds = 0;
            if (IsNumber(AverageLapTimeMinutes.Text, out Int32 number) && number >= 0)
            {
                totalSeconds += number * 60;
            }
            if (IsNumber(AverageLapTimeSeconds.Text, out Int32 number1))
            {
                totalSeconds += number1;
            }

            SetAverageLap(totalSeconds);
            UpdateElements();
        }
        private void FuelPerLap_TextChanged(Object sender, TextChangedEventArgs e)
        {
            if (TotalLaps.Content.ToString() != "" && FuelPerLap.Text != "")
            {
                if (Single.TryParse(FuelPerLap.Text, out Single fuelPerLap))
                {
                    _fuelPerLap = Decimal.Parse(fuelPerLap.ToString());
                    TotalFeulNeeded.Content = Int32.Parse(TotalLaps.Content.ToString()) * _fuelPerLap;
                }
            }

            UpdateElements();
        }
        private void FuelTankCapacityChanged(Object sender, TextChangedEventArgs e)
        {
            if (IsNumber(FuelTankCapacity.Text, out _fuelTankCapacity))
            UpdateElements();
        }
        private void CarSelector_OnSelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            // Activates the Update elements from FuelTankCapacityChanged()
            _car = Enums.GetCarFromCarName(e.AddedItems[0].ToString());
            FuelTankCapacity.Text = Enums.GetCarFuelFromCarName(e.AddedItems[0].ToString()).ToString();

            Settings settings = Xml_deserializer.Xml.Deserialize<Settings>(_settingsSaveFilePath);
            if (settings != null)
            {
                CarTrackCombo carTrackCombo = GetCarTrackComboData(settings);
                if (carTrackCombo != null)
                {
                    SetTotalRaceTime(carTrackCombo.TotalRaceTime);
                    SetAverageLap(carTrackCombo.AverageLapTime);
                    SetFuelPerLap(carTrackCombo.FuelPerLap);
                    SetCarSelected(_car);
                }
                SetCarSelected(CarSelector.SelectedIndex);
            }
            SetCarSelected(CarSelector.SelectedIndex);
        }
        private void TrackSelector_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            _track = Enums.GetTrackEnum(e.AddedItems[0].ToString());
            
            Settings settings = Xml_deserializer.Xml.Deserialize<Settings>(_settingsSaveFilePath);
            if (settings != null)
            {
                CarTrackCombo carTrackCombo = GetCarTrackComboData(settings);
                if (carTrackCombo != null)
                {
                    SetTotalRaceTime(carTrackCombo.TotalRaceTime);
                    SetAverageLap(carTrackCombo.AverageLapTime);
                    SetFuelPerLap(carTrackCombo.FuelPerLap);
                    SetTrackSelected(_track);
                }
                SetTrackSelected(TrackSelector.SelectedIndex);
            }
            SetTrackSelected(TrackSelector.SelectedIndex);
        }

        private CarTrackCombo GetCarTrackComboData(Settings settings)
        {
            foreach (CarTrackCombo carTrackCombo in settings.CarTrackCombo)
            {
                if (carTrackCombo.Car == (Int32) _car && carTrackCombo.Track == (Int32) _track)
                {
                    return carTrackCombo;
                }
            }

            return null;
        }

        // Utilities
        private static Boolean IsNumber(String value, out Int32 number)
        {
            return Int32.TryParse(value, out number);
        }
        private static Boolean IsFloat(String value, out Single number)
        {
            return Single.TryParse(value, out number);
        }

        private void OnSaveClick(Object sender, RoutedEventArgs e)
        {
            Settings settings = Xml_deserializer.Xml.Deserialize<Settings>(_settingsSaveFilePath);
            if (settings != null)
            {
                for (Int32 i = 0; i < settings.CarTrackCombo.Count; i++)
                {
                    CarTrackCombo value = settings.CarTrackCombo[i];

                    if (value.Car != (Int32) _car || value.Track != (Int32) _track) continue;

                    settings.CarTrackCombo[i].AverageLapTime = _averageLapTime;
                    settings.CarTrackCombo[i].Car = (Int32) _car;
                    settings.CarTrackCombo[i].FuelPerLap = _fuelPerLap;
                    settings.CarTrackCombo[i].TotalRaceTime = _totalRaceTime;

                    Xml_deserializer.Xml.Serialize(settings, _settingsSaveFilePath);

                    return;
                }
                settings.CarTrackCombo.Add(
                    new CarTrackCombo()
                    {
                        Car = (Int32) _car,
                        Track = (Int32) _track,
                        AverageLapTime = _averageLapTime,
                        FuelPerLap = _fuelPerLap,
                        TotalRaceTime = _totalRaceTime,
                    });

                Xml_deserializer.Xml.Serialize(settings, _settingsSaveFilePath);
                return;
            }
            else
            {

                settings = new Settings
                {  
                    CarTrackCombo = new List<CarTrackCombo>
                    {
                        new CarTrackCombo()
                        {
                            Car = (Int32)_car,
                            Track = (Int32)_track,
                            AverageLapTime = _averageLapTime,
                            FuelPerLap = _fuelPerLap,
                            TotalRaceTime = _totalRaceTime,
                        }
                    }
                };
            }

            Xml_deserializer.Xml.Serialize(settings, _settingsSaveFilePath);
        }
    }
}
