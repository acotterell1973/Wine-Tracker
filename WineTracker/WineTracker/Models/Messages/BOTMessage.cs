using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineTracker.Models.Messages
{
    public class BotMessage
    {
        public string Id { get; set; }
        public string ConversationId { get; set; }
        public DateTime Created { get; set; }
        public string From { get; set; }
        public string Text { get; set; }
        public string ChannelData { get; set; }
        public string[] Images { get; set; }
        public Attachment[] Attachments { get; set; }
        public string ETag { get; set; }
    }
}
