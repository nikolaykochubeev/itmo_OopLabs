using System;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            DateTime dateTime = DateTime.Now;
            DateTime dateTime2 = DateTime.Now;
            Console.WriteLine(dateTime < dateTime2);
        }
    }
}
