using System;
using System.IO;
using System.Xml;

namespace Fuel_calculator
{
    public static class Logger
    {
        private static String _logFileLocation = Directory.GetCurrentDirectory() + "\\Debug.txt";

        static Logger()
        {
            WriteToLog("------------------------------------------------------------------------------");
            WriteToLog("Log file initiated;");
        }

        public static void WriteToLog(String logText)
        {
            using(StreamWriter writer = File.AppendText(_logFileLocation))
            {
                writer.WriteLine(logText);
            }
        }

    }
}