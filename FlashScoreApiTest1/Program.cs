using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using FlashScore;
using System.Threading.Tasks;
using FlashScore.Enums;
using System.Diagnostics;
using FlashScore.Models;
using System.Threading;

namespace FlashScoreApiTest1
{
    class Program
    {
        static ChromeDriver driver;
        static FlashScoreApi api;
        public static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        static void Main()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--mute-audio");
            options.AddArgument("--disable-images");
            //options.AddArgument("--headless");
            //options.AddArgument("-headless");
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            driver = new ChromeDriver(service, options);
            api = new FlashScoreApi
            {
                driver = driver
            };
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("\n\n################# Проверка номер {0} #################\n\n", i+1);
                try
                {
                    StartTest().Wait();
                    Thread.Sleep(1000 * 5);
                    //Console.ReadLine();
                    //Console.ReadLine();
                    // driver.Quit();
                 
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    //driver.Quit();
                }
            }
            Console.ReadLine();
        }

        public async static Task StartTest()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Match> matches = new List<Match>();
            await Task.Run(async () => 
            {
                matches = await api.GetMatchesOfSomeLeagues(Leagues.ProLeagueMen | Leagues.TTCupMen | Leagues.WinCupMen,DateTime.Now,4);
            });
            stopwatch.Stop();
            Console.WriteLine("Выполнение парсинга заняло " + stopwatch.Elapsed.ToString(@"mm\:ss" ));
            Console.WriteLine($"Всего получено информации о {matches.Count} матчах. Из низ информацию о очных имеют: {matches.Where(m=>m.H2HMatches!=null).Count()}.");
        }
    }
}
