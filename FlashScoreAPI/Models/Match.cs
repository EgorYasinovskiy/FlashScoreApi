using System;
using System.Collections.Generic;

namespace FlashScore.Models
{
    /// <summary>
    /// Модель матча, которая вернется после парсинга.
    /// </summary>
    public class Match
    {
        /// <summary>
        /// Имя первого игрока.
        /// </summary>
        public string FirstPlayer { get; set; }
        /// <summary>
        /// Имя второго игрока.
        /// </summary>
        public string SecondPlayer { get; set; }
        /// <summary>
        /// Ссылка на матч.
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Результаты предыдущих матчей.
        /// </summary>
        public List<MatchH2H> H2HMatches { get; set; } = new List<MatchH2H>();
        /// <summary>
        /// Дата и время начала 
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Название лиги.
        /// </summary>
        public string League { get; set; }
        public bool Posted { get; set; } = false;
        public Match() { }
        public Match(string firstPlayer, string secondPlayer, string link, List<MatchH2H> h2HMatches, string league, DateTime startTime)
        {
            FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            Link = link;
            H2HMatches = h2HMatches;
            League = league;
            StartTime = startTime;
        }
    }

}
