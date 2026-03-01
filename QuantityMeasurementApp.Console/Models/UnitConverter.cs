using System;
using System.Linq;
using System.Reflection;

namespace QuantityMeasurementApp.ConsoleApp.Models
{
    // utility that locates static conversion methods for arbitrary enum unit types
    internal static class UnitConverter<U> where U : struct, Enum
    {
        private static readonly Func<U, double, double> _toBase;
        private static readonly Func<U, double, double> _fromBase;

        static UnitConverter()
        {
            Type uType = typeof(U);
            _toBase = CreateDelegate("ConvertToBaseUnit", uType);
            _fromBase = CreateDelegate("ConvertFromBaseUnit", uType);
        }

        private static Func<U, double, double> CreateDelegate(string methodName, Type uType)
        {
            // search all types in assembly for a static method matching signature
            var asm = uType.Assembly;
            foreach (Type t in asm.GetTypes())
            {
                MethodInfo mi = t.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    .FirstOrDefault(m => m.Name == methodName);
                if (mi != null)
                {
                    ParameterInfo[] pars = mi.GetParameters();
                    if (pars.Length == 2 && pars[0].ParameterType == uType && pars[1].ParameterType == typeof(double))
                    {
                        return (Func<U, double, double>)Delegate.CreateDelegate(typeof(Func<U, double, double>), mi);
                    }
                }
            }
            throw new InvalidOperationException($"No conversion method '{methodName}' found for unit type {uType.Name}");
        }

        public static double ToBase(U unit, double value) => _toBase(unit, value);
        public static double FromBase(U unit, double value) => _fromBase(unit, value);
    }
}