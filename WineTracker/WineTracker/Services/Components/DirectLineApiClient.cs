﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WineTracker.Interface;
using WineTracker.Models.DirectLineClient;
using Conversation = WineTracker.Models.DirectLineClient.Conversation;

namespace WineTracker.Services.Components
{
    public class DirectLineApiClient : IDirectLineApiClient
    {
    
        public readonly string _directLineKey;
        private Conversation _lastConversation;

        private const string Host = "https://directline.botframework.com";

        private static readonly string ConversationsApi = $"{Host}/v3/directline/conversations";

        private string _botSecret;
        public DirectLineApiClient()
        {

        }

        public void SetClientSecret(string botSecret)
        {
            _botSecret = botSecret;
        }
        public async Task GetToken()
        {

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{Host}");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _directLineKey);

                var response = await httpClient.GetStringAsync("/v3/directline/tokens/generate");
                JsonConvert.DeserializeObject<AuthenticationResponse>(response);
            }

        }

        public async Task RefreshToken()
        {

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri($"{Host}");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _botSecret);

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
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _botSecret);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.PostAsync(ConversationsApi, null);

                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<StartConversationResponse>(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task SendMessageAsync(string conversationId, string from, string text)
        {
            string url = $"{Host}/api/conversations/{conversationId}/activities";

            var message = new Message
            {
                Type = "message",
                From = new From { Id = from },
                Text = text
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _botSecret);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.PostAsync(url, content);

                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    // TODO: response.EnsureSuccessStatusCode();
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
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _botSecret);
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
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _botSecret);
                var response = await httpClient.GetStringAsync(url);

                return JsonConvert.DeserializeObject<ReconnectMessageResponse>(response);
            }
        }
        public async Task<ConversationMessages> GetMessagesAsync(string conversationId, string watermark)
        {
            string url = $"{ConversationsApi}/{conversationId}/activities?watermark={watermark}";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _botSecret);
                var response = await httpClient.GetStringAsync(url);

                return JsonConvert.DeserializeObject<ConversationMessages>(response);
            }
        }

    }
}
