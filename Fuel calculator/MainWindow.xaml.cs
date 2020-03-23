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
        private Single _fuelPerLap;
        private Int32 _fuelTankCapacity;
        private Enums.Car _car;
        private Enums.Track _track;

        // Debug
        private DateTime dateTime = DateTime.Now;
        
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
                CalculateRaceTime(setting.CarTrackCombo[0].TotalRaceTime, out Int32 hours, out Int32 minutes);
                TotalRaceTimeHours.Text = hours.ToString();
                TotalRaceTimeMinutes.Text = minutes.ToString();

                // AverageLapTime
                CalculateAvarageLapTime(setting.CarTrackCombo[0].AverageLapTime, out Int32 minutesAv, out Int32 seconds);
                AverageLapTimeMinutes.Text = minutesAv.ToString();
                AverageLapTimeSeconds.Text = seconds.ToString();

                // Fuel Per Lap
                FuelPerLap.Text = setting.CarTrackCombo[0].FuelPerLap.ToString();

                // Set Selectors
                CarSelector.SelectedIndex = setting.CarTrackCombo[0].Car;
                TrackSelector.SelectedIndex = setting.CarTrackCombo[0].Track;
            }
            else
            {
                TotalRaceTimeHours.Text = 0.ToString();
                TotalRaceTimeMinutes.Text = 0.ToString();
                AverageLapTimeMinutes.Text = 0.ToString();
                AverageLapTimeSeconds.Text = 0.ToString();
                FuelPerLap.Text = 0.ToString();

                CarSelector.SelectedIndex = 0;
                TrackSelector.SelectedIndex = 0;
            }

            UpdateElements();
        }

        //Calculations
        private void CalculateRaceTime(Int32 totalRaceTimeInSeconds, out Int32 hours, out Int32 minutes)
        {
            hours = Math.DivRem(totalRaceTimeInSeconds, 3600, out minutes);
        }
        private void CalculateAvarageLapTime(Int32 avarageLapTimeInSeconds, out Int32 minutes, out Int32 seconds)
        {
            minutes = Math.DivRem(avarageLapTimeInSeconds, 60, out seconds);
        }

        private void UpdateElements()
        {
            UpdateTotalLaps();
            UpdateMaxStintLength();
            UpdateTotalFuelNeeded();
        }

        private void UpdateTotalLaps()
        {
            if (_totalRaceTime <= 0 || _averageLapTime <= 0)
            {
                TotalLaps.Content = "0";

                return;
            }
            if (_totalRaceTime == 0 || _averageLapTime == 0) return;
            

                // ReSharper disable once PossibleLossOfFraction
            Decimal laps = (Decimal) _totalRaceTime / _averageLapTime;
            TotalLaps.Content = ((Int32)Math.Ceiling(laps)).ToString();
        }
        private void UpdateMaxStintLength()
        {
            if (_fuelTankCapacity > 0 && _fuelPerLap > 0)
            {
                MaxStintLength.Content = (_fuelTankCapacity / _fuelPerLap).ToString("0.00");
            }
            else
            {
                MaxStintLength.Content = 0;
            }
        }
        private void UpdateTotalFuelNeeded()
        {
            if (_totalRaceTime > 0 && _averageLapTime > 0 && _fuelPerLap > 0 )
            {
                TotalFeulNeeded.Content = (Math.DivRem(_totalRaceTime, _averageLapTime, out _) * _fuelPerLap).ToString("0.00");
            }
            else
            {
                TotalFeulNeeded.Content = 0.00;
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
            FuelPerLapSlider.ValueChanged -= FuelPerLapSlider_ValueChanged;
            if (TotalLaps.Content.ToString() != "" && FuelPerLap.Text != "")
            {
                String fuelPerLap = FuelPerLap.Text;
                if (fuelPerLap.Contains(','))
                {
                    fuelPerLap = fuelPerLap.Replace(',', '.');
                }

                if (Single.TryParse(fuelPerLap, NumberStyles.Number, CultureInfo.InvariantCulture ,out Single tempFuelPerLap))
                {
                    _fuelPerLap = tempFuelPerLap;
                    String totalLaps = TotalLaps.Content.ToString();
                    if (totalLaps.Contains(','))
                    {
                        totalLaps = totalLaps.Replace(',', '.');
                    }

                    Int32.TryParse(totalLaps, out Int32 totalLapsInt);
                    TotalFeulNeeded.Content = totalLapsInt * _fuelPerLap;
                    FuelPerLapSlider.Value = tempFuelPerLap;
                }
                else
                {
                    FuelPerLapSlider.Value = 0;
                    _fuelPerLap = tempFuelPerLap;
                    if (dateTime + new TimeSpan(0, 0, 1) > DateTime.Now)
                    {
                        dateTime = DateTime.Now;
                        String log = "Fuel per lap error. Parsing float failed. Input = " + FuelPerLap.Text + ". Output " + tempFuelPerLap + ";";
                        Logger.WriteToLog(log);
                    }
#if DEBUG
                    String log = "Fuel per lap error. Parsing float failed. Input = " + FuelPerLap.Text + ". Output " +
                                 tempFuelPerLap + ";";
                    Logger.WriteToLog(log);
#endif
                }
            }
            FuelPerLapSlider.ValueChanged += FuelPerLapSlider_ValueChanged;

            UpdateElements();
        }
        private void FuelPerLapSlider_ValueChanged(Object sender, RoutedPropertyChangedEventArgs<Double> e)
        {
            _fuelPerLap = (Single)e.NewValue;
            FuelPerLap.Text = e.NewValue.ToString("0.00");
            UpdateElements();
        }

        private void LogFuelPerLap(Object sender, RoutedEventArgs e)
        {
#if DEBUG
            Logger.WriteToLog("Fuel per lap text box: " + FuelPerLap.Text + ";");
            Logger.WriteToLog("Fuel per lap private: " + _fuelPerLap + ";");
#endif
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
                    CalculateRaceTime(carTrackCombo.TotalRaceTime, out Int32 hours, out Int32 minutes);
                    TotalRaceTimeHours.Text = hours.ToString();
                    TotalRaceTimeMinutes.Text = minutes.ToString();

                    CalculateAvarageLapTime(carTrackCombo.AverageLapTime, out Int32 minutesAv, out Int32 seconds);
                    AverageLapTimeMinutes.Text = minutesAv.ToString();
                    AverageLapTimeSeconds.Text = seconds.ToString();

                    FuelPerLap.Text = carTrackCombo.FuelPerLap.ToString();

                    _car = (Enums.Car) carTrackCombo.Car;
                    return;
                }

                TotalRaceTimeHours.Text = 0.ToString();
                TotalRaceTimeMinutes.Text = 0.ToString();
                AverageLapTimeMinutes.Text = 0.ToString();
                AverageLapTimeSeconds.Text = 0.ToString();
                FuelPerLap.Text = 0.ToString();
                return;
            }
            TotalRaceTimeHours.Text = 0.ToString();
            TotalRaceTimeMinutes.Text = 0.ToString();
            AverageLapTimeMinutes.Text = 0.ToString();
            AverageLapTimeSeconds.Text = 0.ToString();
            FuelPerLap.Text = 0.ToString();
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
                    CalculateRaceTime(carTrackCombo.TotalRaceTime, out Int32 hours, out Int32 minutes);
                    TotalRaceTimeHours.Text = hours.ToString();
                    TotalRaceTimeMinutes.Text = minutes.ToString();

                    CalculateAvarageLapTime(carTrackCombo.AverageLapTime, out Int32 minutesAv, out Int32 seconds);
                    AverageLapTimeMinutes.Text = minutesAv.ToString();
                    AverageLapTimeSeconds.Text = seconds.ToString();
                    FuelPerLap.Text = carTrackCombo.FuelPerLap.ToString();

                    _track = (Enums.Track)carTrackCombo.Track;
                    return;
                }
                TotalRaceTimeHours.Text = 0.ToString();
                TotalRaceTimeMinutes.Text = 0.ToString();
                AverageLapTimeMinutes.Text = 0.ToString();
                AverageLapTimeSeconds.Text = 0.ToString();
                FuelPerLap.Text = 0.ToString();
                return;
            }
            TotalRaceTimeHours.Text = 0.ToString();
            TotalRaceTimeMinutes.Text = 0.ToString();
            AverageLapTimeMinutes.Text = 0.ToString();
            AverageLapTimeSeconds.Text = 0.ToString();
            FuelPerLap.Text = 0.ToString();
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
