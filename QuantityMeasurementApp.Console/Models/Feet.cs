using QuantityMeasurementApp.ConsoleApp.Models;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public sealed class Feet
    {
        private readonly QuantityLength _inner;
        public Feet(double value) => _inner = new QuantityLength(value, LengthUnit.Feet);
        public double Value => _inner.Value;
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || GetType() != obj.GetType()) return false;
            Feet other = (Feet)obj;
            return _inner.Equals(other._inner);
        }
        public override int GetHashCode() => _inner.GetHashCode();
    }
}
