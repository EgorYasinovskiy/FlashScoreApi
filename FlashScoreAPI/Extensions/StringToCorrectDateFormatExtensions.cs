using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FlashScoreAPI.Extensions
{
    public static class StringToCorrectDateFormatExtensions
    {
        public static string ToCorrectDataFormat(this string str)
        {
            var dateParams = str.Split('.',' ');
            var result = $"{dateParams[0]}.{dateParams[1]}.{dateParams[2]} {dateParams[4]}";
            return result;
        }
    }
}
