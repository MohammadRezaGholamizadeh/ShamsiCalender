using Microsoft.Extensions.Configuration;
using ShamsiCalender.Tools;
using ShamsiCalender.Tools.Calculations;
using ShamsiCalender.Tools.Configurations;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

while (true)
{
    Extensions.ConsoleWriteLine(
    "======================================================================================",
    ConsoleColor.Green);

    Extensions.ConsoleWriteLine(
    "Global Calender With Iran TimeZone [ Designed By MohammadReza Gholamizadeh - Phoenix ] ",
    ConsoleColor.Yellow);

    Extensions.ConsoleWriteLine(
    "======================================================================================" + Environment.NewLine,
    ConsoleColor.Green);

    Extensions.ConsoleWriteLine(
    "[1] Holiday Detection." + Environment.NewLine +
    "[2] Convert Hijri Shamsi To Georgian And Hijri Qamari." + Environment.NewLine,
    ConsoleColor.White);

    Extensions.ConsoleWriteLine(
    "======================================================================================" + Environment.NewLine,
    ConsoleColor.Green);

    Extensions.ConsoleWriteLine(
    "Your Choose : ",
    ConsoleColor.White);

    var input = Console.ReadKey();
    switch (input.KeyChar)
    {
        case '1':
            HolidayDetectionCalculation
                .GetHolidayDetectionCalculation(args);
            break;
        case '2':
            DateTimeConverterToForHijriShamsiCalender
                .UseConvertingCalculationOfHijriShamsiToGeorgianAndHijriQamari(args);
            break;
    }












}