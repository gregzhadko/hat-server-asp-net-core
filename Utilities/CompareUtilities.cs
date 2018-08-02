using System;

namespace Utilities
{
    public static class CompareUtilities
    {
        public static bool Equals(this DateTime? x, DateTime? y)
        {
            if (x == null)
            {
                return y == null;
            }

            if (y == null)
            {
                return false;
            }

            return x.Value == y.Value;
        }

        public static bool Equals(this double? x, double? y, double tolerance = 0.01)
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