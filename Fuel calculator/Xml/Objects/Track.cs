using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fuel_calculator.Xml.Objects
{
    [XmlRoot("Track")]
    public class Track
    {
        [XmlElement("Name")] public String Name;
    }

    public class Tracks
    {
        [XmlElement("Track")] public List<Track> Track;


        public static Tracks LoadTracksFromXml()
        {
            return Xml_deserializer.Xml.Deserialize<Tracks>(Directory.GetCurrentDirectory() + "\\Xml\\Tracks.xml");
        }
    }
}
