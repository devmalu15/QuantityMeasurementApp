using QuantityMeasurementApp.ConsoleApp.Models;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    public sealed class Inches
    {
        private readonly QuantityLength _inner;
        public Inches(double value) => _inner = new QuantityLength(value, LengthUnit.Inch);
        public double Value => _inner.Value;
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null || GetType() != obj.GetType()) return false;
            Inches other = (Inches)obj;
            return _inner.Equals(other._inner);
        }
        public override int GetHashCode() => _inner.GetHashCode();
    }
}
