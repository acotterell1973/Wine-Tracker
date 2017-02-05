using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WineTracker.Interface;
using WineTracker.Models;
using WineTracker.Models.DirectLineClient;
using WineTracker.Models.Messages;
using WineTracker.Services.Components;

namespace WineTracker.Services
{
    public class ChatServices : ChatServicesBase, IChatServices
    {
        private readonly IDirectLineApiClient _directLineApiClient;
        private readonly IWineHunterBotConnectorApitClient _wineHunterBotConnectorApitClient;
        private string _conversationId;
        private string _botWaterMark;
        public ChatServices(IDirectLineApiClient directLineApiClient)
        {
            _directLineApiClient = directLineApiClient;
            // Get BOT Token
            _conversationId = _directLineApiClient.Initialize(App.DirectLineKey);
            //Add First BOT Message Here
            Messages = new ObservableCollection<Event>();

        }

        public ObservableCollection<Event> Messages { get; private set; }
        /// <summary>
        /// Send a public message
        /// </summary>
        public async Task SendMessage(string messageText)
        {

            //BOT Conversation

            await _directLineApiClient.SendMessageAsync(_conversationId, App.BotSender.Id, messageText);
            var conversation = await _directLineApiClient.GetMessagesAsync(_conversationId, _botWaterMark);
            Messages.Clear();
            foreach (var message in conversation.Messages)
            {
                TextMessage botMessage;
                if (message.From.Id == "winehunterbot")
                {
                    //UI Message to display
                    botMessage = new TextMessage
                    {
                        AuthorName = App.BotFriend.DisplayName,
                        Body = message.Text,
                        IsAdmin = false,
                        Timestamp = DateTime.Now
                    };
                }
                else
                {
                    //UI Message to display
                    botMessage = new TextMessage
                    {
                        AuthorName = App.BotSender.DisplayName,
                        Body = message.Text,
                        IsAdmin = true,
                        Timestamp = DateTime.Now
                    };
                }

                Messages.Add(botMessage);
            }



        }

        public async Task GetMessages()
        {
            var conversation = await _directLineApiClient.GetMessagesAsync(_conversationId, _botWaterMark);
            Messages.Clear();
            foreach (var message in conversation.Messages)
            {
                TextMessage botMessage;
                if (message.From.Id == "winehunterbot")
                {
                    //UI Message to display
                    botMessage = new TextMessage
                    {
                        AuthorName = App.BotFriend.DisplayName,
                        Body = message.Text,
                        IsAdmin = false,
                        Timestamp = DateTime.Now
                    };
                }
                else
                {
                    //UI Message to display
                    botMessage = new TextMessage
                    {
                        AuthorName = App.BotSender.DisplayName,
                        Body = message.Text,
                        IsAdmin = true,
                        Timestamp = DateTime.Now
                    };
                }

                Messages.Add(botMessage);
            }

        }

        public Task SendImage(byte[] image)
        {
            throw new NotImplementedException("TODO: ChatServices.SendImage");
        }

    }
}
