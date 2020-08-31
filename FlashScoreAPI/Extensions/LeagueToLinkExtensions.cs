using FlashScore.Enums;

namespace FlashScore.Extensions
{
    /// <summary>
    /// Класс расширения для Enums.Leagues
    /// </summary>
    public static class LeagueToLinkExtensions
    {
        /// <summary>
        /// Метод принимает название лиги и возвращает ссылку на страницу предстоящих матчей этой лиги на FlashScore. 
        /// </summary>
        /// <param name="league">Название лиги.</param>
        /// <returns>Ссылка на страницу FlashScore.</returns>
        public static string ToLink(this Leagues league)
        {
            switch (league)
            {
                case Leagues.ProLeagueMen:
                    return "https://www.flashscore.ru/table-tennis/others-men/liga-pro/fixtures/";
                case Leagues.TTCupMen:
                    return "https://www.flashscore.ru/table-tennis/others-men/tt-cup/fixtures/";
                case Leagues.WinCupMen:
                    return "https://www.flashscore.ru/table-tennis/others-men/win-cup/fixtures/";
                default:
                    return null;
            }
        }
    }
}
