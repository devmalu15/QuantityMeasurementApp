using System;

namespace QuantityMeasurementApp.ConsoleApp
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			// simple demonstration of feet equality
			Console.WriteLine("Enter first measurement in feet:");
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
		}
	}
}
