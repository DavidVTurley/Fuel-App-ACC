using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Fuel_calculator.Model
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ListOfTracks
    {
        [XmlIgnore] public static readonly String TracksFileLocation = Directory.GetCurrentDirectory() + "\\Xml\\Tracks.xml";
        [XmlIgnore] public static readonly List<Track> Tracks = LoadTracksFromXml().Track.ToList();



        private Track[] _trackField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Track")]
        public Track[] Track
        {
            get => _trackField;
            set => _trackField = value;
        }



        public static ListOfTracks LoadTracksFromXml()
        {
            return Xml_deserializer.Xml.Deserialize<ListOfTracks>(TracksFileLocation);
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Track
    {

        private Int32 _idField;
        private String _nameField;

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
    }


}