using FlashScore.Enums;
using System.Collections.Generic;

namespace FlashScore.Models
{
    /// <summary>
    /// Результаты очной встречи.
    /// </summary>
    public class MatchH2H
    {
        /// <summary>
        /// Тотал очков в матче.
        /// </summary>
        public int TotalScore;
        /// <summary>
        /// Результат матча.
        /// </summary>
        public H2HResult ResultOfMatch;
        public static bool IsBigger(int count, double total, List<MatchH2H> matches)
        {
            int currrentCount = 0;
            foreach (var match in matches)
            {
                if (match.TotalScore > total)
                {
                    ++currrentCount;
                }
            }
            return currrentCount >= count;
        }
    }
}
