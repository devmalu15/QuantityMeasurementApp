using Microsoft.VisualStudio.TestTools.UnitTesting;
using QApp = QuantityMeasurementApp.ConsoleApp.QuantityMeasurementApp;
using QuantityMeasurementApp.ConsoleApp.Models;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityMeasurementAppTest
    {
        [TestMethod]
        public void testEquality_SameValue()
        {
            bool result = QApp.AreFeetEqual(1.0, 1.0);
            Assert.IsTrue(result, "1.0 ft should equal 1.0 ft");
        }

        [TestMethod]
        public void testEquality_DifferentValue()
        {
            bool result = QApp.AreFeetEqual(1.0, 2.0);
            Assert.IsFalse(result, "1.0 ft should not equal 2.0 ft");
        }

        [TestMethod]
        public void testEquality_SameReference()
        {
            var f = new Feet(5.0);
            Assert.IsTrue(f.Equals(f), "Object should be equal to itself (reflexive)");
        }

        [TestMethod]
        public void testEquality_NullComparison()
        {
            var f = new Feet(3.0);
            Assert.IsFalse(f.Equals(null), "Comparison with null should return false");
        }

        [TestMethod]
        public void testEquality_NonNumericInput()
        {
            var f = new Feet(2.0);
            Assert.IsFalse(f.Equals("not a feet"), "Comparison against different type should return false");
        }

        [TestMethod]
        public void testInchesEquality_SameValue()
        {
            bool result = QApp.AreInchesEqual(1.0, 1.0);
            Assert.IsTrue(result, "1.0 inch should equal 1.0 inch");
        }

        [TestMethod]
        public void testInchesEquality_DifferentValue()
        {
            bool result = QApp.AreInchesEqual(1.0, 2.0);
            Assert.IsFalse(result, "1.0 inch should not equal 2.0 inch");
        }

        [TestMethod]
        public void testInchesEquality_SameReference()
        {
            var i = new Inches(4.0);
            Assert.IsTrue(i.Equals(i), "Inches object should be equal to itself (reflexive)");
        }

        [TestMethod]
        public void testInchesEquality_NullComparison()
        {
            var i = new Inches(6.0);
            Assert.IsFalse(i.Equals(null), "Comparison with null should return false");
        }

        [TestMethod]
        public void testInchesEquality_NonNumericInput()
        {
            var i = new Inches(2.0);
            Assert.IsFalse(i.Equals(123), "Comparison against different type (int) should return false");
        }

        [TestMethod]
        public void testEquality_FeetToInch_EquivalentValue()
        {
            bool result = QApp.AreEqualAcrossUnits(1.0, LengthUnit.Feet, 12.0, LengthUnit.Inch);
            Assert.IsTrue(result, "1.0 ft should equal 12.0 inch");
        }

        [TestMethod]
        public void testEquality_InchToFeet_EquivalentValue_Symmetric()
        {
            bool a = QApp.AreEqualAcrossUnits(12.0, LengthUnit.Inch, 1.0, LengthUnit.Feet);
            bool b = QApp.AreEqualAcrossUnits(1.0, LengthUnit.Feet, 12.0, LengthUnit.Inch);
            Assert.IsTrue(a && b, "Symmetric cross-unit equality must hold");
        }

        [TestMethod]
        public void testEquality_InvalidUnit_Throws()
        {
            bool thrown = false;
            try
            {
                QApp.AreEqualAcrossUnits(1.0, (LengthUnit)999, 1.0, LengthUnit.Feet);
            }
            catch (ArgumentOutOfRangeException)
            {
                thrown = true;
            }
            Assert.IsTrue(thrown, "Expected ArgumentOutOfRangeException for invalid unit");
        }

        [TestMethod]
        public void testEquality_YardToYard_SameValue()
        {
            bool result = QApp.AreEqualAcrossUnits(1.0, LengthUnit.Yard, 1.0, LengthUnit.Yard);
            Assert.IsTrue(result, "1.0 yard should equal 1.0 yard");
        }

        [TestMethod]
        public void testEquality_YardToYard_DifferentValue()
        {
            bool result = QApp.AreEqualAcrossUnits(1.0, LengthUnit.Yard, 2.0, LengthUnit.Yard);
            Assert.IsFalse(result, "1.0 yard should not equal 2.0 yard");
        }

        [TestMethod]
        public void testEquality_YardToFeet_EquivalentValue()
        {
            bool result = QApp.AreEqualAcrossUnits(1.0, LengthUnit.Yard, 3.0, LengthUnit.Feet);
            Assert.IsTrue(result, "1.0 yard should equal 3.0 feet");
        }

        [TestMethod]
        public void testEquality_YardToInches_EquivalentValue()
        {
            bool result = QApp.AreEqualAcrossUnits(1.0, LengthUnit.Yard, 36.0, LengthUnit.Inch);
            Assert.IsTrue(result, "1.0 yard should equal 36.0 inches");
        }

        [TestMethod]
        public void testEquality_CentimeterToCentimeter_SameValue()
        {
            bool result = QApp.AreEqualAcrossUnits(2.0, LengthUnit.Centimeter, 2.0, LengthUnit.Centimeter);
            Assert.IsTrue(result, "2.0 cm should equal 2.0 cm");
        }

        [TestMethod]
        public void testEquality_CentimeterToInch_EquivalentValue()
        {
            bool result = QApp.AreEqualAcrossUnits(1.0, LengthUnit.Centimeter, 0.393701, LengthUnit.Inch);
            Assert.IsTrue(result, "1.0 cm should equal 0.393701 inches");
        }

        [TestMethod]
        public void testEquality_CentimeterToFeet_NonEquivalentValue()
        {
            bool result = QApp.AreEqualAcrossUnits(1.0, LengthUnit.Centimeter, 1.0, LengthUnit.Feet);
            Assert.IsFalse(result, "1.0 cm should not equal 1.0 feet");
        }

        [TestMethod]
        public void testEquality_MultiUnit_TransitiveProperty()
        {
            bool a = QApp.AreEqualAcrossUnits(1.0, LengthUnit.Yard, 3.0, LengthUnit.Feet);
            bool b = QApp.AreEqualAcrossUnits(3.0, LengthUnit.Feet, 36.0, LengthUnit.Inch);
            bool c = QApp.AreEqualAcrossUnits(1.0, LengthUnit.Yard, 36.0, LengthUnit.Inch);
            Assert.IsTrue(a && b && c, "Transitive equality should hold across yard/feet/inch");
        }

        [TestMethod]
        public void testConversion_FeetToInches()
        {
            double result = QApp.Convert(1.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(12.0, result, 1e-9, "1 foot should be 12 inches");
        }

        [TestMethod]
        public void testConversion_InchesToFeet()
        {
            double result = QApp.Convert(24.0, LengthUnit.Inch, LengthUnit.Feet);
            Assert.AreEqual(2.0, result, 1e-9, "24 inches should be 2 feet");
        }

        [TestMethod]
        public void testConversion_YardsToInches()
        {
            double result = QApp.Convert(1.0, LengthUnit.Yard, LengthUnit.Inch);
            Assert.AreEqual(36.0, result, 1e-9, "1 yard should be 36 inches");
        }

        [TestMethod]
        public void testConversion_InchesToYards()
        {
            double result = QApp.Convert(72.0, LengthUnit.Inch, LengthUnit.Yard);
            Assert.AreEqual(2.0, result, 1e-9, "72 inches should be 2 yards");
        }

        [TestMethod]
        public void testConversion_CentimetersToInches()
        {
            double result = QApp.Convert(2.54, LengthUnit.Centimeter, LengthUnit.Inch);
            Assert.AreEqual(1.0, result, 1e-6, "2.54 cm should be approximately 1 inch");
        }

        [TestMethod]
        public void testConversion_FootToYard()
        {
            double result = QApp.Convert(6.0, LengthUnit.Feet, LengthUnit.Yard);
            Assert.AreEqual(2.0, result, 1e-9, "6 feet should be 2 yards");
        }

        [TestMethod]
        public void testConversion_RoundTrip_PreservesValue()
        {
            double v = 5.5;
            double a = QApp.Convert(v, LengthUnit.Feet, LengthUnit.Inch);
            double b = QApp.Convert(a, LengthUnit.Inch, LengthUnit.Feet);
            Assert.AreEqual(v, b, 1e-6, "Round-trip conversion should preserve value within tolerance");
        }

        [TestMethod]
        public void testConversion_ZeroValue()
        {
            double result = QApp.Convert(0.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(0.0, result, 1e-9, "0 feet should be 0 inches");
        }

        [TestMethod]
        public void testConversion_NegativeValue()
        {
            double result = QApp.Convert(-1.0, LengthUnit.Feet, LengthUnit.Inch);
            Assert.AreEqual(-12.0, result, 1e-9, "-1 foot should be -12 inches");
        }

        [TestMethod]
        public void testConversion_InvalidValue_Throws()
        {
            bool thrown = false;
            try
            {
                QApp.Convert(double.NaN, LengthUnit.Feet, LengthUnit.Inch);
            }
            catch (ArgumentException)
            {
                thrown = true;
            }
            Assert.IsTrue(thrown, "NaN value should cause ArgumentException");
        }

        [TestMethod]
        public void testConversion_InvalidUnit_Throws()
        {
            bool thrown = false;
            try
            {
                QApp.Convert(1.0, (LengthUnit)999, LengthUnit.Feet);
            }
            catch (ArgumentOutOfRangeException)
            {
                thrown = true;
            }
            Assert.IsTrue(thrown, "Invalid unit should cause ArgumentOutOfRangeException");
        }

        [TestMethod]
        public void testAddition_SameUnit_FeetPlusFeet()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(2.0, LengthUnit.Feet);
            var result = QApp.Add(q1, q2);
            Assert.AreEqual(3.0, result.Value, 1e-9, "1 ft + 2 ft should be 3 ft");
            Assert.AreEqual(LengthUnit.Feet, result.Unit, "Result should be in feet");
        }

        [TestMethod]
        public void testAddition_SameUnit_InchPlusInch()
        {
            var q1 = new QuantityLength(6.0, LengthUnit.Inch);
            var q2 = new QuantityLength(6.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2);
            Assert.AreEqual(12.0, result.Value, 1e-9, "6 in + 6 in should be 12 in");
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [TestMethod]
        public void testAddition_CrossUnit_FeetPlusInches()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2);
            Assert.AreEqual(2.0, result.Value, 1e-9, "1 ft + 12 in should be 2 ft");
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_CrossUnit_InchPlusFeet()
        {
            var q1 = new QuantityLength(12.0, LengthUnit.Inch);
            var q2 = new QuantityLength(1.0, LengthUnit.Feet);
            var result = QApp.Add(q1, q2);
            Assert.AreEqual(24.0, result.Value, 1e-9, "12 in + 1 ft should be 24 in");
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [TestMethod]
        public void testAddition_CrossUnit_YardPlusFeet()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Yard);
            var q2 = new QuantityLength(3.0, LengthUnit.Feet);
            var result = QApp.Add(q1, q2);
            Assert.AreEqual(2.0, result.Value, 1e-9, "1 yd + 3 ft should be 2 yd");
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [TestMethod]
        public void testAddition_CrossUnit_CentimeterPlusInch()
        {
            var q1 = new QuantityLength(2.54, LengthUnit.Centimeter);
            var q2 = new QuantityLength(1.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2);
            Assert.AreEqual(5.08, result.Value, 1e-4, "2.54 cm + 1 in should be ~5.08 cm");
            Assert.AreEqual(LengthUnit.Centimeter, result.Unit);
        }

        [TestMethod]
        public void testAddition_WithZero()
        {
            var q1 = new QuantityLength(5.0, LengthUnit.Feet);
            var q2 = new QuantityLength(0.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2);
            Assert.AreEqual(5.0, result.Value, 1e-9, "5 ft + 0 in should be 5 ft");
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_NegativeValues()
        {
            var q1 = new QuantityLength(5.0, LengthUnit.Feet);
            var q2 = new QuantityLength(-2.0, LengthUnit.Feet);
            var result = QApp.Add(q1, q2);
            Assert.AreEqual(3.0, result.Value, 1e-9, "5 ft + (-2 ft) should be 3 ft");
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_NullOperand_Throws()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            bool thrown = false;
            try
            {
                QApp.Add(q1, null);
            }
            catch (ArgumentNullException)
            {
                thrown = true;
            }
            Assert.IsTrue(thrown, "Null operand should throw ArgumentNullException");
        }

        [TestMethod]
        public void testAddition_LargeValues()
        {
            var q1 = new QuantityLength(1e6, LengthUnit.Feet);
            var q2 = new QuantityLength(1e6, LengthUnit.Feet);
            var result = QApp.Add(q1, q2);
            Assert.AreEqual(2e6, result.Value, 1e3, "Large value addition should work");
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_SmallValues()
        {
            var q1 = new QuantityLength(0.001, LengthUnit.Feet);
            var q2 = new QuantityLength(0.002, LengthUnit.Feet);
            var result = QApp.Add(q1, q2);
            Assert.AreEqual(0.003, result.Value, 1e-9, "Small value addition should work");
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_StaticMethodFeetPlusInches()
        {
            double result = QApp.Add(1.0, LengthUnit.Feet, 12.0, LengthUnit.Inch, LengthUnit.Feet);
            Assert.AreEqual(2.0, result, 1e-9, "Static add: 1 ft + 12 in should be 2 ft");
        }

        [TestMethod]
        public void testAddition_YardPlusInchesInYards()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Yard);
            var q2 = new QuantityLength(36.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2);
            Assert.AreEqual(2.0, result.Value, 1e-9, "1 yd + 36 in should be 2 yd");
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Feet()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2, LengthUnit.Feet);
            Assert.AreEqual(2.0, result.Value, 1e-9, "1 ft + 12 in in feet should be 2 ft");
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Inches()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2, LengthUnit.Inch);
            Assert.AreEqual(24.0, result.Value, 1e-9, "1 ft + 12 in in inches should be 24 in");
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Yards()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2, LengthUnit.Yard);
            Assert.AreEqual(0.66666666, result.Value, 1e-7, "1 ft + 12 in in yards should be ~0.667 yd");
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Centimeters()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Inch);
            var q2 = new QuantityLength(1.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2, LengthUnit.Centimeter);
            Assert.AreEqual(5.08, result.Value, 1e-4, "1 in + 1 in in centimeters should be ~5.08 cm");
            Assert.AreEqual(LengthUnit.Centimeter, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_SameAsFirstOperand()
        {
            var q1 = new QuantityLength(2.0, LengthUnit.Yard);
            var q2 = new QuantityLength(3.0, LengthUnit.Feet);
            var result = QApp.Add(q1, q2, LengthUnit.Yard);
            Assert.AreEqual(3.0, result.Value, 1e-9, "2 yd + 3 ft in yards should be 3 yd");
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_SameAsSecondOperand()
        {
            var q1 = new QuantityLength(2.0, LengthUnit.Yard);
            var q2 = new QuantityLength(3.0, LengthUnit.Feet);
            var result = QApp.Add(q1, q2, LengthUnit.Feet);
            Assert.AreEqual(9.0, result.Value, 1e-9, "2 yd + 3 ft in feet should be 9 ft");
            Assert.AreEqual(LengthUnit.Feet, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Commutativity()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result1 = QApp.Add(q1, q2, LengthUnit.Yard);
            var result2 = QApp.Add(q2, q1, LengthUnit.Yard);
            Assert.AreEqual(result1.Value, result2.Value, 1e-9, "Addition should be commutative with explicit target");
            Assert.AreEqual(result1.Unit, result2.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_WithZero()
        {
            var q1 = new QuantityLength(5.0, LengthUnit.Feet);
            var q2 = new QuantityLength(0.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2, LengthUnit.Yard);
            Assert.AreEqual(1.66666666, result.Value, 1e-7, "5 ft + 0 in in yards should be ~1.667 yd");
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_NegativeValues()
        {
            var q1 = new QuantityLength(5.0, LengthUnit.Feet);
            var q2 = new QuantityLength(-2.0, LengthUnit.Feet);
            var result = QApp.Add(q1, q2, LengthUnit.Inch);
            Assert.AreEqual(36.0, result.Value, 1e-9, "5 ft + (-2 ft) in inches should be 36 in");
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_NullTargetUnit()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            bool thrown = false;
            try
            {
                QApp.Add(q1, q2, null);
            }
            catch (ArgumentNullException)
            {
                thrown = true;
            }
            Assert.IsTrue(thrown, "Null target unit should throw ArgumentNullException");
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_LargeToSmallScale()
        {
            var q1 = new QuantityLength(1000.0, LengthUnit.Feet);
            var q2 = new QuantityLength(500.0, LengthUnit.Feet);
            var result = QApp.Add(q1, q2, LengthUnit.Inch);
            Assert.AreEqual(18000.0, result.Value, 10.0, "1000 ft + 500 ft in inches should be 18000 in");
            Assert.AreEqual(LengthUnit.Inch, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_SmallToLargeScale()
        {
            var q1 = new QuantityLength(12.0, LengthUnit.Inch);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2, LengthUnit.Yard);
            Assert.AreEqual(0.66666666, result.Value, 1e-7, "12 in + 12 in in yards should be ~0.667 yd");
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_AllCombinations()
        {
            var feet = new QuantityLength(1.0, LengthUnit.Feet);
            var inches = new QuantityLength(12.0, LengthUnit.Inch);
            var yard = new QuantityLength(1.0, LengthUnit.Yard);
            var cm = new QuantityLength(30.48, LengthUnit.Centimeter);

            var result1 = QApp.Add(feet, inches, LengthUnit.Feet);
            Assert.AreEqual(2.0, result1.Value, 1e-9);

            var result2 = QApp.Add(yard, feet, LengthUnit.Yard);
            Assert.AreEqual(1.33333333, result2.Value, 1e-7);

            var result3 = QApp.Add(inches, cm, LengthUnit.Inch);
            Assert.AreEqual(24.0, result3.Value, 1e-5);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_PrecisionTolerance()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Yard);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = QApp.Add(q1, q2, LengthUnit.Feet);
            double yardInFeet = 3.0;
            double inchesInFeet = 1.0;
            double expectedSum = yardInFeet + inchesInFeet;
            Assert.AreEqual(expectedSum, result.Value, 1e-9, "Precision should be maintained across conversions");
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_InstanceMethod()
        {
            var q1 = new QuantityLength(1.0, LengthUnit.Feet);
            var q2 = new QuantityLength(12.0, LengthUnit.Inch);
            var result = q1.Add(q2, LengthUnit.Yard);
            Assert.AreEqual(0.66666666, result.Value, 1e-7, "Instance Add with target unit should work");
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_YardsCentimeters()
        {
            var yards = new QuantityLength(2.0, LengthUnit.Yard);
            var cm = new QuantityLength(30.48, LengthUnit.Centimeter);
            var result = QApp.Add(yards, cm, LengthUnit.Centimeter);
            double yardsInFeet = 6.0;
            double yardsInCm = yardsInFeet / (0.393701 / 12.0);
            double expectedSum = yardsInCm + 30.48;
            Assert.IsTrue(Math.Abs(result.Value - expectedSum) < 1.0, "Yards + cm in centimeters should be accurate");
            Assert.AreEqual(LengthUnit.Centimeter, result.Unit);
        }
    }
}

