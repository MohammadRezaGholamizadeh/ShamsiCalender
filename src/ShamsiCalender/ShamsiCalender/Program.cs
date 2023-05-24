using Microsoft.Extensions.Configuration;
using ShamsiCalender.Tools.Configurations;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

while (true)
{
    Console.WriteLine("insert new input : " + Environment.NewLine);
    var holidays = new ConfigurationBuilder()
                       .AddJsonFile("holidays.json")
                       .AddEnvironmentVariables()
                       .AddCommandLine(args)
                       .Build();
    var hijriHolidays = new HijriHolidays();
    holidays.Bind(nameof(HijriHolidays), hijriHolidays);


    HijriCalendar hijriCalendar = new HijriCalendar();

    // Set the time zone to Iran Standard Time (GMT+3:30)
    CultureInfo arCult = new CultureInfo("ar-SA");
    TimeZoneInfo iranTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
    var test = DateTime.ParseExact("1444/11/06", "yyyy/MM/dd", arCult);
    // Specify the desired date and time
    DateTime desiredDateTime = new DateTime(2023, 5, 25, 14, 30, 0); // Example: May 25, 2023 14:30

    // Convert the desired date and time to Iran Standard Time
    DateTime iranDateTime = TimeZoneInfo.ConvertTime(desiredDateTime, iranTimeZone);

    // Get the Hijri date
    int hijriYear = hijriCalendar.GetYear(iranDateTime);
    int hijriMonth = hijriCalendar.GetMonth(iranDateTime);
    int hijriDay = hijriCalendar.GetDayOfMonth(iranDateTime);
    var test2 = DateTime.ParseExact($"{hijriYear}/{hijriMonth}/{(hijriDay < 10 ? $"0{hijriDay}" : hijriDay)}", "yyyy/MM/dd", arCult);
    // Display the Hijri date
    Console.WriteLine($"Hijri Date (Iran): {hijriYear}/{hijriMonth}/{hijriDay}");

    //var shamsiHolidays = new ShamsiHolidays();
    //holidays.Bind(nameof(ShamsiHolidays), shamsiHolidays);


    //var input = Console.ReadLine();
    //var inputDateTimeFormat = input!.Split("-");

    //var georgianDateTime =
    //    new DateTime(
    //        int.Parse(inputDateTimeFormat[0]),
    //        int.Parse(inputDateTimeFormat[1]),
    //        int.Parse(inputDateTimeFormat[2]),
    //        0, 0, 0, 0);

    //var persianCalender = new PersianCalendar();

    //var shamsiCalender = new PersianCalendar();
    //var shamsiYear = shamsiCalender.GetYear(georgianDateTime);
    //var shamsiMonth = shamsiCalender.GetMonth(georgianDateTime);
    //var shamsiDay = shamsiCalender.GetDayOfMonth(georgianDateTime);
    //var shamsiMonthNewFormat = shamsiMonth < 10 ? $"0{shamsiMonth}" : $"{shamsiMonth}";
    //var shamsiDayNewFormat = shamsiDay < 10 ? $"0{shamsiDay}" : $"{shamsiDay}";
    //var shamsiFormat = $"{shamsiMonthNewFormat}/{shamsiDayNewFormat}";
    ////CultureInfo arCult = new CultureInfo("ar-SA");
    ////DateTime gregorianDate = DateTime.ParseExact("1444/02/02", "yyyy/MM/dd", arCult);
    //var hijriCalender = new HijriCalendar();
    //var hijriYear = hijriCalender.GetYear(georgianDateTime);
    //var hijriMonth = hijriCalender.GetMonth(georgianDateTime);
    //var hijriKabiseList = new int[] { 2, 5, 7, 10, 13, 16, 18, 21, 24, 26, 29 };
    //var ff = hijriCalender.GetDayOfMonth(gregorianDate);
    //int hijriDay;
    //if (hijriKabiseList.Contains(hijriYear % 30) && hijriMonth > 2)
    //{
    //    hijriDay = hijriCalender.GetDayOfMonth(georgianDateTime) - 2;
    //}
    //else
    //{
    //    hijriDay = hijriCalender.GetDayOfMonth(georgianDateTime) - 1;
    //}
    //var hijriMonthNewFormat = hijriMonth < 10 ? $"0{hijriMonth}" : $"{hijriMonth}";
    //var hijriDayNewFormat = hijriDay < 10 ? $"0{hijriDay}" : $"{hijriDay}";
    //var hijriFormat = $"{hijriMonthNewFormat}/{hijriDayNewFormat}";


    //Console.WriteLine(hijriFormat + Environment.NewLine);
    //if (hijriHolidays.Holidays.Any(_ => _ == hijriFormat) || shamsiHolidays.Holidays.Any(_ => _ == shamsiFormat))
    //{
    //    Console.ForegroundColor = ConsoleColor.Green;
    //    Console.WriteLine("This is a holiday !!!" + Environment.NewLine);
    //}
    //else
    //{
    //    Console.ForegroundColor = ConsoleColor.Red;
    //    Console.WriteLine("This is a not holiday !!!" + Environment.NewLine);
    //}
    //var hijriDateTimeFormat = new DateTime(hijriYear, hijriMonth, hijriDay);
    //Console.ResetColor();
}