using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xml_deserializer.Xml_Objects;

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
        private CarInfo.Car _car;

        private static String SettingsSaveFilePath;



        public MainWindow()
        {
            InitializeComponent();
            foreach (KeyValuePair<CarInfo.Car, (String, Int32)> carFuelAmount in CarInfo.CarFuelAmounts.Where(carFuelAmount => carFuelAmount.Key != CarInfo.Car.Empty))
            {
                CarSelector.Items.Add(carFuelAmount.Value.Item1);
            }

            CarSelector.SelectedIndex = 0;
            FuelTankCapacity.Text = CarInfo.GetCarFeulFromCarEnum(CarInfo.GetCarFromCarName(CarSelector.Text)).ToString(CultureInfo.InvariantCulture);
           
            SettingsSaveFilePath = Directory.GetCurrentDirectory() + "\\SavedSettings.txt";

            //Load the data from the save file
            if (File.Exists(SettingsSaveFilePath))
            {
                Settings setting = Xml_deserializer.Xml.Deserialize<Settings>(SettingsSaveFilePath);

                TotalRaceTimeHours.Text = Math.DivRem(setting.TotalRaceTime, 3600, out Int32 raceMinutes).ToString(CultureInfo.InvariantCulture);
                TotalRaceTimeMinutes.Text = raceMinutes.ToString(CultureInfo.InvariantCulture);

                AverageLapTimeMinutes.Text = Math.DivRem(setting.AverageLapTime, 60, out Int32 averageLapTimeSeconds).ToString(CultureInfo.InvariantCulture);
                AverageLapTimeSeconds.Text = averageLapTimeSeconds.ToString(CultureInfo.InvariantCulture);

                FuelPerLap.Text = setting.FuelPerLap.ToString(CultureInfo.InvariantCulture);

                CarSelector.SelectedIndex = setting.Car;
            }

        }

        private void UpdateElements()
        {
            UpdateTotalLaps();
            UpdateLapsTillPitstop();
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
        private void UpdateLapsTillPitstop()
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

            _totalRaceTime = totalSeconds;
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

            _averageLapTime = totalSeconds;
            UpdateElements();
        }
        private void FuelPerLap_TextChanged(Object sender, TextChangedEventArgs e)
        {
            if (TotalLaps.Content.ToString() != "" && FuelPerLap.Text != "")
            {
                if (Single.TryParse(FuelPerLap.Text, out Single fuelPerLap))
                {
                    _fuelPerLap = Decimal.Parse(fuelPerLap.ToString(CultureInfo.InvariantCulture));
                    TotalFeulNeeded.Content = Int32.Parse(TotalLaps.Content.ToString()) * _fuelPerLap;
                }
            }

            UpdateElements();
        }
        private void FuelTankCapacityChanged(Object sender, TextChangedEventArgs e)
        {
            if (IsNumber(FuelTankCapacity.Text, out _fuelTankCapacity)) ; 
            UpdateElements();
        }
        private void CarSelector_OnSelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            // Activates the Update elements from FuelTankCapacityChanged()
            _car = CarInfo.GetCarFromCarName(e.AddedItems[0].ToString());
            FuelTankCapacity.Text = CarInfo.GetCarFuelFromCarName(e.AddedItems[0].ToString()).ToString(CultureInfo.InvariantCulture);
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
            Settings settings = new Settings
            {
                AverageLapTime = _averageLapTime,
                Car = (Int32) _car,
                FuelPerLap = _fuelPerLap,
                TotalRaceTime = _totalRaceTime
            };

            Xml_deserializer.Xml.Serialize(settings, SettingsSaveFilePath);
        }

        private void Window_Loaded(Object sender, RoutedEventArgs e)
        {
            if (File.Exists(SettingsSaveFilePath))
            {
                Settings settings = Xml_deserializer.Xml.Deserialize<Settings>(SettingsSaveFilePath);
            }
        }
    }
}
