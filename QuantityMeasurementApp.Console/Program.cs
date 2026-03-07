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
						HandleTemperatureOperations();
						break;
					case "5":
						DisplayQuickDemo();
						break;
					case "6":
						exit = true;
						Console.WriteLine("\nGoodbye!");
						break;
					default:
						Console.WriteLine("\nInvalid choice. Please try again.\n");
						break;
				}
			}
		}

		private static LengthUnit ParseLengthUnit(string input)
		{
			return input?.Trim().ToLower() switch
			{
				"feet" => LengthUnit.Feet,
				"inch" => LengthUnit.Inch,
				"yard" => LengthUnit.Yard,
				"centimeter" => LengthUnit.Centimeter,
				_ => throw new ArgumentException("Invalid length unit")
			};
		}

		private static WeightUnit ParseWeightUnit(string input)
		{
			return input?.Trim().ToLower() switch
			{
				"kilogram" => WeightUnit.Kilogram,
				"gram" => WeightUnit.Gram,
				"pound" => WeightUnit.Pound,
				_ => throw new ArgumentException("Invalid weight unit")
			};
		}

		private static VolumeUnit ParseVolumeUnit(string input)
		{
			return input?.Trim().ToLower() switch
			{
				"litre" => VolumeUnit.Litre,
				"millilitre" => VolumeUnit.Millilitre,
				"gallon" => VolumeUnit.Gallon,
				_ => throw new ArgumentException("Invalid volume unit")
			};
		}

		private static TemperatureUnit ParseTemperatureUnit(string input)
		{
			return input?.Trim().ToLower() switch
			{
				"celsius" => TemperatureUnit.Celsius,
				"fahrenheit" => TemperatureUnit.Fahrenheit,
				"kelvin" => TemperatureUnit.Kelvin,
				_ => throw new ArgumentException("Invalid temperature unit")
			};
		}

		private static void PrintMainMenu()
		{
			Console.WriteLine("\n========================================");
			Console.WriteLine("  QUANTITY MEASUREMENT APPLICATION");
			Console.WriteLine("========================================");
			Console.WriteLine("1. Length Measurements (feet, inch, yard, cm)");
			Console.WriteLine("2. Weight Measurements (kg, gram, pound)");
			Console.WriteLine("3. Volume Measurements (litre, ml, gallon)");
			Console.WriteLine("4. Temperature Measurements (Celsius, Fahrenheit, Kelvin)");
			Console.WriteLine("5. Quick Demo");
			Console.WriteLine("6. Exit");
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
				Console.WriteLine("4. Subtract two lengths");
				Console.WriteLine("5. Divide two lengths");
				Console.WriteLine("6. Back to main menu");
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
						SubtractLengths();
						break;
					case "5":
						DivideLengths();
						break;
					case "6":
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
				Console.WriteLine("4. Subtract two weights");
				Console.WriteLine("5. Divide two weights");
				Console.WriteLine("6. Back to main menu");
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
						SubtractWeights();
						break;
					case "5":
						DivideWeights();
						break;
					case "6":
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
				Console.WriteLine("4. Subtract two volumes");
				Console.WriteLine("5. Divide two volumes");
				Console.WriteLine("6. Back to main menu");
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
						SubtractVolumes();
						break;
					case "5":
						DivideVolumes();
						break;
					case "6":
						back = true;
						break;
					default:
						Console.WriteLine("Invalid choice.");
						break;
				}
			}
		}

		private static void HandleTemperatureOperations()
		{
			bool back = false;
			while (!back)
			{
				Console.WriteLine("\n--- TEMPERATURE OPERATIONS ---");
				Console.WriteLine("1. Compare two temperatures (Equality)");
				Console.WriteLine("2. Convert temperature unit");
				Console.WriteLine("3. Back to main menu");
				Console.Write("Select operation: ");

				string choice = Console.ReadLine();
				switch (choice?.Trim().ToLower())
				{
					case "1":
						CompareTemperatures();
						break;
					case "2":
						ConvertTemperature();
						break;
					case "3":
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
			LengthUnit unit1;
			try { unit1 = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Feet/Inch/Yard/Centimeter): ");
			LengthUnit unit2;
			try { unit2 = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			bool equal = new Quantity<LengthUnit>(val1, unit1).Equals(new Quantity<LengthUnit>(val2, unit2));
			Console.WriteLine($"\n{val1} {unit1} equals {val2} {unit2}? {equal.ToString().ToLowerInvariant()}");
		}

		private static void ConvertLength()
		{
			Console.WriteLine("\nAvailable length units: Feet, Inch, Yard, Centimeter");
			Console.Write("Enter value: ");
			if (!double.TryParse(Console.ReadLine(), out double val)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter source unit (Feet/Inch/Yard/Centimeter): ");
			LengthUnit sourceUnit;
			try { sourceUnit = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter target unit (Feet/Inch/Yard/Centimeter): ");
			LengthUnit targetUnit;
			try { targetUnit = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var q = new Quantity<LengthUnit>(val, sourceUnit);
			var converted = q.ConvertTo(targetUnit);
			Console.WriteLine($"\n{val} {sourceUnit} = {converted.Value} {converted.Unit}");
		}

		private static void AddLengths()
		{
			Console.WriteLine("\nAvailable length units: Feet, Inch, Yard, Centimeter");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Feet/Inch/Yard/Centimeter): ");
			LengthUnit unit1;
			try { unit1 = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Feet/Inch/Yard/Centimeter): ");
			LengthUnit unit2;
			try { unit2 = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter result unit (Feet/Inch/Yard/Centimeter): ");
			LengthUnit resultUnit;
			try { resultUnit = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var q1 = new Quantity<LengthUnit>(val1, unit1);
			var q2 = new Quantity<LengthUnit>(val2, unit2);
			var sum = q1.Add(q2, resultUnit);
			Console.WriteLine($"\n{val1} {unit1} + {val2} {unit2} = {sum.Value} {sum.Unit}");
		}

		// ===== WEIGHT HANDLERS =====
		private static void CompareWeights()
		{
			Console.WriteLine("\nAvailable weight units: Kilogram, Gram, Pound");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Kilogram/Gram/Pound): ");
			WeightUnit unit1;
			try { unit1 = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Kilogram/Gram/Pound): ");
			WeightUnit unit2;
			try { unit2 = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			bool equal = new Quantity<WeightUnit>(val1, unit1).Equals(new Quantity<WeightUnit>(val2, unit2));
			Console.WriteLine($"\n{val1} {unit1} equals {val2} {unit2}? {equal.ToString().ToLowerInvariant()}");
		}

		private static void ConvertWeight()
		{
			Console.WriteLine("\nAvailable weight units: Kilogram, Gram, Pound");
			Console.Write("Enter value: ");
			if (!double.TryParse(Console.ReadLine(), out double val)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter source unit (Kilogram/Gram/Pound): ");
			WeightUnit sourceUnit;
			try { sourceUnit = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter target unit (Kilogram/Gram/Pound): ");
			WeightUnit targetUnit;
			try { targetUnit = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var q = new Quantity<WeightUnit>(val, sourceUnit);
			var converted = q.ConvertTo(targetUnit);
			Console.WriteLine($"\n{val} {sourceUnit} = {converted.Value} {converted.Unit}");
		}

		private static void AddWeights()
		{
			Console.WriteLine("\nAvailable weight units: Kilogram, Gram, Pound");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Kilogram/Gram/Pound): ");
			WeightUnit unit1;
			try { unit1 = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Kilogram/Gram/Pound): ");
			WeightUnit unit2;
			try { unit2 = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter result unit (Kilogram/Gram/Pound): ");
			WeightUnit resultUnit;
			try { resultUnit = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var q1 = new Quantity<WeightUnit>(val1, unit1);
			var q2 = new Quantity<WeightUnit>(val2, unit2);
			var sum = q1.Add(q2, resultUnit);
			Console.WriteLine($"\n{val1} {unit1} + {val2} {unit2} = {sum.Value} {sum.Unit}");
		}

		// ===== VOLUME HANDLERS =====
		private static void CompareVolumes()
		{
			Console.WriteLine("\nAvailable volume units: Litre, Millilitre, Gallon");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Litre/Millilitre/Gallon): ");
			VolumeUnit unit1;
			try { unit1 = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Litre/Millilitre/Gallon): ");
			VolumeUnit unit2;
			try { unit2 = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

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
			VolumeUnit sourceUnit;
			try { sourceUnit = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter target unit (Litre/Millilitre/Gallon): ");
			VolumeUnit targetUnit;
			try { targetUnit = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var v = new Quantity<VolumeUnit>(val, sourceUnit);
			var converted = v.ConvertTo(targetUnit);
			Console.WriteLine($"\n{val} {sourceUnit} = {converted.Value} {converted.Unit}");
		}

		private static void AddVolumes()
		{
			Console.WriteLine("\nAvailable volume units: Litre, Millilitre, Gallon");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Litre/Millilitre/Gallon): ");
			VolumeUnit unit1;
			try { unit1 = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Litre/Millilitre/Gallon): ");
			VolumeUnit unit2;
			try { unit2 = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter result unit (Litre/Millilitre/Gallon): ");
			VolumeUnit resultUnit;
			try { resultUnit = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var v1 = new Quantity<VolumeUnit>(val1, unit1);
			var v2 = new Quantity<VolumeUnit>(val2, unit2);
			var result = v1.Add(v2, resultUnit);
			Console.WriteLine($"\n{val1} {unit1} + {val2} {unit2} = {result.Value} {result.Unit}");
		}

		// ===== SUBTRACT HANDLERS =====
		private static void SubtractLengths()
		{
			Console.WriteLine("\nAvailable length units: Feet, Inch, Yard, Centimeter");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Feet/Inch/Yard/Centimeter): ");
			LengthUnit unit1;
			try { unit1 = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Feet/Inch/Yard/Centimeter): ");
			LengthUnit unit2;
			try { unit2 = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter result unit (Feet/Inch/Yard/Centimeter): ");
			LengthUnit resultUnit;
			try { resultUnit = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var q1 = new Quantity<LengthUnit>(val1, unit1);
			var q2 = new Quantity<LengthUnit>(val2, unit2);
			var diff = q1.Subtract(q2, resultUnit);
			Console.WriteLine($"\n{val1} {unit1} - {val2} {unit2} = {diff.Value} {diff.Unit}");
		}

		private static void SubtractWeights()
		{
			Console.WriteLine("\nAvailable weight units: Kilogram, Gram, Pound");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Kilogram/Gram/Pound): ");
			WeightUnit unit1;
			try { unit1 = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Kilogram/Gram/Pound): ");
			WeightUnit unit2;
			try { unit2 = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter result unit (Kilogram/Gram/Pound): ");
			WeightUnit resultUnit;
			try { resultUnit = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var q1 = new Quantity<WeightUnit>(val1, unit1);
			var q2 = new Quantity<WeightUnit>(val2, unit2);
			var diff = q1.Subtract(q2, resultUnit);
			Console.WriteLine($"\n{val1} {unit1} - {val2} {unit2} = {diff.Value} {diff.Unit}");
		}

		private static void SubtractVolumes()
		{
			Console.WriteLine("\nAvailable volume units: Litre, Millilitre, Gallon");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Litre/Millilitre/Gallon): ");
			VolumeUnit unit1;
			try { unit1 = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Litre/Millilitre/Gallon): ");
			VolumeUnit unit2;
			try { unit2 = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter result unit (Litre/Millilitre/Gallon): ");
			VolumeUnit resultUnit;
			try { resultUnit = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var v1 = new Quantity<VolumeUnit>(val1, unit1);
			var v2 = new Quantity<VolumeUnit>(val2, unit2);
			var result = v1.Subtract(v2, resultUnit);
			Console.WriteLine($"\n{val1} {unit1} - {val2} {unit2} = {result.Value} {result.Unit}");
		}

		// ===== DIVIDE HANDLERS =====
		private static void DivideLengths()
		{
			Console.WriteLine("\nAvailable length units: Feet, Inch, Yard, Centimeter");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Feet/Inch/Yard/Centimeter): ");
			LengthUnit unit1;
			try { unit1 = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Feet/Inch/Yard/Centimeter): ");
			LengthUnit unit2;
			try { unit2 = ParseLengthUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var q1 = new Quantity<LengthUnit>(val1, unit1);
			var q2 = new Quantity<LengthUnit>(val2, unit2);
			double result = q1.Divide(q2);
			Console.WriteLine($"\n{val1} {unit1} / {val2} {unit2} = {result}");
		}

		private static void DivideWeights()
		{
			Console.WriteLine("\nAvailable weight units: Kilogram, Gram, Pound");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Kilogram/Gram/Pound): ");
			WeightUnit unit1;
			try { unit1 = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Kilogram/Gram/Pound): ");
			WeightUnit unit2;
			try { unit2 = ParseWeightUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var q1 = new Quantity<WeightUnit>(val1, unit1);
			var q2 = new Quantity<WeightUnit>(val2, unit2);
			double result = q1.Divide(q2);
			Console.WriteLine($"\n{val1} {unit1} / {val2} {unit2} = {result}");
		}

		private static void DivideVolumes()
		{
			Console.WriteLine("\nAvailable volume units: Litre, Millilitre, Gallon");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Litre/Millilitre/Gallon): ");
			VolumeUnit unit1;
			try { unit1 = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Litre/Millilitre/Gallon): ");
			VolumeUnit unit2;
			try { unit2 = ParseVolumeUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var v1 = new Quantity<VolumeUnit>(val1, unit1);
			var v2 = new Quantity<VolumeUnit>(val2, unit2);
			double result = v1.Divide(v2);
			Console.WriteLine($"\n{val1} {unit1} / {val2} {unit2} = {result}");
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

			// Temperature demo
			Console.WriteLine("\n--- TEMPERATURE EXAMPLES ---");
			var t1 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.Celsius);
			var t2 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.Fahrenheit);
			Console.WriteLine($"0°C = 32°F? {t1.Equals(t2)}");
			var t3 = t1.ConvertTo(TemperatureUnit.Kelvin);
			Console.WriteLine($"0°C = {t3.Value} K");
			// Note: Arithmetic operations are not supported for temperature
			try
			{
				var tsum = t1.Add(t2);
			}
			catch (NotSupportedException ex)
			{
				Console.WriteLine($"Temperature addition: {ex.Message}");
			}

			Console.WriteLine("\n================================");
		}

		// ===== TEMPERATURE HANDLERS =====
		private static void CompareTemperatures()
		{
			Console.WriteLine("\nAvailable temperature units: Celsius, Fahrenheit, Kelvin");
			Console.Write("Enter first value: ");
			if (!double.TryParse(Console.ReadLine(), out double val1)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter first unit (Celsius/Fahrenheit/Kelvin): ");
			TemperatureUnit unit1;
			try { unit1 = ParseTemperatureUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter second value: ");
			if (!double.TryParse(Console.ReadLine(), out double val2)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter second unit (Celsius/Fahrenheit/Kelvin): ");
			TemperatureUnit unit2;
			try { unit2 = ParseTemperatureUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			bool equal = new Quantity<TemperatureUnit>(val1, unit1).Equals(new Quantity<TemperatureUnit>(val2, unit2));
			Console.WriteLine($"\n{val1} {unit1} equals {val2} {unit2}? {equal.ToString().ToLowerInvariant()}");
		}

		private static void ConvertTemperature()
		{
			Console.WriteLine("\nAvailable temperature units: Celsius, Fahrenheit, Kelvin");
			Console.Write("Enter value: ");
			if (!double.TryParse(Console.ReadLine(), out double val)) { Console.WriteLine("Invalid input."); return; }

			Console.Write("Enter source unit (Celsius/Fahrenheit/Kelvin): ");
			TemperatureUnit sourceUnit;
			try { sourceUnit = ParseTemperatureUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			Console.Write("Enter target unit (Celsius/Fahrenheit/Kelvin): ");
			TemperatureUnit targetUnit;
			try { targetUnit = ParseTemperatureUnit(Console.ReadLine()); } catch { Console.WriteLine("Invalid unit."); return; }

			var q = new Quantity<TemperatureUnit>(val, sourceUnit);
			var converted = q.ConvertTo(targetUnit);
			Console.WriteLine($"\n{val} {sourceUnit} = {converted.Value} {converted.Unit}");
		}
	}
}
