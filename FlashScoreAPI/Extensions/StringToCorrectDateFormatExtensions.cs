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
