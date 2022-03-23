using System;
using System.IO;

namespace ageofqueenscom.Code
{
    public static class Log
    {
        
        public static async void Write(Exception e)
        {
            DateTime Date = DateTime.Now;
            using StreamWriter writetext = new StreamWriter("log.txt", true);
            await writetext.WriteAsync(Date.TimeOfDay.ToString() + ": " + e.Message + e.StackTrace);
        }
    }
}
