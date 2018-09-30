using System;

namespace Utilities
{
    public static class DateUtilities
    {
        public static DateTime ToDateTime(this int timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(timestamp);
        }
    }
}