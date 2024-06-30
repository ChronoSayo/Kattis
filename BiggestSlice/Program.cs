using System;
using System.Collections.Generic;

class Program
{
    static readonly double PI = Math.PI;

    static void Main(string[] args)
    {
        uint lines;
        while(true)
        {
            if (uint.TryParse(Console.ReadLine(), out lines))
                break;
            Console.WriteLine("Invalid input. Must be a positive integer.");
        }

        while (lines-- > 0)
        {
            string[] input;
            while (true)
            {
                input = Console.ReadLine().Split();
                if (IsValidInput(input, out string errorMessage))
                    break;
                Console.WriteLine(errorMessage);
            }

            double radius = double.Parse(input[0]);
            int numCuts = int.Parse(input[1]);
            double degrees = double.Parse(input[2]);
            double minutes = double.Parse(input[3]);
            double seconds = double.Parse(input[4]);

            minutes += seconds / 60.0;
            degrees += minutes / 60.0;

            HashSet<double> uniqueCutAngles = new HashSet<double>();
            double currentAngle = 0.0;

            for (int i = 0; i < numCuts; i++)
            {
                if (currentAngle >= 360.0)
                    currentAngle -= 360.0;
                if (uniqueCutAngles.Contains(currentAngle))
                    break;
                uniqueCutAngles.Add(currentAngle);
                currentAngle += degrees;
            }

            List<double> sortedCutAngles = new List<double>(uniqueCutAngles);
            sortedCutAngles.Sort();

            List<double> segmentSizes = new List<double>();
            for (int i = 1; i < sortedCutAngles.Count; i++)
            {
                segmentSizes.Add(sortedCutAngles[i] - sortedCutAngles[i - 1]);
            }
            segmentSizes.Add(360.0 - sortedCutAngles[sortedCutAngles.Count - 1] + sortedCutAngles[0]);

            double maxSegmentSize = -1;
            foreach (var s in segmentSizes)
            {
                if (s > maxSegmentSize)
                    maxSegmentSize = s;
            }

            double area = radius * radius * PI * (maxSegmentSize / 360.0);
            Console.WriteLine($"{area:F6}");
        }

        static bool IsValidInput(string[] input, out string errorMessage)
        {
            errorMessage = "";

            if (input.Length != 5)
            {
                errorMessage = "Invalid input: Expected 5 values (radius, cuts, degrees, minutes, seconds).";
                return false;
            }

            bool IsPositiveInteger(string value, uint min, uint max, string fieldName, out uint result, ref string error)
            {
                if (!uint.TryParse(value, out result) || result < min || result > max)
                {
                    error = $"Invalid {fieldName}: Must be a positive integer between {min} and {max}.";
                    return false;
                }
                return true;
            }

            bool IsPositiveNumber(string value, double min, double max, string fieldName, out double result, ref string error)
            {
                if (!double.TryParse(value, out result) || result < min || result >= max)
                {
                    error = $"Invalid {fieldName}: Must be between {min} and {max}.";
                    return false;
                }
                return true;
            }

            if (!IsPositiveInteger(input[0], 1, 100, "radius", out _, ref errorMessage)) 
                return false;
            if (!IsPositiveInteger(input[1], 1, 100000000, "cuts", out _, ref errorMessage)) 
                return false;
            if (!IsPositiveNumber(input[2], 0, 360, "degrees", out _, ref errorMessage)) 
                return false;
            if (!IsPositiveNumber(input[3], 0, 60, "minutes", out _, ref errorMessage)) 
                return false;
            if (!IsPositiveNumber(input[4], 0, 60, "seconds", out _, ref errorMessage)) 
                return false;

            return true;
        }
    }
}