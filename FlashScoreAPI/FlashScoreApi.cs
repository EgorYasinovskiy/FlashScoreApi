using FlashScore.Additions;
using FlashScore.Enums;
using FlashScore.Extensions;
using FlashScore.Models;
using FlashScoreAPI.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using System.Linq;

namespace FlashScore
{
    /// <summary>
    /// Класс для получения информации о матчах FlashScore.
    /// </summary>
    public class FlashScoreApi
    {
        /// <summary>
        /// Веб-Драйвер для парсинга.
        /// </summary>
        public IWebDriver driver { get; set; }
        /// <summary>
        /// Асинхронный метод получения матчей по одной лиге.
        /// </summary>
        /// <param name="league">Одна из разрешенных лиг.</param>
        /// <returns>Список матчей указанной лиги.</returns>
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public async Task<List<Match>> GetMatchesOfLeague(Leagues league, DateTime DayOfMatch,int minH2H)
        {
            logger.Trace($"Начинаю парсинг матчей лиги {league.ToTextString()}.");
            List<Match> matches = new List<Match>();
            driver.Navigate().GoToUrl(league.ToLink());
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30))
            {
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div.event__match")));
            }
            catch (Exception e)
            {
                logger.Error($"Матчи в лиге {league} не найдены.\n{e.Message}");
                return matches;
            }
            try
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(500)
                };
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id=\"onetrust-accept-btn-handler\"]")));
                driver.FindElement(By.XPath("//*[@id=\"onetrust-accept-btn-handler\"]")).Click();
            }
            catch
            {

            }
            foreach (var match in driver.FindElements(By.CssSelector("div.event__match")))
            {
               
                var timeblock = match.FindElements(By.CssSelector("div.event__time"));
                if(timeblock.Count == 0)
                {
                    continue;
                }
                var additions = timeblock[0].FindElements(By.CssSelector("div.event__stage--pkv"));
                if( additions.Count != 0)
                {
                    continue;
                }
                else if(timeblock[0].Text.Contains("Отменен") || timeblock[0].Text.Contains("ТКР"))
                {
                    continue;
                }    
                string strDate = $"{DateTime.Now.Year}.{timeblock[0].Text}".ToCorrectDataFormat();

                try
                {
                    if (!DateTime.ParseExact(strDate, "yyyy.dd.MM HH:mm", CultureInfo.InvariantCulture).IfDateSame(DayOfMatch))
                    {
                        continue;
                    }
                }
                catch
                {
                    continue;
                }
                var id = match.GetAttribute("id").Replace("g_25_", "") + "/#h2h";
                matches.Add(new Match()
                {
                    Link = "https://www.flashscore.ru/match/" + id
                });
            }
            logger.Trace($"Найдено матчей для парсинга {matches.Count}.");
            Console.Write("\rМатчей проверено 0 / {0} .", matches.Count);
            var mainWindow = driver.CurrentWindowHandle;
            for (int i=0;i<matches.Count;i++)
            {
                try
                {
                    await Parser.FillInfoAboutMatchAsync(matches[i], driver,minH2H);
                    Console.Write("\rМатчей проверено {0} / {1} .", i+1, matches.Count);
                }
                catch(Exception e)
                {
                    foreach(var h in driver.WindowHandles.Where(h=>!h.Equals(mainWindow)))
                    {
                        driver.SwitchTo().Window(h);
                        driver.Close();
                    }
                    driver.SwitchTo().Window(mainWindow);
                    logger.Error($"Ошибка проверки матча по ссылке {matches[i].Link}.\n {e.Message}");
                }
            }
            Console.Write("\n");
            logger.Trace($"Собрана информация о {matches.Count}-x матчах лиги {league.ToTextString()}.");
            return matches;
        }
        /// <summary>
        /// Асинхронный метод получения матчей из нескольких лиг через флаг.
        /// </summary>
        /// <param name="leagues">Перечисление лиг в виде флагов.</param>
        /// <returns>Список матчей указанных лиг.</returns>
        public async Task<List<Match>> GetMatchesOfSomeLeagues(Leagues leagues, DateTime minStartTime, int minH2H)
        {
            List<Match> AllMatches = new List<Match>();
            if (leagues.HasFlag(Leagues.ProLeagueMen))
            { 
                AllMatches.AddRange(await GetMatchesOfLeague(Leagues.ProLeagueMen, minStartTime,minH2H));
            }
            if (leagues.HasFlag(Leagues.TTCupMen))
            {
                AllMatches.AddRange(await GetMatchesOfLeague(Leagues.TTCupMen, minStartTime,minH2H));
            }
            if (leagues.HasFlag(Leagues.WinCupMen))
            {
                AllMatches.AddRange(await GetMatchesOfLeague(Leagues.WinCupMen, minStartTime,minH2H));
            }
            if(leagues.HasFlag(Leagues.SetkaCupMen))
            {
                AllMatches.AddRange(await GetMatchesOfLeague(Leagues.SetkaCupMen, minStartTime, minH2H));
            }    
            return AllMatches;
        }
    }
}
