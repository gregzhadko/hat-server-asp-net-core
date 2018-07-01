using System;

namespace Utilities
{
    public static class CompareUtilities
    {
        public static bool NullableDoubleCompare(double? x, double? y, double tolerance = 0.01)
        {
            if (x == null)
            {
                return y == null;
            }

            if (y == null)
            {
                return false;
            }

            return Math.Abs(x.Value - y.Value) < tolerance;
        }
    }
}