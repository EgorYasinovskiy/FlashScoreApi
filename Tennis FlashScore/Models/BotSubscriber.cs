namespace Tennis_FlashScore.Models
{
    public class BotSubscriber
    {
        public long ChatId { get; set; }
        public string UserName { get; set; }
        public bool Stopped { get; set; }
    }
}
