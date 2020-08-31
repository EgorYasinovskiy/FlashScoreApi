    using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_FlashScore.Extensions
{
    public static class DateTimeUnixExtensions
    {
        public static long ToUnixTime(this DateTime dt)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(dt - sTime).TotalSeconds;
        }
        public static DateTime ToDateTime(this long unixtime)
        {
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(unixtime);
        }
    }
}
