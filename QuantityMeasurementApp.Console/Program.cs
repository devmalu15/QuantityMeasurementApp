using System;
using QuantityMeasurementApp.ConsoleApp.Models;
using QApp = QuantityMeasurementApp.ConsoleApp.QuantityMeasurementApp;

namespace QuantityMeasurementApp.ConsoleApp
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			bool exit = false;
			while (!exit)
			{
				PrintMainMenu();
				string choice = Console.ReadLine();

				switch (choice?.Trim().ToLower())
				{
					case "1":
						HandleLengthOperations();
						break;
					case "2":
						HandleWeightOperations();
						break;
					case "3":
						HandleVolumeOperations();
						break;
					case "4":
						DisplayQuickDemo();
						break;
					case "5":
						exit = true;
						Console.WriteLine("\nGoodbye!");
						break;
					default:
						Console.WriteLine("\nInvalid choice. Please try again.\n");
						break;
				}
			}
		}

		private static void PrintMainMenu()
		{
			Console.WriteLine("\n========================================");
			Console.WriteLine("  QUANTITY MEASUREMENT APPLICATION");
			Console.WriteLine("========================================");
			Console.WriteLine("1. Length Measurements (feet, inch, yard, cm)");
			Console.WriteLine("2. Weight Measurements (kg, gram, pound)");
			Console.WriteLine("3. Volume Measurements (litre, ml, gallon)");
			Console.WriteLine("4. Quick Demo");
			Console.WriteLine("5. Exit");
			Console.WriteLine("========================================");
			Console.Write("Select option: ");
		}

		private static void HandleLengthOperations()
		{
			bool back = false;
			while (!back)
			{
				Console.WriteLine("\n--- LENGTH OPERATIONS ---");
				Console.WriteLine("1. Compare two lengths (Equality)");
				Console.WriteLine("2. Convert length unit");
				Console.WriteLine("3. Add two lengths");
				Console.WriteLine("4. Back to main menu");
				Console.Write("Select operation: ");

				string choice = Console.ReadLine();
				switch (choice?.Trim().ToLower())
				{
					case "1":
						CompareLengths();
						break;
					case "2":
						ConvertLength();
						break;
					case "3":
						AddLengths();
						break;
					case "4":
						back = true;
						break;
					default:
						Console.WriteLine("Invalid choice.");
						break;
				}
			}
		}

		private static void HandleWeightOperations()
		{
			bool back = false;
			while (!back)
			{
				Console.WriteLine("\n--- WEIGHT OPERATIONS ---");
				Console.WriteLine("1. Compare two weights (Equality)");
				Console.WriteLine("2. Convert weight unit");
				Console.WriteLine("3. Add two weights");
				Console.WriteLine("4. Back to main menu");
				Console.Write("Select operation: ");

				string choice = Console.ReadLine();
				switch (choice?.Trim().ToLower())
				{
					case "1":
						CompareWeights();
						break;
					case "2":
						ConvertWeight();
						break;
					case "3":
						AddWeights();
						break;
					case "4":
						back = true;
						break;
					default:
						Console.WriteLine("Invalid choice.");
						break;
				}
			}
		}

		private static void HandleVolumeOperations()
		{
			bool back = false;
			while (!back)
			{
				Console.WriteLine("\n--- VOLUME OPERATIONS ---");
				Console.WriteLine("1. Compare two volumes (Equality)");
				Console.WriteLine("2. Convert volume unit");
				Console.WriteLine("3. Add two volumes");
				Console.WriteLine("4. Back to main menu");
				Console.Write("Select operation: ");

				string choice = Console.ReadLine();
				switch (choice?.Trim().ToLower())
				{
					case "1":
						CompareVolumes();
						break;
					case "2":
						ConvertVolume();
						break;
					case "3":
						AddVolumes();
						break;
					case "4":
						back = true;
						break;
					default:
						Console.WriteLine("Invalid choice.");
						break;
				}
			}
		}

		// ===== LENGTH HANDLERS =====
		private static void CompareLengths()
		{
			Console.WriteLine("\nAvailable length units: Feet, Inch, Yard, Centimeter");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Feet/Inch/Yard/Centimeter): ");
			if (!Enum.TryParse<LengthUnit>(Console.ReadLine(), true, out var unit1)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Feet/Inch/Yard/Centimeter): ");
			if (!Enum.TryParse<LengthUnit>(Console.ReadLine(), true, out var unit2)) { Console.WriteLine("Invalid unit."); return; }

			bool equal = QApp.AreEqualAcrossUnits(val1, unit1, val2, unit2);
			Console.WriteLine($"\n{val1} {unit1} equals {val2} {unit2}? {equal.ToString().ToLowerInvariant()}");
		}

		private static void ConvertLength()
		{
			Console.WriteLine("\nAvailable length units: Feet, Inch, Yard, Centimeter");
			Console.Write("Enter value: ");
			if (!double.TryParse(Console.ReadLine(), out double val)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter source unit (Feet/Inch/Yard/Centimeter): ");
			if (!Enum.TryParse<LengthUnit>(Console.ReadLine(), true, out var sourceUnit)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter target unit (Feet/Inch/Yard/Centimeter): ");
			if (!Enum.TryParse<LengthUnit>(Console.ReadLine(), true, out var targetUnit)) { Console.WriteLine("Invalid unit."); return; }

			double result = QApp.Convert(val, sourceUnit, targetUnit);
			Console.WriteLine($"\n{val} {sourceUnit} = {result} {targetUnit}");
		}

		private static void AddLengths()
		{
			Console.WriteLine("\nAvailable length units: Feet, Inch, Yard, Centimeter");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Feet/Inch/Yard/Centimeter): ");
			if (!Enum.TryParse<LengthUnit>(Console.ReadLine(), true, out var unit1)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Feet/Inch/Yard/Centimeter): ");
			if (!Enum.TryParse<LengthUnit>(Console.ReadLine(), true, out var unit2)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter result unit (Feet/Inch/Yard/Centimeter): ");
			if (!Enum.TryParse<LengthUnit>(Console.ReadLine(), true, out var resultUnit)) { Console.WriteLine("Invalid unit."); return; }

			double result = QApp.Add(val1, unit1, val2, unit2, resultUnit);
			Console.WriteLine($"\n{val1} {unit1} + {val2} {unit2} = {result} {resultUnit}");
		}

		// ===== WEIGHT HANDLERS =====
		private static void CompareWeights()
		{
			Console.WriteLine("\nAvailable weight units: Kilogram, Gram, Pound");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Kilogram/Gram/Pound): ");
			if (!Enum.TryParse<WeightUnit>(Console.ReadLine(), true, out var unit1)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Kilogram/Gram/Pound): ");
			if (!Enum.TryParse<WeightUnit>(Console.ReadLine(), true, out var unit2)) { Console.WriteLine("Invalid unit."); return; }

			bool equal = QApp.AreEqualAcrossWeightUnits(val1, unit1, val2, unit2);
			Console.WriteLine($"\n{val1} {unit1} equals {val2} {unit2}? {equal.ToString().ToLowerInvariant()}");
		}

		private static void ConvertWeight()
		{
			Console.WriteLine("\nAvailable weight units: Kilogram, Gram, Pound");
			Console.Write("Enter value: ");
			if (!double.TryParse(Console.ReadLine(), out double val)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter source unit (Kilogram/Gram/Pound): ");
			if (!Enum.TryParse<WeightUnit>(Console.ReadLine(), true, out var sourceUnit)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter target unit (Kilogram/Gram/Pound): ");
			if (!Enum.TryParse<WeightUnit>(Console.ReadLine(), true, out var targetUnit)) { Console.WriteLine("Invalid unit."); return; }

			double result = QApp.Convert(val, sourceUnit, targetUnit);
			Console.WriteLine($"\n{val} {sourceUnit} = {result} {targetUnit}");
		}

		private static void AddWeights()
		{
			Console.WriteLine("\nAvailable weight units: Kilogram, Gram, Pound");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Kilogram/Gram/Pound): ");
			if (!Enum.TryParse<WeightUnit>(Console.ReadLine(), true, out var unit1)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Kilogram/Gram/Pound): ");
			if (!Enum.TryParse<WeightUnit>(Console.ReadLine(), true, out var unit2)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter result unit (Kilogram/Gram/Pound): ");
			if (!Enum.TryParse<WeightUnit>(Console.ReadLine(), true, out var resultUnit)) { Console.WriteLine("Invalid unit."); return; }

			double result = QApp.Add(val1, unit1, val2, unit2, resultUnit);
			Console.WriteLine($"\n{val1} {unit1} + {val2} {unit2} = {result} {resultUnit}");
		}

		// ===== VOLUME HANDLERS =====
		private static void CompareVolumes()
		{
			Console.WriteLine("\nAvailable volume units: Litre, Millilitre, Gallon");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Litre/Millilitre/Gallon): ");
			if (!Enum.TryParse<VolumeUnit>(Console.ReadLine(), true, out var unit1)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Litre/Millilitre/Gallon): ");
			if (!Enum.TryParse<VolumeUnit>(Console.ReadLine(), true, out var unit2)) { Console.WriteLine("Invalid unit."); return; }

			var v1 = new Quantity<VolumeUnit>(val1, unit1);
			var v2 = new Quantity<VolumeUnit>(val2, unit2);
			bool equal = v1.Equals(v2);
			Console.WriteLine($"\n{val1} {unit1} equals {val2} {unit2}? {equal.ToString().ToLowerInvariant()}");
		}

		private static void ConvertVolume()
		{
			Console.WriteLine("\nAvailable volume units: Litre, Millilitre, Gallon");
			Console.Write("Enter value: ");
			if (!double.TryParse(Console.ReadLine(), out double val)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter source unit (Litre/Millilitre/Gallon): ");
			if (!Enum.TryParse<VolumeUnit>(Console.ReadLine(), true, out var sourceUnit)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter target unit (Litre/Millilitre/Gallon): ");
			if (!Enum.TryParse<VolumeUnit>(Console.ReadLine(), true, out var targetUnit)) { Console.WriteLine("Invalid unit."); return; }

			var v = new Quantity<VolumeUnit>(val, sourceUnit);
			var converted = v.ConvertTo(targetUnit);
			Console.WriteLine($"\n{val} {sourceUnit} = {converted.Value} {targetUnit}");
		}

		private static void AddVolumes()
		{
			Console.WriteLine("\nAvailable volume units: Litre, Millilitre, Gallon");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Litre/Millilitre/Gallon): ");
			if (!Enum.TryParse<VolumeUnit>(Console.ReadLine(), true, out var unit1)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Litre/Millilitre/Gallon): ");
			if (!Enum.TryParse<VolumeUnit>(Console.ReadLine(), true, out var unit2)) { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter result unit (Litre/Millilitre/Gallon): ");
			if (!Enum.TryParse<VolumeUnit>(Console.ReadLine(), true, out var resultUnit)) { Console.WriteLine("Invalid unit."); return; }

			var v1 = new Quantity<VolumeUnit>(val1, unit1);
			var v2 = new Quantity<VolumeUnit>(val2, unit2);
			var result = v1.Add(v2, resultUnit);
			Console.WriteLine($"\n{val1} {unit1} + {val2} {unit2} = {result.Value} {resultUnit}");
		}

		// ===== DEMO =====
		private static void DisplayQuickDemo()
		{
			Console.WriteLine("\n========== QUICK DEMO ==========");
			
			// Length demo
			Console.WriteLine("\n--- LENGTH EXAMPLES ---");
			Console.WriteLine("1 foot = 12 inches? " + QApp.AreEqualAcrossUnits(1.0, LengthUnit.Feet, 12.0, LengthUnit.Inch));
			Console.WriteLine("1 yard = 3 feet? " + QApp.AreEqualAcrossUnits(1.0, LengthUnit.Yard, 3.0, LengthUnit.Feet));
			double cmToInch = QApp.Convert(2.54, LengthUnit.Centimeter, LengthUnit.Inch);
			Console.WriteLine($"2.54 cm = {cmToInch} inches");
			Console.WriteLine($"1 foot + 12 inches = {QApp.Add(1.0, LengthUnit.Feet, 12.0, LengthUnit.Inch, LengthUnit.Feet)} feet");

			// Weight demo
			Console.WriteLine("\n--- WEIGHT EXAMPLES ---");
			Console.WriteLine("1 kg = 1000 grams? " + QApp.AreEqualAcrossWeightUnits(1.0, WeightUnit.Kilogram, 1000.0, WeightUnit.Gram));
			Console.WriteLine("1 pound = 453.592 grams? " + QApp.AreEqualAcrossWeightUnits(1.0, WeightUnit.Pound, 453.592, WeightUnit.Gram));
			double poundsToKg = QApp.Convert(2.20462, WeightUnit.Pound, WeightUnit.Kilogram);
			Console.WriteLine($"2.20462 pounds = {poundsToKg} kg");
			Console.WriteLine($"1 kg + 1 pound = {QApp.Add(1.0, WeightUnit.Kilogram, 1.0, WeightUnit.Pound, WeightUnit.Kilogram)} kg");

			// Volume demo
			Console.WriteLine("\n--- VOLUME EXAMPLES ---");
			var v1 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Litre);
			var v2 = new Quantity<VolumeUnit>(1000.0, VolumeUnit.Millilitre);
			Console.WriteLine($"1 litre = 1000 ml? {v1.Equals(v2)}");
			var v3 = v1.ConvertTo(VolumeUnit.Gallon);
			Console.WriteLine($"1 litre = {v3.Value} gallons");
			var v4 = new Quantity<VolumeUnit>(1.0, VolumeUnit.Gallon);
			var vsum = v1.Add(v4, VolumeUnit.Millilitre);
			Console.WriteLine($"1 litre + 1 gallon = {vsum.Value} ml");

			Console.WriteLine("\n================================");
		}
	}
}
