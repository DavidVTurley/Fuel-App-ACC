using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fuel_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (KeyValuePair<CarInfo.Car, (String, Int32)> carFuelAmount in CarInfo.CarFuelAmounts.Where(carFuelAmount => carFuelAmount.Key != CarInfo.Car.Empty))
            {
                CarSelector.Items.Add(carFuelAmount.Value.Item1);
            }

            CarSelector.SelectedIndex = 0;
            FuelTankCapacity.Text = CarInfo.GetCarFeulFromCarEnum(CarInfo.GetCarFromCarName(CarSelector.Text)).ToString(CultureInfo.InvariantCulture);
        }



        /// <summary>
        ///  CarInfo make and fuel load
        /// </summary>
        private Int32 _totalRaceTime;
        private Int32 _averageLapTime;
        private Decimal _fuelPerLap;
        private Int32 _fuelTankCapacity;


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
            if (_fuelTankCapacity > 0 && _fuelPerLap > 0 && _totalRaceTime > 0)
            {
                TotalFeulNeeded.Content = (_totalRaceTime / _averageLapTime) * _fuelPerLap;
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
            if (TotalLaps.Content.ToString() != "" && FeulPerLap.Text != "")
            {
                if (Single.TryParse(FeulPerLap.Text, out Single fuelPerLap))
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

        
    }
}
