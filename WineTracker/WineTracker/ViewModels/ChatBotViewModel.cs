using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WineTracker.Extensions;
using WineTracker.Interface;
using WineTracker.Models;
using WineTracker.Models.DirectLineClient;
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
        private int _messageCount;
        public override async void Init(object initData)
        {
            base.Init(initData);
            _chatServices.OnBotMessageReceived += chatServices_OnBotMessage;
            BotMessageHandler.BotMessageEventReceived += BotMessageHandler_BotMessageEventReceived;
        }

        Dictionary<string,Activity> trackMessages = new Dictionary<string, Activity>(); 
        private void chatServices_OnBotMessage(object sender, EventHandler.OnMessageEventArgs e)
        {
            var messages = e.BotActivityMessage.Messages;
            foreach (Activity activity in messages)
            {
                if (activity.Id != null)
                {
                    var id = activity.Id.Split('|')[1];
                    if (!trackMessages.ContainsKey(id))
                    {
                        if (activity.From.Id == "winehunterbot")
                        {
                            BotMessageHandler.PublishMessage(activity.From.Name, activity.Text);
                        }
                        trackMessages.Add(id, activity);
                    }
                }
            }
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
        }

    }
}
