using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineTracker.Extensions;
using WineTracker.Interface;
using WineTracker.Models;
using WineTracker.Models.Messages;
using WineTracker.Services;
using Xamarin.Forms;

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
        private EventViewModelFactory _eventViewModelFactory;
        private IChatServices _chatServices;
        private const float DefaultChatWindowHeight = 500;
        public override async void Init(object initData)
        {
            base.Init(initData);
            Model = new WineItemInfo();

            BotMessageHandler.BotMessageEventReceived += BotMessageHandler_BotMessageEventReceived;

            await InitiliazeConversation("Andrew Cotterell");
            //Adjust the chat windows when the keyboad is open or close.
            KeyboardHelper.KeyboardChanged += (sender, e) =>
            {
                //Default Height is 500
                
            };
        }

        private async Task InitiliazeConversation(string fullName)
        {
            await _chatServices.SendMessage(fullName);

            _chatServices.Messages.SynchronizeWith(Events, i => _eventViewModelFactory.Get(i, App.BotSender.DisplayName));
            var lastMessage = (TextMessage)_chatServices.Messages.Last();
            BotMessageHandler.PublishMessage(lastMessage.AuthorName, lastMessage.Body);
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
            var lastMessage = (TextMessage)_chatServices.Messages.Last();
            BotMessageHandler.PublishMessage(lastMessage.AuthorName, lastMessage.Body);
        }

    }
}
