using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
            return new Settings(4, 4, 73.5, "key", true, true, true, false, 60, TimeSpan.FromHours(7), TimeSpan.FromHours(22));
        }
        public Settings()
        { 
        }

        public Settings(int minH2HCount, int h2HCountCheck, double totalToCheck, string apiTelegramTokenKey, bool proLeague, bool tTCup, bool winCup, bool useLink, int minutesBeforePost, TimeSpan startPosting, TimeSpan endPosting)
        {
            MinH2HCount = minH2HCount;
            H2HCountCheck = h2HCountCheck;
            TotalToCheck = totalToCheck;
            ApiTelegramTokenKey = apiTelegramTokenKey;
            ProLeague = proLeague;
            TTCup = tTCup;
            WinCup = winCup;
            UseLink = useLink;
            MinutesBeforePost = minutesBeforePost;
            StartPosting = startPosting;
            EndPosting = endPosting;
        }
    }
}
