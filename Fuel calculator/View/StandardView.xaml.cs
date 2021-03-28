using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using Fuel_calculator.Xml.Objects;

namespace Fuel_calculator.View
{
    /// <summary>
    /// Interaction logic for StandardView.xaml
    /// </summary>
    public partial class StandardView : UserControl
    {
        public Cars Cars = Cars.LoadCarsFromXml();
        public Tracks Tracks = Tracks.LoadTracksFromXml();
        public SavedSetups SavedSetups = SavedSetups.LoadCarTrackComboFromXml();


        public StandardView()
        {
            InitializeComponent();

            DataContext = this;
        }
    }
}