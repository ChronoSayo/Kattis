using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        const string errorYears = "Invalid number of years. Please enter a positive integer less than or equal to $.";
        const string errorCities = "Invalid number of cities. Please enter a positive integer less than or equal to $.";
        const string errorCityName = "Invalid city name. Please enter a name with only lowercase letters, no spaces, no digits, and a maximum of $ characters.";
        const int maxYears = 50;
        const int maxCities = 100;
        const int maxCityNameLength = 20;

        if (!GetValidIntInput(out int years, 1, maxYears, errorYears.Replace("$", maxYears.ToString())))
            return;

        for (int i = 0; i < years; i++)
        {
            if (!GetValidIntInput(out int cities, 1, maxCities, errorCities.Replace("$", maxCities.ToString())))
                continue;

            var uniqueCities = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (int j = 0; j < cities; j++)
            {
                string city;
                while (!IsValidCityName(city = Console.ReadLine(), maxCityNameLength))
                    Console.WriteLine(errorCityName.Replace("$", maxCityNameLength.ToString()));

                uniqueCities.Add(city);
            }

            Console.WriteLine(uniqueCities.Count);
        }
    }

    static bool GetValidIntInput(out int value, int minValue, int maxValue, string errorMessage)
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out value) && value >= minValue && value <= maxValue)
                return true;
            Console.WriteLine(errorMessage);
        }
    }

    static bool IsValidCityName(string city, int maxLength)
    {
        return !string.IsNullOrWhiteSpace(city)
               && city.Length <= maxLength
               && !city.Contains(' ')
               && !city.Any(char.IsDigit);
    }
}