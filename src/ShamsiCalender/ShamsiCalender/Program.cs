using Microsoft.Extensions.Configuration;
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
    var hijriHolidays = new HijriHolidays();
    holidays.Bind(nameof(HijriHolidays), hijriHolidays);

    var shamsiHolidays = new ShamsiHolidays();
    holidays.Bind(nameof(ShamsiHolidays), shamsiHolidays);

    CultureInfo arCult = new CultureInfo("ar-SA");
    TimeZoneInfo iranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");

    var input = Console.ReadLine();
    var inputDateTimeFormat = input!.Split("-");
    var desiredDateTime =
        new DateTime(
            int.Parse(inputDateTimeFormat[0]),
            int.Parse(inputDateTimeFormat[1]),
            int.Parse(inputDateTimeFormat[2]),
            0, 0, 0, 0);

    DateTime iranDateTime =
             TimeZoneInfo.ConvertTime(desiredDateTime, iranTimeZone);

    HijriCalendar hijriCalendar = new HijriCalendar();
    int hijriYear1 = hijriCalendar.GetYear(iranDateTime);
    int hijriMonth1 = hijriCalendar.GetMonth(iranDateTime);
    int hijriDay1 = hijriCalendar.GetDayOfMonth(iranDateTime);

    var hijriDateFormat = $"{hijriYear1}/{(hijriMonth1 < 10 ? $"0{hijriMonth1}" : hijriMonth1)}/{(hijriDay1 < 10 ? $"0{hijriDay1}" : hijriDay1)}";
    var test2 = DateTime.ParseExact(hijriDateFormat, "yyyy/MM/dd", arCult);
    if ((test2 - iranDateTime).TotalDays > 0)
    {
        hijriDay1 = hijriDay1 - 2;
    }
    else
    {
        hijriDay1 = hijriDay1 - 1;
    }
    var hijriDateFormat2 = $"{(hijriMonth1 < 10 ? $"0{hijriMonth1}" : hijriMonth1)}/{(hijriDay1 < 10 ? $"0{hijriDay1}" : hijriDay1)}";


    var shamsiCalender = new PersianCalendar();
    var shamsiYear = shamsiCalender.GetYear(iranDateTime);
    var shamsiMonth = shamsiCalender.GetMonth(iranDateTime);
    var shamsiDay = shamsiCalender.GetDayOfMonth(iranDateTime);
    var shamsiMonthNewFormat = shamsiMonth < 10 ? $"0{shamsiMonth}" : $"{shamsiMonth}";
    var shamsiDayNewFormat = shamsiDay < 10 ? $"0{shamsiDay}" : $"{shamsiDay}";
    var shamsiFormat = $"{shamsiMonthNewFormat}/{shamsiDayNewFormat}";



    Console.WriteLine(hijriDateFormat2 + Environment.NewLine);
    if (hijriHolidays.Holidays.Any(_ => _ == hijriDateFormat2) || shamsiHolidays.Holidays.Any(_ => _ == shamsiFormat))
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