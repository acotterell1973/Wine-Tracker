using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WineTracker.Extensions;
using WineTracker.Interface;
using WineTracker.Models;
using WineTracker.Models.Messages;
using WineTracker.Services;

namespace WineTracker.ViewModels
{
    public class ChatBotViewModel : BaseViewModel<WineItemInfo>
    {
        public ChatBotViewModel(IChatServices chatServices)
        {
            Events = new ObservableCollection<EventViewModel>();
            _eventViewModelFactory = new EventViewModelFactory();
            _chatServices = chatServices;
        }

        private readonly EventViewModelFactory _eventViewModelFactory;
        private readonly IChatServices _chatServices;
        private const float DefaultChatWindowHeight = 500;
        public override async void Init(object initData)
        {
            base.Init(initData);
            BotMessageHandler.BotMessageEventReceived += BotMessageHandler_BotMessageEventReceived;
        }

        #region property
        public ObservableCollection<EventViewModel> Events { get; set; }
        
        public float ChatLayoutHeight { get; set; } = DefaultChatWindowHeight;
        #endregion
        private async void BotMessageHandler_BotMessageEventReceived(object sender, EventHandler.BotMessageEventEventArgs e)
        {
           
            if (string.IsNullOrEmpty(e.Message))
                return;
            var text = e.Message;
           
            await _chatServices.SendMessage(text);

            _chatServices.Messages.SynchronizeWith(Events, i => _eventViewModelFactory.Get(i, App.BotSender.DisplayName));

            string botid = string.Empty;
            Queue<TextMessage> messageQueue = new Queue<TextMessage>();
            //find the last set of messages sent by the bot
            foreach (var source in _chatServices.Messages.Reverse())
            {
                var textmessage = (TextMessage) source;
                if (botid == "") botid = textmessage.AuthorName;

                if (botid != textmessage.AuthorName) break;
                messageQueue.Enqueue(textmessage);
            }
            foreach (var textMessage in messageQueue.ToList())
            {
                var botMessage = messageQueue.Dequeue();
                BotMessageHandler.PublishMessage(botMessage.AuthorName, botMessage.Body);
            }
            
        }

    }
}
