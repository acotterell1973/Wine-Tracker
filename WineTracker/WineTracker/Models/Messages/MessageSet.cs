using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineTracker.Models.Messages
{
    public class MessageSet
    {

        public BotMessage[] Messages { get; set; }
        public string Watermark { get; set; }
        public string ETag { get; set; }
    }
}
