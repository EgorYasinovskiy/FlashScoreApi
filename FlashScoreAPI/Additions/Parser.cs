using FlashScore.Extensions;
using FlashScore.Models;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace FlashScore.Additions
{
    /// <summary>
    /// Вспомогательный статический класс для преобразования html кода в объекты классов.
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Асинхронный метод заполнения и парсинга информации о матче.
        /// </summary>
        /// <param name="m"> Матч, информацию о котором нужно получить.</param>
        /// <param name="driver"> Веб-Драйвер.</param>
        /// <returns></returns>
        public static async Task FillInfoAboutMatchAsync(Match m, IWebDriver driver,int minH2H)
        { 
            driver.Navigate().GoToUrl(m.Link);
            var mainWindow = driver.CurrentWindowHandle;
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30))
            {
                PollingInterval = TimeSpan.FromMilliseconds(200)
            };
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div.description___37C6i_4 div")));
            
            var names = driver.FindElements(By.CssSelector("div.participantName___1pLLLzn a"));
            string firstPlayer = string.Empty;
            string secondPlayer = string.Empty;
            if (names.Count == 2)
            {
                firstPlayer = names[0].Text;
                secondPlayer = names[1].Text;
            }
            else
            {
                throw new Exception("Ошибка при парсинге имен. Блоки с именами не были найдены.\n");
            }
            var timeDivs =driver.FindElements(By.CssSelector("div.description___37C6i_4 div"));
            DateTime startTime = new DateTime();
            if (timeDivs.Count >= 2)
            {
                startTime = DateTime.ParseExact(timeDivs[1].Text, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            }
            else
            {
                throw new Exception("Ошибка при парсинге даты матча. Блок не был найден.\n");
            }
            string league = string.Empty; 
            var leagueDiv = driver.FindElements(By.CssSelector("span.country___2ELXQSx a"));
            if (leagueDiv.Count > 0)
            {
                league = leagueDiv[0].Text;
            }
            else
            {
                throw new Exception("Ошибка при парсинге лиги. Блок не был найден.\n");
            }
            var h2hMathes = driver.FindElements(By.XPath("//*[@id=\"detail\"]/div[6]/div[3]/div[2]/div"));
            if (h2hMathes.Count < minH2H)
            {
                m.FirstPlayer = firstPlayer;
                m.SecondPlayer = secondPlayer;
                m.League = league;
                m.StartTime = startTime;
            }
            else
            {
                Actions actions = new Actions(driver);
                actions.MoveToElement(h2hMathes.Last());
                actions.Perform();
                m.FirstPlayer = firstPlayer;
                m.SecondPlayer = secondPlayer;
                m.League = league;
                m.StartTime = startTime;
                m.H2HMatches = await GetH2HInfoAsync(h2hMathes, driver,mainWindow);
            }
         
        }
        /// <summary>
        /// Метод парсинга H2H матчей.
        /// Считывает тотал и результат матча.
        /// </summary>
        /// <param name="elements">Блоки H2H матчей на FlashScore.</param>
        /// <param name="driver">Веб-Драйвер.</param>
        /// <returns>Список матчей H2H.</returns>
        public static List<MatchH2H> GetH2HInfo(ReadOnlyCollection<IWebElement> elements, IWebDriver driver,string mainWindow)
        {
            List<MatchH2H> matchesH2h = new List<MatchH2H>();
            foreach (var element in elements)
            {
                MatchH2H matchH2H = new MatchH2H() { TotalScore = 0, ResultOfMatch = Enums.H2HResult.None };
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(200)
                };
                element.Click();
                var h2hWindow = driver.WindowHandles.Where(m => !m.Equals(mainWindow)).First();
                driver.SwitchTo().Window(h2hWindow);
                try
                {
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector("div#detail > div.summary___Cbl3b9P")));
                }
                catch(TimeoutException)
                {
                    foreach (var handle in driver.WindowHandles.Where(m => !m.Equals(mainWindow)))
                    {
                        driver.SwitchTo().Window(handle);
                        driver.Close();
                    }
                    driver.SwitchTo().Window(mainWindow);
                    return matchesH2h;
                }
                var scoreBlocks = driver.FindElements(By.CssSelector("div#detail div.part___1Pd43ek")).Where(m => !m.GetAttribute("class").Contains("part--current"));
                foreach (var scoreBlock in scoreBlocks)
                {
                    if (!string.IsNullOrEmpty(scoreBlock.Text)
                        && scoreBlock.Text.IsNumer())
                    {
                        matchH2H.TotalScore += Int32.Parse(scoreBlock.Text);
                    }
                }
                if (matchH2H.TotalScore != 0)
                {
                    matchH2H.ResultOfMatch = Enums.H2HResult.Fineshed;
                }
                matchesH2h.Add(matchH2H);
                driver.Close();
                driver.SwitchTo().Window(mainWindow);
            }
            foreach(var handle in driver.WindowHandles.Where(m=>!m.Equals(mainWindow)))
            {
                driver.SwitchTo().Window(handle);
                driver.Close();
            }
            driver.SwitchTo().Window(mainWindow);
            return matchesH2h;
        }
        /// <summary>
        /// Асинхронная реализация парсинга матчей H2H.
        /// </summary>
        /// <param name="elements">Блоки H2H матчей на FlashScore.</param>
        /// <param name="driver">Веб-Драйвер.</param>
        /// <returns>Список матчей H2H.</returns>
        public static async Task<List<MatchH2H>> GetH2HInfoAsync(ReadOnlyCollection<IWebElement> elements, IWebDriver driver,string mainWindow)
        {
            return await Task.Run(() => GetH2HInfo(elements, driver,mainWindow));
        }
    }
}
