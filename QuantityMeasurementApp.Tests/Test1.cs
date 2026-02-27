using Microsoft.VisualStudio.TestTools.UnitTesting;
// alias the class to avoid confusion with namespace
using QApp = QuantityMeasurementApp.ConsoleApp.QuantityMeasurementApp;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityMeasurementAppTest
{
        [TestMethod]
        public void testEquality_SameValue()
        {
            // given two identical feet measurements
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
            var f = new QApp.Feet(5.0);
            Assert.IsTrue(f.Equals(f), "Object should be equal to itself (reflexive)");
        }

        [TestMethod]
        public void testEquality_NullComparison()
        {
            var f = new QApp.Feet(3.0);
            Assert.IsFalse(f.Equals(null), "Comparison with null should return false");
        }

        [TestMethod]
        public void testEquality_NonNumericInput()
        {
            // since the class only accepts doubles, comparing to a non-Feet object should be false
            var f = new QApp.Feet(2.0);
            Assert.IsFalse(f.Equals("not a feet"), "Comparison against different type should return false");
        }
    }
}
