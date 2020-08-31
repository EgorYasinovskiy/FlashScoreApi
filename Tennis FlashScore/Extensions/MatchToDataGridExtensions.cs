using FlashScore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_FlashScore.Extensions
{
    public static class MatchToDataGridExtensions
    {
        public static string[] ToDataGridRow(this Match match)
        {
            return new string[] { match.League, match.FirstPlayer, match.SecondPlayer, match.StartTime.ToString("yyyy.MM.dd HH:mm") };
        }
    }
}
