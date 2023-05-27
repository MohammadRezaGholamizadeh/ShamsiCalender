using Microsoft.Extensions.Configuration;
using ShamsiCalender.Tools.Configurations;
using System.Globalization;

namespace ShamsiCalender.Tools.Calculations
{
    public static class HolidayDetectionCalculation
    {
        public static void GetHolidayDetectionCalculation(string[] args)
        {
            while (true)
            {
                Extensions.ConsoleWrite(
                           Environment.NewLine + "Insert Your DateTime : ",
                           ConsoleColor.White);
                Extensions.ConsoleWriteLine(
                           "[Or Type 0 To Exit]",
                           ConsoleColor.DarkYellow);

                Extensions.SetForegroundConsoleColor();
                var input = Console.ReadLine();

                if (input!.Trim().ToLower() == "0")
                {
                    Console.Clear();
                    break;
                }
                else if (input == null || input == "" || input.Split("-").Count() != 3)
                {
                    Extensions.ConsoleWriteLine(
                               Environment.NewLine + " * Invalid DateTime !!!",
                               ConsoleColor.Red);
                    continue;
                }
                var hijriHolidays =
                    Extensions.GetAllHolidaysForType<HijriHolidays>(args);

                var shamsiHolidays =
                    Extensions.GetAllHolidaysForType<ShamsiHolidays>(args);

                var desiredDateTime = input!.GetDateTimeFormat();

                DateTime georgianDateTimeWithIranTimeZone =
                         Extensions.ConvertTimeWithTimeZone(desiredDateTime, "Iran Standard Time");

                HijriCalendar hijriCalendar = new HijriCalendar();
                int hijriYear = hijriCalendar.GetYear(georgianDateTimeWithIranTimeZone);
                int hijriMonth = hijriCalendar.GetMonth(georgianDateTimeWithIranTimeZone);
                int hijriDay = hijriCalendar.GetDayOfMonth(georgianDateTimeWithIranTimeZone);

                var hijriDateFormat =
                    Extensions.GetDateTimeStringFormat(
                               hijriYear,
                               hijriMonth,
                               hijriDay);

                var georgianDateTimeWithArabCulture =
                    Extensions.ParseExactToDateTime(hijriDateFormat, "ar-SA");


                var shamsiCalender = new PersianCalendar();
                if (shamsiCalender.IsLeapYear(shamsiCalender.GetYear(georgianDateTimeWithIranTimeZone))
                    && DateTime.IsLeapYear(georgianDateTimeWithIranTimeZone.Year))
                {
                    hijriDay = hijriDay - 1;
                }
                else if ((georgianDateTimeWithArabCulture - georgianDateTimeWithIranTimeZone)
                          .TotalDays > 0)
                {
                    hijriDay = hijriDay - 2;
                }
                else
                {
                    hijriDay = hijriDay - 1;
                }

                var hijriDateFormatWithMonthAndDay =
                    $"{(hijriMonth < 10 ? $"0{hijriMonth}" : hijriMonth)}/{(hijriDay < 10 ? $"0{hijriDay}" : hijriDay)}";

                var shamsiYear = shamsiCalender.GetYear(georgianDateTimeWithIranTimeZone);
                var shamsiMonth = shamsiCalender.GetMonth(georgianDateTimeWithIranTimeZone);
                var shamsiDay = shamsiCalender.GetDayOfMonth(georgianDateTimeWithIranTimeZone);
                var shamsiMonthNewFormat = shamsiMonth < 10 ? $"0{shamsiMonth}" : $"{shamsiMonth}";
                var shamsiDayNewFormat = shamsiDay < 10 ? $"0{shamsiDay}" : $"{shamsiDay}";
                var shamsiFormatWithMonthAndDay = $"{shamsiMonthNewFormat}/{shamsiDayNewFormat}";

                Extensions.ConsoleWriteLine(
                    Environment.NewLine +
                    $"HijriQamariDate => [{hijriYear}/{hijriDateFormatWithMonthAndDay}]\n" +
                    $"HijriShamsiDate => [{shamsiYear}/{shamsiFormatWithMonthAndDay}]" +
                    Environment.NewLine, ConsoleColor.Yellow);

                if (hijriHolidays.Holidays.Any(_ => _ == hijriDateFormatWithMonthAndDay) || shamsiHolidays.Holidays.Any(_ => _ == shamsiFormatWithMonthAndDay))
                {
                    Extensions.ConsoleWrite(
                    "Holiday Status => ",
                    ConsoleColor.White);
                    Extensions.ConsoleWriteLine(
                    "This Is Holiday !!!" + Environment.NewLine,
                    ConsoleColor.Green);
                }
                else
                {
                    Extensions.ConsoleWrite(
                    "Holiday Status => ",
                    ConsoleColor.White);
                    Extensions.ConsoleWriteLine(
                    "This is Not Holiday !!!" + Environment.NewLine,
                    ConsoleColor.Red);
                }

            }
        }
    }

    public static class DateTimeConverterToForHijriShamsiCalender
    {
        public static void UseConvertingCalculationOfHijriShamsiToGeorgianAndHijriQamari(string[] args)
        {
            while (true)
            {
                Extensions.ConsoleWrite(
                           Environment.NewLine + "Insert Your DateTime : ",
                           ConsoleColor.White);
                Extensions.ConsoleWriteLine(
                           "[Or Type 0 To Exit]",
                           ConsoleColor.DarkYellow);

                Extensions.SetForegroundConsoleColor();
                var input = Console.ReadLine();

                if (input!.Trim().ToLower() == "0")
                {
                    Console.Clear();
                    break;
                }
                else if (input == null || input == "" || input.Split("-").Count() != 3)
                {
                    Extensions.ConsoleWriteLine(
                               Environment.NewLine + " * Invalid DateTime !!!",
                               ConsoleColor.Red);
                    continue;
                }

                var shamsiCalender = new PersianCalendar();
                var desiredDateTime = input!.GetDateTimeFormat();

                DateTime georgianDateTimeWithIranTimeZone =
                         shamsiCalender.ToDateTime(
                             desiredDateTime.Year,
                             desiredDateTime.Month,
                             desiredDateTime.Day,
                             0, 0, 0, 0);

                HijriCalendar hijriCalendar = new HijriCalendar();
                int hijriYear = hijriCalendar.GetYear(georgianDateTimeWithIranTimeZone);
                int hijriMonth = hijriCalendar.GetMonth(georgianDateTimeWithIranTimeZone);
                int hijriDay = hijriCalendar.GetDayOfMonth(georgianDateTimeWithIranTimeZone);

                var hijriDateFormat =
                    Extensions.GetDateTimeStringFormat(
                               hijriYear,
                               hijriMonth,
                               hijriDay);

                var georgianDateTimeWithArabCulture =
                    Extensions.ParseExactToDateTime(hijriDateFormat, "ar-SA");
                var shamsiYear = shamsiCalender.GetYear(georgianDateTimeWithIranTimeZone);
                var georgainYear = georgianDateTimeWithIranTimeZone.Year;

                if ((georgianDateTimeWithArabCulture - georgianDateTimeWithIranTimeZone)
                           .TotalDays < 0)
                {
                    hijriDay = hijriDay - 0;
                }
                else if (DateTime.IsLeapYear(georgianDateTimeWithIranTimeZone.Year)
                         && shamsiCalender.IsLeapYear(shamsiCalender.GetYear(georgianDateTimeWithIranTimeZone))
                         && hijriCalendar.IsLeapYear(hijriYear))
                {
                    hijriDay = hijriDay - 2;
                }
                else if (DateTime.IsLeapYear(georgianDateTimeWithIranTimeZone.Year)
                    && (shamsiCalender.IsLeapYear(shamsiCalender.GetYear(georgianDateTimeWithIranTimeZone))
                    || hijriCalendar.IsLeapYear(hijriYear)))
                {
                    hijriDay = hijriDay - 1;
                }
                else if ((georgianDateTimeWithArabCulture - georgianDateTimeWithIranTimeZone)
                          .TotalDays > 0)
                {
                    hijriDay = hijriDay - 2;
                }
                else
                {
                    hijriDay = hijriDay - 1;
                }

                var hijriDateFormatWithMonthAndDay =
                $"{(hijriMonth < 10 ? $"0{hijriMonth}" : hijriMonth)}/{(hijriDay < 10 ? $"0{hijriDay}" : hijriDay)}";

                var hijriShamsiDateFormatWithMonthAndDay =
                    $"{shamsiCalender.GetYear(georgianDateTimeWithIranTimeZone)}/{(shamsiCalender.GetMonth(georgianDateTimeWithIranTimeZone) < 10 ? $"0{shamsiCalender.GetMonth(georgianDateTimeWithIranTimeZone)}" : shamsiCalender.GetMonth(georgianDateTimeWithIranTimeZone))}/{(shamsiCalender.GetDayOfMonth(georgianDateTimeWithIranTimeZone) < 10 ? $"0{shamsiCalender.GetDayOfMonth(georgianDateTimeWithIranTimeZone)}" : shamsiCalender.GetDayOfMonth(georgianDateTimeWithIranTimeZone))}";
                Extensions.ConsoleWriteLine(
                    Environment.NewLine +
                    $"HijriShamsiDate => [{hijriShamsiDateFormatWithMonthAndDay}] | Is Leap Year : {shamsiCalender.IsLeapYear(shamsiCalender.GetYear(georgianDateTimeWithIranTimeZone))}\n" +
                    $"HijriQamariDate => [{hijriYear}/{hijriDateFormatWithMonthAndDay}] | Is Leap Year : {hijriCalendar.IsLeapYear(hijriYear)}\n" +
                    $"GeorgianDate    => [{georgianDateTimeWithIranTimeZone.Year}/" +
                    $"{(georgianDateTimeWithIranTimeZone.Month < 10 ? $"0{georgianDateTimeWithIranTimeZone.Month}" : georgianDateTimeWithIranTimeZone.Month)}/" +
                    $"{(georgianDateTimeWithIranTimeZone.Day < 10 ? $"0{georgianDateTimeWithIranTimeZone.Day}" : georgianDateTimeWithIranTimeZone.Day)}] " +
                    $"| Is Leap Year : {DateTime.IsLeapYear(georgianDateTimeWithIranTimeZone.Year)}" +
                    Environment.NewLine, ConsoleColor.Yellow);
            }
        }
    }
}
