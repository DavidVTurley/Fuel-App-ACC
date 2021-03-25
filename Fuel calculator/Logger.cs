using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Fuel_calculator
{
#if DEBUG
    public static class Logger
    {
        private static readonly String LogFileLocation = Directory.GetCurrentDirectory() + "\\Debug.txt";

        static Logger()
        {
            WriteToLog("------------------------------------------------------------------------------");
            WriteToLog("Log file initiated;");
            WriteToLog("Current language info: " + CultureInfo.CurrentCulture.ToString());
        }

        public static void WriteToLog(String logText)
        {
            using(StreamWriter writer = File.AppendText(LogFileLocation))
            {
                writer.WriteLine(logText);
            }
        }
    }
#endif
}