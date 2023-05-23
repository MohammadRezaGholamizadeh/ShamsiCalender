using Microsoft.Extensions.Configuration;
using System.Globalization;


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

    var input = Console.ReadLine();
    var inputDateTimeFormat = input!.Split("-");

    var georgianDateTime =
        new DateTime(
            int.Parse(inputDateTimeFormat[0]),
            int.Parse(inputDateTimeFormat[1]),
            int.Parse(inputDateTimeFormat[2]),
            0,
                    0,
                    0,
                    0);

    var persianCalender = new PersianCalendar();
    var georgianCal =
        persianCalender
        .ToDateTime(int.Parse(inputDateTimeFormat[0]),
                    int.Parse(inputDateTimeFormat[1]),
                    int.Parse(inputDateTimeFormat[2]),
                    0,
                    0,
                    0,
                    0);

    var hijriCalender = new HijriCalendar();
    var hijriYear = hijriCalender.GetYear(georgianDateTime);
    var hijriMonth = hijriCalender.GetMonth(georgianDateTime);
    var hijriDay = hijriCalender.GetDayOfMonth(georgianDateTime) - 1;
    var hijriMonthNewFormat = hijriMonth < 10 ? $"0{hijriMonth}" : $"{hijriMonth}";
    var hijriDayNewFormat = hijriDay < 10 ? $"0{hijriDay}" : $"{hijriDay}";
    var hijriFormat = $"{hijriMonthNewFormat}/{hijriDayNewFormat}";
    if (hijriHolidays.Holidays.Any(_ => _ == hijriFormat))
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("This is a holiday !!!" + Environment.NewLine);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("This is a not holiday !!!" + Environment.NewLine);
    }
    var hijriDateTimeFormat = new DateTime(hijriYear, hijriMonth, hijriDay);
    Console.ResetColor();
}


public class HijriHolidays
{
    public List<string> Holidays { get; set; }
}