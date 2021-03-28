using System;
using System.Windows;
using System.Windows.Controls;
using Fuel_calculator.View;
using Fuel_calculator.Xml.Objects;

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
        }

        private void ManualMenuButton_OnClick(Object sender, RoutedEventArgs e)
        {
            SwitchView(new StandardView());
        }

        private void SwitchView(Control control)
        {
            Panel.Children.Clear();
            Panel.Children.Add(control);
        }
    }
}