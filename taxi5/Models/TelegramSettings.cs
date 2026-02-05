using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace taxi4.Models
{
    internal class TelegramSettings
    {
        public string BotToken { get; set; }
        public long ChatId { get; set; }
        public bool Enabled { get; set; }
    }
}
