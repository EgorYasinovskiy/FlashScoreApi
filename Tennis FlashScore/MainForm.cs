using FlashScore;
using FlashScore.Enums;
using FlashScore.Models;
using FlashScoreAPI.Extensions;
using NLog;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tennis_FlashScore.Extensions;
using Tennis_FlashScore.Models;

namespace Tennis_FlashScore
{
    public partial class MainForm : Form
    {
        public Settings AppSettings { get; set; } = new Settings();
        private bool IsWorking { get; set; } = false;
        FlashScoreApi api = new FlashScoreApi();
        ChromeDriver driver = null;
        Logger logger = LogManager.GetCurrentClassLogger();
        private TelegramBotManager telegram = null;
        public MainForm()
        {
            InitializeComponent();
            startButton.Click += Start;
            stopButton.Click += Stop;
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            nudTimeBeforePost.ValueChanged += TimeChanged;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            stopButton.Enabled = false;
        }
        private void TimeChanged(object sender, EventArgs e)
        {
            if(AppSettings!=null)
            {
                AppSettings.MinutesBeforePost = (int)nudTimeBeforePost.Value;
            }
        }
        private void Stop(object sender, EventArgs e)
        {
            stopButton.Enabled = false;
            startButton.Enabled = true;
            IsWorking = false;
            if(telegram!=null && telegram.Bot!=null)
            {
                telegram.Bot.StopReceiving();
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            var set = await Settings.Load();
            if (set != null)
            {
                AppSettings = set;
            }
            nudTimeBeforePost.BeginInvoke(new MethodInvoker(delegate
            {
                nudTimeBeforePost.Value = AppSettings.MinutesBeforePost;
            }));
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (telegram != null)
            {
                await telegram.SaveSubs();
            }
            await Settings.Save(AppSettings);
        }

        private async void Start(object sender, EventArgs e)
        {
            await Task.Run(async() =>
            {
                Invoke(new MethodInvoker(delegate
                {
                    startButton.Enabled = false;
                    stopButton.Enabled = true;
                }));
                IsWorking = true;
                telegram = new TelegramBotManager(AppSettings.ApiTelegramTokenKey);
                await telegram.LoadSubs();
                InitDriver();
                bool flag = true;
                DateTime lastParse = DateTime.Now;
                Leagues l = 0x0000;
                if (AppSettings.ProLeague)
                {
                    l |= Leagues.ProLeagueMen;
                }
                if (AppSettings.TTCup)
                {
                    l |= Leagues.TTCupMen;
                }
                if (AppSettings.WinCup)
                {
                    l |= Leagues.WinCupMen;
                }
                List<Match> matches = new List<Match>();
                try
                {
                    flag = false;
                    if (DateTime.Now.TimeOfDay < TimeSpan.FromHours(5))
                    {
                        matches = await api.GetMatchesOfSomeLeagues(l, DateTime.Now, AppSettings.MinH2HCount);
                    }
                    else
                    {
                        matches = await api.GetMatchesOfSomeLeagues(l, DateTime.Now.AddDays(1), AppSettings.MinH2HCount);
                    }
                }
                catch (Exception exception)
                {
                    logger.Error($"Ошибка - {exception.Message} при получении списка матчей. Повтор c в 3 часа ночи.");
                }
                matches = matches.OrderBy(m => m.StartTime).ToList();
                FeelDataGrid(matches);
                while (IsWorking)
                {
                    bool error = false;
                    if (flag && DateTime.Now.TimeOfDay >= TimeSpan.FromHours(3) && DateTime.Now.TimeOfDay <= TimeSpan.FromHours(5))
                    {
                        try
                        {
                            flag = false;
                            matches = await api.GetMatchesOfSomeLeagues(l, DateTime.Now, AppSettings.MinH2HCount);
                        }
                        catch (Exception exception)
                        {
                            error = true;
                            logger.Error($"Ошибка - {exception.Message} при получении списка матчей. Повтор c перезапуском хрома.");
                            if (!IsWorking)
                            {
                                return;
                            }
                        }
                        matches = matches.Where(m => m.H2HMatches.Count >= AppSettings.H2HCountCheck).OrderBy(m => m.StartTime).ToList();
                        FeelDataGrid(matches);
                    }
                    if (error)
                    {
                        if (!IsWorking)
                        {
                            return;
                        }
                        driver.Quit();
                        InitDriver();
                        flag = true;
                        continue;
                    }
                    for (int i = 0; i < matches.Count; i++)
                    {
                        if(matches[i].StartTime == DateTime.MinValue)
                        {
                            continue;
                        }
                        var startTime = matches[i].StartTime.ToUnixTime();
                        startTime -= 60 * AppSettings.MinutesBeforePost;
                        var startTimeDt = startTime.ToDateTime();
                        if (startTimeDt <= DateTime.Now && startTimeDt.TimeOfDay > AppSettings.StartPosting && startTimeDt.TimeOfDay < AppSettings.EndPosting)
                        {
                            await telegram.SendToSubs(matches[i],AppSettings);
                        }
                        if(matches[i].Posted)
                        {
                            matches.RemoveAt(i);
                        }
                    }
                    if (DateTime.Now - lastParse >= TimeSpan.FromHours(12))
                    {
                        flag = true;
                    }
                    FeelDataGrid(matches);
                    Thread.Sleep(60 * 1000);
                   
                }
            });
        }
        private void FeelDataGrid(List<Match> matches)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                mathesDataGrid.Rows.Clear();
                for(int i=0;i<matches.Count; i++)
                {
                    mathesDataGrid.Rows.Add(matches[i].ToDataGridRow());
                }
            }));
        }
        private void InitDriver()
        {
            if (driver == null)
            {
                var service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;
                driver = new ChromeDriver(service);
                api.driver = driver;
            }
        }
    }
}
