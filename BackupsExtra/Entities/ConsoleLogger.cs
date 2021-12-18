using System;
using System.Globalization;
using BackupsExtra.Interfaces;

namespace BackupsExtra.Entities
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string text, bool timeCodePrefix = false)
        {
            if (timeCodePrefix)
                text += ':' + DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Console.WriteLine(text);
        }
    }
}