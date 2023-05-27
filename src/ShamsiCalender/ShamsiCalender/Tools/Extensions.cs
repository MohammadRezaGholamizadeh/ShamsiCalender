using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace ShamsiCalender.Tools
{
    public static class Extensions
    {
        public static T GetAllHolidaysForType<T>(string[] args) where T : class
        {
            var holidays = new ConfigurationBuilder()
                               .AddJsonFile("holidays.json")
                               .AddEnvironmentVariables()
                               .AddCommandLine(args)
                               .Build();

            var instance = Activator.CreateInstance(typeof(T));
            holidays.Bind(typeof(T).Name, instance);
            return (instance as T)!;
        }

        public static DateTime GetDateTimeFormat(this string input)
        {
            var inputDateTimeFormat = input!.Split("-");
            var desiredDateTime =
                new DateTime(
                    int.Parse(inputDateTimeFormat[0]),
                    int.Parse(inputDateTimeFormat[1]),
                    int.Parse(inputDateTimeFormat[2]),
                    0, 0, 0, 0);
            return desiredDateTime;
        }

        public static DateTime ConvertTimeWithTimeZone(
            DateTime datetime,
            string timeZoneId)
        {
            TimeZoneInfo timeZoneInfo =
                TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTime(datetime, timeZoneInfo);
        }

        public static string GetDateTimeStringFormat(int year, int month, int day)
        {
            return $"{year}/{(month < 10 ? $"0{month}" : month)}/{(day < 10 ? $"0{day}" : day)}";
        }

        public static DateTime ParseExactToDateTime(string dateTimeStringFormat, string culture)
        {
            CultureInfo cultureInfo = new CultureInfo(culture);
            return DateTime.ParseExact(dateTimeStringFormat, "yyyy/MM/dd", cultureInfo);
        }

        public static void ConsoleWriteLine(
            string message,
            ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.ResetColor();
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void ConsoleWrite(
             string message,
             ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.ResetColor();
            Console.ForegroundColor = consoleColor;
            Console.Write(message);
            Console.ResetColor();
        }
        public static void SetForegroundConsoleColor(
           ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.ForegroundColor = consoleColor;
        }
        public static void SetBackgroundConsoleColor(
           ConsoleColor consoleColor = ConsoleColor.White)
        {
            Console.BackgroundColor = consoleColor;
        }
    }
}
