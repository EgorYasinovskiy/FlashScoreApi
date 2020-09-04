using FlashScore.Enums;

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
            string UaFlag = "\U0001F1FA\U0001F1E6";
            string RuFlag = "\U0001F1F8\U0001F1EE";
            switch (l)
            {
                case Leagues.ProLeagueMen:
                    return $"\" {RuFlag} Лига ПРО {RuFlag} \"";
                case Leagues.TTCupMen:
                    return $"\" {UaFlag} TT CUP {UaFlag} \"";
                case Leagues.WinCupMen:
                    return $"\"{UaFlag} Win CUP {UaFlag} \"";
                case Leagues.SetkaCupMen:
                    return $"\"{UaFlag} Setka Cup {UaFlag} \"";
                default: 
                    return string.Empty;
            }
        }
    }
}
