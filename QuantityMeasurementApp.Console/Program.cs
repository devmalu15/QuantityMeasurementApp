using System;
using QuantityMeasurementApp.ConsoleApp.Models;

namespace QuantityMeasurementApp.ConsoleApp
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			double hardInchA = 1.0, hardInchB = 1.0;
			bool inchesEqual = QuantityMeasurementApp.AreInchesEqual(hardInchA, hardInchB);
			Console.WriteLine($"Input: {hardInchA} inch and {hardInchB} inch");
			Console.WriteLine($"Output: Equal ({inchesEqual.ToString().ToLowerInvariant()})");

			double hardFeetA = 1.0, hardFeetB = 1.0;
			bool feetEqual = QuantityMeasurementApp.AreFeetEqual(hardFeetA, hardFeetB);
			Console.WriteLine($"Input: {hardFeetA} ft and {hardFeetB} ft");
			Console.WriteLine($"Output: Equal ({feetEqual.ToString().ToLowerInvariant()})");

			bool cross = QuantityMeasurementApp.AreEqualAcrossUnits(1.0, Models.LengthUnit.Feet, 12.0, Models.LengthUnit.Inch);
			Console.WriteLine($"Input: Quantity(1.0, \"feet\") and Quantity(12.0, \"inches\")");
			Console.WriteLine($"Output: Equal ({cross.ToString().ToLowerInvariant()})");
			Console.WriteLine("\nInteractive check — Enter first measurement in feet:");
			string input1 = Console.ReadLine();
			Console.WriteLine("Enter second measurement in feet:");
			string input2 = Console.ReadLine();

			if (double.TryParse(input1, out double first) &&
				double.TryParse(input2, out double second))
			{
				bool equal = QuantityMeasurementApp.AreFeetEqual(first, second);
				Console.WriteLine($"Input: {first} ft and {second} ft");
				Console.WriteLine($"Output: Equal ({equal.ToString().ToLowerInvariant()})");
			}
			else
			{
				Console.WriteLine("Invalid numeric input. Please enter numbers.");
			}

            // UC8 demonstration: direct unit conversions
            Console.WriteLine("\nUC8: Unit methods demonstration");
            double inchesToFeet = LengthUnit.Inch.ConvertToBaseUnit(12.0);
            Console.WriteLine($"LengthUnit.Inch.ConvertToBaseUnit(12.0) = {inchesToFeet}");
            double feetToInches = LengthUnit.Inch.ConvertFromBaseUnit(1.0);
            Console.WriteLine($"LengthUnit.Inch.ConvertFromBaseUnit(1.0) = {feetToInches}");
        }
    }
}
