using System;
using System.Collections.Generic;
using System.Linq;

namespace Fuel_calculator
{
    public static class Enums
    {
        public enum Car
        {
            // ReSharper disable InconsistentNaming
            AMR_V12_Vantage_GT3,
            AMR_V8_Vantage_GT3,
            Audi_R8_LMS,
            Audi_R8_LMS_Evo,
            Bentley_Continental_GT3_2015,
            Bentley_Continental_GT3_2018,
            BMW_M6_GT3,
            Emil_Frey_Jaguar_G3,
            Ferrari_488_GT3,
            Honda_NSX_GT3,
            Honda_NSX_GT3_Evo,
            Lamborghini_Hurcan_GT3,
            Lamborghini_Hurcan_GT3_Evo,
            Lamborghini_Hurcan_ST,
            Lexus_RC_GT3,
            McLarren_650S_GT3,
            McLarren_720S_GT3,
            Mercedes_AMG_GT3,
            Nissan_GT_R_Nismo_GT3_2015,
            Nissan_GT_R_Nismo_GT3_2018,
            Porsche_991_GT3_R,
            Porsche_991_GT3_Cup,
            Reiter_Engineering_R_EX_GT3,

            Empty
            // ReSharper restore InconsistentNaming
        }
        public static Dictionary<Car, (String, Int32)> CarFuelAmounts = new Dictionary<Car, (String carName, Int32 feul)>
            {
                // ReSharper disable StringLiteralTypo
                {Car.AMR_V12_Vantage_GT3, ("AMR V12 Vantage GT3", 132)},
                {Car.AMR_V8_Vantage_GT3, ("AMR V8 Vantage GT3", 120)},
                {Car.Audi_R8_LMS, ("Audi R8 LMS", 120)},
                {Car.Audi_R8_LMS_Evo, ("Audi R8 LMS Evo", 120)},
                {Car.Bentley_Continental_GT3_2015, ("Bentley Continental GT3 2015", 132)},
                {Car.Bentley_Continental_GT3_2018, ("Bentley Continental GT3 2018", 132)},
                {Car.BMW_M6_GT3, ("BMW M6 GT3", 125)},
                {Car.Emil_Frey_Jaguar_G3, ("Emil Frey Jaguar G3", 119)},
                {Car.Ferrari_488_GT3, ("Ferrari 488 GT3", 110)},
                {Car.Honda_NSX_GT3, ("Honda NSX GT3", 120)},
                {Car.Honda_NSX_GT3_Evo, ("Honda NSX GT3 Evo", 120)},
                {Car.Lamborghini_Hurcan_GT3, ("Lamborghini Hurcan GT3", 120)},
                {Car.Lamborghini_Hurcan_GT3_Evo, ("Lamborghini Huracan GT3 Evo", 120)},
                {Car.Lamborghini_Hurcan_ST, ("Lamborghini Huracan ST", 120)},
                {Car.Lexus_RC_GT3, ("Lexus RC GT3", 120)},
                {Car.McLarren_650S_GT3, ("McLarren 650S GT3", 125)},
                {Car.McLarren_720S_GT3, ("McLarren 720S GT3", 125)},
                {Car.Mercedes_AMG_GT3, ("Mercedes AMG GT3", 120)},
                {Car.Nissan_GT_R_Nismo_GT3_2015, ("Nissan GT-R Nismo GT3 2015", 132)},
                {Car.Nissan_GT_R_Nismo_GT3_2018, ("Nissan GT-R Nismo GT3 2018", 132)},
                {Car.Porsche_991_GT3_R, ("Porsche 991 GT3-R", 120)}, //There is another version, but for fuel it is the same
                {Car.Porsche_991_GT3_Cup, ("Porsche 991 GT3 Cup", 100)},
                {Car.Reiter_Engineering_R_EX_GT3, ("Reiter Engineering R-EX GT3", 130)},
                // ReSharper restore StringLiteralTypo
            };

        public static Car GetCarFromCarName(String carName)
        {
            foreach (KeyValuePair<Car, (String, Int32)> carFuelAmount in CarFuelAmounts.Where(carFuelAmount =>
                carFuelAmount.Value.Item1 == carName))
            {
                return carFuelAmount.Key;
            }

            return Car.Empty;
        }
        public static Int32 GetCarFuelFromCarEnum(Car car)
        {
            if (car == Car.Empty) return 0;
            return CarFuelAmounts[car].Item2;
        }
        public static Int32 GetCarFuelFromCarName(String car)
        {
            foreach ((String carName, Int32 fuel) in CarFuelAmounts.Values)
            {
                if (String.Equals(car, carName)) return fuel;
            }

            return 0;
        }

        public enum Track
        {
            // ReSharper disable StringLiteralTypo
            // ReSharper disable IdentifierTypo
            // ReSharper disable InconsistentNaming
            Monza,
            Brands_Hatch,
            Silverstone,
            Paul_Ricard,
            Misano,
            Zandvoort,
            Spa_Francorchamps,
            Nürburgring,
            Hungaroring,
            Barcelona,
            Zolder,
            Mount_Panorama,
            Laguna_Seca,
            Suzuka,
            Kyalami,

            Empty,
        }
        public static Dictionary<Track, String> TrackNames = new Dictionary<Track, String>
        {
            {Track.Monza, "Monza"},
            {Track.Brands_Hatch, "Brands Hatch"},
            {Track.Silverstone, "Silverstone"},
            {Track.Paul_Ricard, "Paul Ricard"},
            {Track.Misano, "Misano"},
            {Track.Zandvoort, "Zandvoort"},
            {Track.Spa_Francorchamps, "Spa Francorchamps"},
            {Track.Nürburgring, "Nürburgring"},
            {Track.Hungaroring, "Hungaroring"},
            {Track.Barcalona, "Barcalona"},
            {Track.Zolder, "Zolder"},
            {Track.Mount_Panorama, "Mount Panorama"},
            {Track.Laguna_Seca, "Laguna Seca"},
            {Track.Suzuka, "Suzuka"},
            {Track.Kyalami, "Kyalami"}
            // ReSharper restore IdentifierTypo
            // // ReSharper restore InconsistentNaming
            // ReSharper restore StringLiteralTypo
        };

        public static String GetTrackName(Track track)
        {
            return TrackNames[track];
        }
        public static Track GetTrackEnum(String trackName)
        {
            foreach (KeyValuePair<Track, String> value in TrackNames)
            {
                if (trackName == value.Value)
                {
                    Track track = value.Key;
                    return track;
                }
            }

            return Track.Monza;
        }
    }
}