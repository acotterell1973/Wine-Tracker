using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WineTracker.EventHandler;
using WineTracker.Interface;
using WineTracker.Models.DirectLineClient;
using Conversation = WineTracker.Models.DirectLineClient.Conversation;

namespace WineTracker.Services.Components
{
    public class DirectLineApiClient : IDirectLineApiClient
    {

        private string _directLineKey;
        private string _directLineToken;
        private string _conversationId = "NOT_CONNECTED";
        private string _botWaterMark;
        private string _streamUrl;
        private Websockets.IWebSocketConnection _connection;
        private const string Host = "https://directline.botframework.com";
        private static readonly string ConversationsApi = $"{Host}/v3/directline/conversations";


        /// <summary>
        /// Initialize the conversation stream
        /// </summary>
        /// <param name="directLineSecret"></param>
        /// <returns></returns>
        public string Initialize(string directLineSecret)
        {
            if (string.IsNullOrEmpty(directLineSecret))
                throw new ArgumentException("Argument is null or empty", nameof(directLineSecret));

            _directLineKey = directLineSecret;
            var tsk = Task.Run(async () =>
              {
                  var token = await GetTokenAsync();
                  _directLineToken = token.Token;


                  //Start the converstation and get back the stream
                  //url that we will use for the websocket _connection.
                  var startConversation = await StartConversationAsync();

                  if (startConversation != null)
                  {
                      _directLineToken = startConversation.Token;
                      _conversationId = startConversation.ConversationId;
                      _streamUrl = startConversation.StreamUrl;


                      //We don't need the secret after we have a token
                      //With the refresh we will use the token
                      _directLineKey = string.Empty;


                      // 60 seconds to connect to the WebSocket URL 
                      _connection = Websockets.WebSocketFactory.Create();
                      _connection.OnLog += Connection_OnLog;
                      _connection.OnError += Connection_OnError;
                      _connection.OnMessage += Connection_OnMessage;
                      _connection.OnOpened += Connection_OnOpened;

                      //open the stream and start listening for activities
                      if (_connection.IsOpen) _connection.Close();
                      _connection.Open(_streamUrl);
                  }
              });

            tsk.Wait();
            return _conversationId;
        }

        #region WebSocket Connection Event Handlers
        private void Connection_OnOpened()
        {

        }

        public event EventHandler<OnMessageEventArgs> OnBotMessage;
        private void Connection_OnMessage(string response)
        {
            var activity = JsonConvert.DeserializeObject<ConversationMessages>(response);
            //ignore empty messages. Empty messages are to ensure socket connection.
            if(activity==null) return;
            //Track activity via watermark
            activity = TrackActivity(activity);

            OnBotMessage?.Invoke(this, new OnMessageEventArgs(activity));
        }

        private void Connection_OnError(string obj)
        {

        }

        private void Connection_OnLog(string obj)
        {

        }
        #endregion

        #region public actions
        
        public async Task SendMessageAsync(string from, string text)
        {
            string url = $"{ConversationsApi}/{_conversationId}/activities";

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

        public async Task EndMessageAsync(string from)
        {
            string url = $"{Host}/api/conversations/{_conversationId}/activities";

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
        #endregion
        #region Private Helpers
        /// <summary>
        /// Opens a direct conversation with a specific BOT
        /// This will also return a stream Url to communicate over 
        /// webSockets
        /// </summary>
        /// <returns></returns>
        private async Task<StartConversationResponse> StartConversationAsync()
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

        private async Task<ReconnectMessageResponse> ReconnectAsync(string conversationId, string watermark)
        {
            string url = $"{ConversationsApi}/{conversationId}?watermark={watermark}";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _directLineToken);
                var response = await httpClient.GetStringAsync(url);

                return JsonConvert.DeserializeObject<ReconnectMessageResponse>(response);
            }
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
        ////Refreshes the token 5 minutes for it expires
        //private async Task PeriodicRefreshTokenAsync(CancellationToken cancellationToken)
        //{
        //    while (true)
        //    {
        //        //var authResponse = await GetBotConnectorToken();
        //        //_botConnectorToken = authResponse.access_token;
        //        ////wait 5 minutes before the token expires to get a new one.
        //        //var interval = TimeSpan.FromSeconds(authResponse.expires_in - (5 * 60));
        //        //await Task.Delay(interval, cancellationToken);
        //    }
        //}
        private async Task RefreshTokenAsync()
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

        private ConversationMessages TrackActivity(ConversationMessages activity)
        {
            //TODO: Implement
            return activity;
        }
        #endregion
    }
}
