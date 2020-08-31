using FlashScore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Tennis_FlashScore.Extensions;

namespace Tennis_FlashScore.Models
{
    public class TelegramBotManager
    {
        public TelegramBotClient Bot { get; set; }
        public List<BotSubscriber> subscribers { get; set; } = new List<BotSubscriber>();

        public TelegramBotManager(string token)
        {
            Bot = new TelegramBotClient(token);
            Bot.OnMessage += OnRecieve;
            Bot.StartReceiving();
            var x = Bot.GetMeAsync().Result;
        }
        public async Task SendToSubs(Match match,Settings AppSettings)
        {
            if (match.Posted)
            {
                return;
            }
            else
            {
                foreach (var sub in subscribers.Where(s=> !s.Stopped))
                {
                    await Bot.SendTextMessageAsync(sub.ChatId, match.ToMessage(AppSettings.UseLink, AppSettings.H2HCountCheck, AppSettings.TotalToCheck));
                    await Task.Delay(100);
                }
                match.Posted = true;
            }
        }
        public async void OnRecieve(object sender,MessageEventArgs e)
        {
            if(e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                Enums.Command c = e.Message.Text.ToCommand();
                switch (c)
                {
                    case Enums.Command.start:
                        {
                            if(subscribers.Select(s => s.ChatId).Contains(e.Message.Chat.Id))
                            {
                                if (subscribers.Where(s => s.ChatId == e.Message.Chat.Id).First().Stopped == false)
                                {
                                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Бот уже запущен!");
                                }
                                else if(subscribers.Where(s => s.ChatId == e.Message.Chat.Id).First().Stopped)
                                {
                                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Ты включил рассылку сигнала! Для остановки напиши /stop");
                                    subscribers.Where(s => s.ChatId == e.Message.Chat.Id).First().Stopped = false;
                                }
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, $"Приветсвую, {e.Message.From.FirstName}. Ты включил рассылку сигнала! Для остановки напиши /stop") ;
                                subscribers.Add(new BotSubscriber() { ChatId = e.Message.Chat.Id, Stopped = false, UserName = e.Message.From.FirstName });
                                await SaveSubs();
                            }
                            break;
                        }
                    case Enums.Command.stop:
                        {
                            if (subscribers.Select(s => s.ChatId).Contains(e.Message.Chat.Id))
                            {
                                if (subscribers.Where(s => s.ChatId == e.Message.Chat.Id).First().Stopped)
                                {
                                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Бот уже остановлен!");
                                }
                                else if (!subscribers.Where(s => s.ChatId == e.Message.Chat.Id).First().Stopped)
                                {
                                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Ты выключил рассылку сигнала! Для остановки напиши /start");
                                    subscribers.Where(s => s.ChatId == e.Message.Chat.Id).First().Stopped = true;
                                }
                            }
                        }
                        break;
                    case Enums.Command.unknown:
                        {
                            if (!subscribers.Select(s => s.ChatId).Contains(e.Message.Chat.Id))
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Неизвестная команда!");
                                subscribers.Add(new BotSubscriber() { ChatId = e.Message.Chat.Id, Stopped = true, UserName = e.Message.From.FirstName });
                                await SaveSubs();
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Неизвестная команда!");
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public async Task SaveSubs()
        {
            using (StreamWriter sw = new StreamWriter("subs.json", false, Encoding.UTF8))
            {
                var json = JsonConvert.SerializeObject(subscribers);
                await sw.WriteAsync(json);
            }
        }
        public async Task LoadSubs()
        {
            if(File.Exists("subs.json"))
            {
                using (StreamReader sr = new StreamReader("subs.json", Encoding.UTF8))
                {
                    var subs = JsonConvert.DeserializeObject<List<BotSubscriber>>( await sr.ReadToEndAsync());
                    if(subs!=null)
                    {
                        subscribers = subs;
                    }
                }
            }
        }
    }
}
