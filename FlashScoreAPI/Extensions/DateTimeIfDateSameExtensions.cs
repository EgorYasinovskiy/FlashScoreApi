using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashScoreAPI.Extensions
{
    public static class DateTimeIfDateSameExtensions
    {
        public static bool IfDateSame(this DateTime dateTime, DateTime toCheck)
        {
            return dateTime.ToShortDateString().Equals(toCheck.ToShortDateString());
        }
    }
}
