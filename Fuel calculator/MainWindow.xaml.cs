using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        private static String _settingsSaveFilePath;

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
