using System;

namespace FlashScore.Extensions
{

    // <summary>
    /// Класс расширения строки.
    /// </summary>
    public static class StringIsNumverExtensions
    {
        // <summary>
        /// Метод, определяющий является ли строко последовательностью цифр.
        /// </summary>
        /// <param name="str">Входная строка.</param>
        /// <returns>Является ли строка числом или нет.</returns>
        public static bool IsNumer(this String str)
        {
            foreach (var c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
