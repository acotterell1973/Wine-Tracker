using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WineTracker.EventHandler;
using WineTracker.Interface;
using WineTracker.Models.Messages;

namespace WineTracker.Services
{
    public class ChatServices : ChatServicesBase, IChatServices
    {
        private readonly IDirectLineApiClient _directLineApiClient;
        #region properties
        public ObservableCollection<Event> Messages { get; }
        #endregion

        public ChatServices(IDirectLineApiClient directLineApiClient)
        {
            //Message container
            Messages = new ObservableCollection<Event>();

            _directLineApiClient = directLineApiClient;

            //Listens for messages received from the bot
            _directLineApiClient.OnBotMessage += directLineApiClient_OnBotMessage;

            // Get BOT Token
            var conversationId = _directLineApiClient.Initialize(App.DirectLineKey);
            if (conversationId == "NOT_CONNECTED" || conversationId == string.Empty)
            {
                //UI Message to display
                var botMessage = new TextMessage
                {
                    AuthorName = App.BotSender.DisplayName,
                    Body = ":( ... I'm sorry, I'm not able to start a conversation with the assistance at this time. Please try again later.",
                    IsAdmin = true,
                    Timestamp = DateTime.Now
                };
                Messages.Add(botMessage);
            }

        }


        private void directLineApiClient_OnBotMessage(object sender, EventHandler.OnMessageEventArgs e)
        {
            if (e?.BotActivityMessage == null) return;
            Messages.Clear();
            //foreach (var message in e.BotActivityMessage.Messages)
            //{
            //    TextMessage botMessage;
            //    if (message.From.Id == "winehunterbot")
            //    {
            //        //UI Message to display
            //        botMessage = new TextMessage
            //        {
            //            AuthorName = App.BotFriend.DisplayName,
            //            Body = message.Text,
            //            IsAdmin = false,
            //            Timestamp = DateTime.Now
            //        };
            //    }
            //    else
            //    {
            //        //UI Message to display
            //        botMessage = new TextMessage
            //        {
            //            AuthorName = App.BotSender.DisplayName,
            //            Body = message.Text,
            //            IsAdmin = true,
            //            Timestamp = DateTime.Now
            //        };
            //    }

            //    Messages.Add(botMessage);
            //}
            
            OnBotMessageReceived?.Invoke(this, new OnMessageEventArgs(e.BotActivityMessage));
        }

        #region public methods
        /// <summary>
        /// Send a public message
        /// </summary>
        public async Task SendMessage(string messageText)
        {
            //BOT Conversation
            await _directLineApiClient.SendMessageAsync(App.BotSender.Id, messageText);
        }

        public event EventHandler<OnMessageEventArgs> OnBotMessageReceived;

        public Task SendImage(byte[] image)
        {
            throw new NotImplementedException("TODO: ChatServices.SendImage");
        }
        #endregion
    }
}
