using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_FlashScore.Models
{
    public class Settings
    {
        public int MinH2HCount { get; set; }
        public int H2HCountCheck { get; set; }
        public double TotalToCheck { get; set; }
        public string ApiTelegramTokenKey { get; set; }
        public bool ProLeague { get; set; }
        public bool TTCup { get; set; }
        public bool WinCup { get; set; }
        public bool SetkaCup { get; set; }
        public bool UseLink { get; set; }
        public int MinutesBeforePost { get; set; }
        public TimeSpan StartPosting { get; set; }
        public TimeSpan EndPosting { get; set; }   
       
        public static async Task Save(Settings settings)
        {
            using(StreamWriter sw = new StreamWriter("settings.json",false,Encoding.UTF8))
            {
                var json = JsonConvert.SerializeObject(settings);
                await sw.WriteAsync(json);
            }
        }
        public static async Task<Settings> Load()
        {
            if (File.Exists("settings.json"))
            {
                using (StreamReader sr = new StreamReader("settings.json", Encoding.UTF8))
                {
                    Settings res = JsonConvert.DeserializeObject<Settings>(await sr.ReadToEndAsync());
                    return res;
                }
            }
            return new Settings(5, 4, 73.5, "#PASTE_API_TOKEN_HERE#", true, true, false,true, false, 10, TimeSpan.FromHours(7), TimeSpan.FromHours(22));
        }
        public Settings()
        { 
        }

        public Settings(int minH2HCount, int h2HCountCheck, double totalToCheck, string apiTelegramTokenKey, bool proLeague, bool tTCup, bool winCup,bool setkaCup, bool useLink, int minutesBeforePost, TimeSpan startPosting, TimeSpan endPosting)
        {
            MinH2HCount = minH2HCount;
            H2HCountCheck = h2HCountCheck;
            TotalToCheck = totalToCheck;
            ApiTelegramTokenKey = apiTelegramTokenKey;
            ProLeague = proLeague;
            TTCup = tTCup;
            WinCup = winCup;
            SetkaCup = setkaCup;
            UseLink = useLink;
            MinutesBeforePost = minutesBeforePost;
            StartPosting = startPosting;
            EndPosting = endPosting;
        }
    }
}
