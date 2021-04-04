// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace Fuel_calculator.Model
{
    /// <remarks/>
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ListOfCars
    {
        [XmlIgnore] public static readonly String CarsFileLocation = Directory.GetCurrentDirectory() + "\\Xml\\Cars.xml";
        [XmlIgnore] public static readonly List<Car> Cars = LoadCarsFromXml().Car.ToList();

        private Car[] _carField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Car")]
        public Car[] Car
        {
            get => _carField;
            set => _carField = value;
        }




        public static ListOfCars LoadCarsFromXml()
        {
            return Xml_deserializer.Xml.Deserialize<ListOfCars>(CarsFileLocation);
        }

    }

    /// <remarks/>
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Car
    {
        private Int32 _idField;
        private String _nameField;
        private Int32 _maxFuelField;

        /// <remarks/>
        public Int32 Id
        {
            get => _idField;
            set => _idField = value;
        }

        /// <remarks/>
        public String Name
        {
            get => _nameField;
            set => _nameField = value;
        }

        /// <remarks/>
        public Int32 MaxFuel
        {
            get => _maxFuelField;
            set => _maxFuelField = value;
        }
    }
}