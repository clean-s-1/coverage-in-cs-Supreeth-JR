using System;

namespace TypewiseAlert.Common
{
    public static class Logger 
    {
        public static void LogMessage(string msgToLogged)
        {
            Console.WriteLine($"{msgToLogged}");
        }
    }
}
