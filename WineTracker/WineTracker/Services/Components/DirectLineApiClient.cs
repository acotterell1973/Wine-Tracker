﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WineTracker.Interface;
using WineTracker.Models.DirectLineClient;
using Conversation = WineTracker.Models.DirectLineClient.Conversation;

namespace WineTracker.Services.Components
{

    public class DirectLineApiClient : IDirectLineApiClient
    {

        private string _directLineKey;
        private string _directLineToken;
        private string _conversationId;


        private const string Host = "https://directline.botframework.com";

        private static readonly string ConversationsApi = $"{Host}/v3/directline/conversations";


        public string Initialize(string directLineSecret)
        {
            if (string.IsNullOrEmpty(directLineSecret))
                throw new ArgumentException("Argument is null or empty", nameof(directLineSecret));

            _directLineKey = directLineSecret;
            var tsk = Task.Run(async () =>
              {
                  var token = await GetTokenAsync();
                  _directLineToken = token.Token;

                  var startConversation = await StartConversationAsync();
                  _directLineToken = startConversation.Token;
                  _conversationId = startConversation.ConversationId;

                  //We don't need the secret after we have a token
                  //With the refresh we will use the token
                  _directLineKey = string.Empty;
              });

            tsk.Wait();
            return _conversationId;
        }

        private async Task<AuthenticationResponse> GetTokenAsync()
        {

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{Host}");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _directLineKey);


                var response = await httpClient.PostAsync("/v3/directline/tokens/generate", null);
                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var token = JsonConvert.DeserializeObject<AuthenticationResponse>(content);
                        return token;
                    }
                }
            }
            return null;

        }
        //Refreshes the token 5 minutes for it expires
        private async Task PeriodicRefreshTokenAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                //var authResponse = await GetBotConnectorToken();
                //_botConnectorToken = authResponse.access_token;
                ////wait 5 minutes before the token expires to get a new one.
                //var interval = TimeSpan.FromSeconds(authResponse.expires_in - (5 * 60));
                //await Task.Delay(interval, cancellationToken);
            }
        }
        public async Task RefreshTokenAsync()
        {

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{Host}");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _directLineToken);

                var response = await httpClient.GetStringAsync("/v3/directline/tokens/refresh");
                JsonConvert.DeserializeObject<AuthenticationResponse>(response);
            }

        }

        /// <summary>
        /// Opens a direct conversation with a specific BOT
        /// </summary>
        /// <returns></returns>
        public async Task<StartConversationResponse> StartConversationAsync()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _directLineToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.PostAsync(ConversationsApi, null);

                if (response.IsSuccessStatusCode)
                {
                    var startConversationResponse = JsonConvert.DeserializeObject<StartConversationResponse>(await response.Content.ReadAsStringAsync());
                    _conversationId = startConversationResponse.ConversationId;
                    return startConversationResponse;
                }
                return null;
            }
        }

        public async Task SendMessageAsync(string from, string text)
        {
            await SendMessageAsync(_conversationId, from, text);
        }
        public async Task SendMessageAsync(string conversationId, string from, string text)
        {
            string url = $"{ConversationsApi}/{conversationId}/activities";

            var message = new Message
            {
                Type = "message",
                From = new From { Id = from },
                Text = text
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _directLineToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(url, content);

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        
                    }
                }
            }
        }

        public async Task EndMessageAsync(string conversationId, string from)
        {
            string url = $"{Host}/api/conversations/{conversationId}/activities";

            var message = new Message
            {
                Type = "endOfConversation",
                From = new From { Id = from }
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _directLineToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(url, content);

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    // TODO: response.EnsureSuccessStatusCode();
                }
            }
        }

        /// <summary>
        /// Reconnects to a conversation if the connection is lost
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="watermark"></param>
        /// <returns></returns>
        public async Task<ReconnectMessageResponse> ReconnectAsync(string conversationId, string watermark)
        {
            string url = $"{ConversationsApi}/{conversationId}?watermark={watermark}";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _directLineToken);
                var response = await httpClient.GetStringAsync(url);

                return JsonConvert.DeserializeObject<ReconnectMessageResponse>(response);
            }
        }
        public async Task<ConversationMessages> GetMessagesAsync(string conversationId, string watermark)
        {
            string url = $"{ConversationsApi}/{conversationId}/activities?watermark={watermark}";

            if (string.IsNullOrEmpty(watermark))
            {
                url = $"{ConversationsApi}/{conversationId}/activities";
            }

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _directLineToken);
                var response = await httpClient.GetStringAsync(url);

                return JsonConvert.DeserializeObject<ConversationMessages>(response);
            }
        }

    }
}
