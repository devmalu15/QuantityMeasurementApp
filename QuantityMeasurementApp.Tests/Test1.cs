using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.ConsoleApp.Models;
using QApp = QuantityMeasurementApp.ConsoleApp.QuantityMeasurementApp;
using System;

namespace QuantityMeasurementApp.ConsoleApp.Tests
{
    [TestClass]
    public class QuantityTests
    {
        // we use extension methods rather than an interface for units

        [TestMethod]
        public void testGenericQuantity_LengthOperations_Equality()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.IsTrue(q1.Equals(q2));
        }

        [TestMethod]
        public void testGenericQuantity_LengthOperations_Conversion()
        {
            var q = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var converted = q.ConvertTo(LengthUnit.Inch);
            Assert.AreEqual(12.0, converted.Value, 1e-9);
            Assert.AreEqual(LengthUnit.Inch, converted.Unit);
        }

        [TestMethod]
        public void testGenericQuantity_LengthOperations_Addition()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            var result = q1.Add(q2, LengthUnit.Yard);
            Assert.AreEqual(0.67, Math.Round(result.Value, 2));
            Assert.AreEqual(LengthUnit.Yard, result.Unit);
        }

        [TestMethod]
        public void testGenericQuantity_WeightOperations_Equality()
        {
            var w1 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            var w2 = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);
            Assert.IsTrue(w1.Equals(w2));
        }

        [TestMethod]
        public void testGenericQuantity_WeightOperations_Conversion()
        {
            var w = new Quantity<WeightUnit>(2.20462, WeightUnit.Pound);
            var converted = w.ConvertTo(WeightUnit.Kilogram);
            Assert.AreEqual(1.0, converted.Value, 1e-5);
            Assert.AreEqual(WeightUnit.Kilogram, converted.Unit);
        }

        [TestMethod]
        public void testGenericQuantity_WeightOperations_Addition()
        {
            var w1 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            var w2 = new Quantity<WeightUnit>(1.0, WeightUnit.Pound);
            var result = w1.Add(w2);
            double expected = 1.0 + 0.453592;
            Assert.AreEqual(expected, result.Value, 1e-6);
            Assert.AreEqual(WeightUnit.Kilogram, result.Unit);
        }

        [TestMethod]
        public void testCrossCategoryPrevention_LengthVsWeight()
        {
            object qL = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            object qW = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            Assert.IsFalse(qL.Equals(qW));
        }

        [TestMethod]
        public void testQuantityMeasurementApp_SimplifiedDemonstration_Equality()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            Assert.IsTrue(QApp.AreEqual(q1, q2));
        }

        [TestMethod]
        public void testQuantityMeasurementApp_SimplifiedDemonstration_Conversion()
        {
            var q = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var conv = QApp.Convert(q, LengthUnit.Inch);
            Assert.AreEqual(12.0, conv.Value, 1e-9);
        }

        [TestMethod]
        public void testQuantityMeasurementApp_SimplifiedDemonstration_Addition()
        {
            var q1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inch);
            var sum = QApp.Add(q1, q2);
            Assert.AreEqual(2.0, sum.Value, 1e-9);
        }

        [TestMethod]
        public void testGenericQuantity_ConstructorValidation_NullUnit()
        {
            bool thrown = false;
            try
            {
                var q = new Quantity<LengthUnit>(1.0, (LengthUnit)999);
            }
            catch (ArgumentOutOfRangeException)
            {
                thrown = true;
            }
            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public void testGenericQuantity_ConstructorValidation_InvalidValue()
        {
            bool thrown = false;
            try
            {
                var q = new Quantity<LengthUnit>(double.NaN, LengthUnit.Feet);
            }
            catch (ArgumentException)
            {
                thrown = true;
            }
            Assert.IsTrue(thrown);
        }
    }
}

