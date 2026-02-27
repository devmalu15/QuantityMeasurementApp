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
    }
}
