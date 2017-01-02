using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineTracker.Models.Messages
{
    public class Conversation
    {
        public string ConversationId { get; set; }
        public string Token { get; set; }
        public string ETag { get; set; }
    }
}
