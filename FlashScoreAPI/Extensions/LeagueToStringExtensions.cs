using FlashScore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashScoreAPI.Extensions
{
    /// <summary>
    /// Класс расширения для Leagues.
    /// </summary>
    public static class LeagueToStringExtensions
    {
        /// <summary>
        /// Преобразует объект Enum, в строковое представление.
        /// </summary>
        /// <param name="l">Объект лиги.</param>
        /// <returns>Строковое представление.</returns>
        public static string ToTextString(this Leagues l)
        {
            switch (l)
            {
                case Leagues.ProLeagueMen:
                    return "\"Лига ПРО\"";
                case Leagues.TTCupMen:
                    return "\"TT CUP\"";
                case Leagues.WinCupMen:
                    return "\"Win CUP\"";
                case Leagues.SetkaCupMen:
                    return "\"Setka Cup\"";
                default: 
                    return string.Empty;
            }
        }
    }
}
