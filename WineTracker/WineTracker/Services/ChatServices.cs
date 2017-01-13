using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WineTracker.Interface;
using WineTracker.Models;
using WineTracker.Models.Messages;

namespace WineTracker.Services
{
    public class ChatServices : ChatServicesBase, IChatServices
    {
        private readonly IDirectLineApiClient _directLineApiClient;

        public ChatServices(IDirectLineApiClient directLineApiClient)
        {
            _directLineApiClient = directLineApiClient;
            // Get BOT Token
            _directLineApiClient.Initialize(App.DirectLineKey);

            //Add First BOT Message Here
            Messages = new ObservableCollection<Event>();

        }

        public ObservableCollection<Event> Messages { get; private set; }
        /// <summary>
        /// Send a public message
        /// </summary>
        public async Task SendMessage(string messageText)
        {
            await Task.Run(() =>
            {
                Messages.Add(new TextMessage
                {
                    AuthorName = "Andrew",
                    Body = messageText,
                  
                    IsAdmin = true,
                    Timestamp = DateTime.Now
                });
            });


        }

        public Task SendImage(byte[] image)
        {
            throw new NotImplementedException("TODO: ChatServices.SendImage");
        }

    }
}
