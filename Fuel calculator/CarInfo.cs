using System;
using System.Collections.Generic;
using System.Linq;

namespace Feul_calculator
{
    public static class CarInfo
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
            McLarren_729S_GT3,
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
            { Car.AMR_V12_Vantage_GT3, ("AMR V12 Vantage GT3", 1) },
           { Car.AMR_V8_Vantage_GT3, ("AMR V8 Vantage GT3", 2) },
           { Car.Audi_R8_LMS, ("Audi R8 LMS", 3) },
           { Car.Audi_R8_LMS_Evo, ("Audi R8 LMS Evo", 4) },
           { Car.Bentley_Continental_GT3_2015, ("Bentley Continental GT3 2015", 5) },
           { Car.Bentley_Continental_GT3_2018, ("Bentley Continental GT3 2018", 6) },
           { Car.BMW_M6_GT3, ("BMW M6 GT3", 100) },
           { Car.Emil_Frey_Jaguar_G3, ("Emil Frey Jaguar G3", 100) },
           { Car.Ferrari_488_GT3, ("Ferrari 488 GT3", 100) },
           { Car.Honda_NSX_GT3, ("Honda NSX GT3", 100) },
           { Car.Honda_NSX_GT3_Evo, ("Honda NSX GT3 Evo", 100) },
           { Car.Lamborghini_Hurcan_GT3, ("Lamborghini Hurcan GT3", 100) },
           { Car.Lamborghini_Hurcan_GT3_Evo, ("Lamborghini Huracan GT3 Evo", 100) },
           { Car.Lamborghini_Hurcan_ST, ("Lamborghini Huracan ST", 100) },
           { Car.Lexus_RC_GT3, ("Lexus RC GT3", 100) },
           { Car.McLarren_650S_GT3, ("McLarren 650S GT3", 100) },
           { Car.McLarren_729S_GT3, ("McLarren 729S GT3", 100) },
           { Car.Mercedes_AMG_GT3, ("Mercedes AMG GT3", 100) },
           { Car.Nissan_GT_R_Nismo_GT3_2015, ("Nissan GT-R Nismo GT3 2015", 100) },
           { Car.Nissan_GT_R_Nismo_GT3_2018, ("Nissan GT-R Nismo GT3 2018", 100) },
           { Car.Porsche_991_GT3_R, ("Porsche 991 GT3-R", 100) },
           { Car.Porsche_991_GT3_Cup, ("Porsche 991 GT3 Cup", 100) },
           { Car.Reiter_Engineering_R_EX_GT3, ("Reiter Engineering R-EX GT3", 100) },
           // ReSharper restore StringLiteralTypo
        };

        public static Car GetCarFromCarName(String carName)
        {
            foreach (KeyValuePair<Car, (String, Int32)> carFuelAmount in CarFuelAmounts.Where(carFuelAmount => carFuelAmount.Value.Item1 == carName))
            {
                return carFuelAmount.Key;
            }

            return Car.Empty;
        }
        public static Int32 GetCarFeulFromCarEnum(Car car)
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
    }
}