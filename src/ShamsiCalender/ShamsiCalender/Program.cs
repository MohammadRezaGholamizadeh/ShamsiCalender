using Microsoft.Extensions.Configuration;
using ShamsiCalender.Tools;
using ShamsiCalender.Tools.Configurations;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

while (true)
{
    Console.WriteLine("Insert Your DateTime : " + Environment.NewLine);
    var holidays = new ConfigurationBuilder()
                       .AddJsonFile("holidays.json")
                       .AddEnvironmentVariables()
                       .AddCommandLine(args)
                       .Build();
    var hijriHolidays =
        Extensions.GetAllHolidaysForType<HijriHolidays>(args);

    var shamsiHolidays =
        Extensions.GetAllHolidaysForType<ShamsiHolidays>(args);

    var input = Console.ReadLine();
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

    if ((georgianDateTimeWithArabCulture - georgianDateTimeWithIranTimeZone)
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


    var shamsiCalender = new PersianCalendar();
    var shamsiYear = shamsiCalender.GetYear(georgianDateTimeWithIranTimeZone);
    var shamsiMonth = shamsiCalender.GetMonth(georgianDateTimeWithIranTimeZone);
    var shamsiDay = shamsiCalender.GetDayOfMonth(georgianDateTimeWithIranTimeZone);
    var shamsiMonthNewFormat = shamsiMonth < 10 ? $"0{shamsiMonth}" : $"{shamsiMonth}";
    var shamsiDayNewFormat = shamsiDay < 10 ? $"0{shamsiDay}" : $"{shamsiDay}";
    var shamsiFormat = $"{shamsiMonthNewFormat}/{shamsiDayNewFormat}";



    Console.WriteLine(hijriDateFormatWithMonthAndDay + Environment.NewLine);
    if (hijriHolidays.Holidays.Any(_ => _ == hijriDateFormatWithMonthAndDay) || shamsiHolidays.Holidays.Any(_ => _ == shamsiFormat))
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("This is a holiday !!!" + Environment.NewLine);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("This is a not holiday !!!" + Environment.NewLine);
    }
}