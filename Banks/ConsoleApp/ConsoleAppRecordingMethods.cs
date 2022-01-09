using System;

namespace Banks
{
    public class ConsoleAppRecordingMethods
    {
        public string ConsoleToString(string temp)
        {
            Console.WriteLine(temp);
            return Convert.ToString(Console.ReadLine());
        }

        public int ConsoleToInt(string temp)
        {
            Console.WriteLine(temp);
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}