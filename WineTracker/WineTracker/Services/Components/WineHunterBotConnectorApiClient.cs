using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WineTracker.Models.DirectLineClient;

// ReSharper disable FunctionNeverReturns

namespace WineTracker.Services.Components
{
    public class WineHunterBotConnectorApitClient : IWineHunterBotConnectorApitClient
    {
        private const string Host = "https://779347e3.ngrok.io";
        private static readonly string ConversationsApi = $"{Host}/api/";
        private string _botConnectorToken;
        
        public class AuthResponse
        {
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public int ext_expires_in { get; set; }
            public string access_token { get; set; }
        }

        public WineHunterBotConnectorApitClient()
        {
            Task.Run(async () =>
            {
                await PeriodicRefreshTokenAsync(CancellationToken.None);
            });
        }

        //Refreshes the token 5 minutes for it expires
        private async Task PeriodicRefreshTokenAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                var authResponse = await GetBotConnectorToken();
                _botConnectorToken = authResponse.access_token;
                //wait 5 minutes before the token expires to get a new one.
                var interval = TimeSpan.FromSeconds(authResponse.expires_in -(5 * 60));
                await Task.Delay(interval, cancellationToken);
            }
        }

        private async Task<AuthResponse> GetBotConnectorToken()
        {
            AuthResponse resp;
            var tokenRequest = new
            {
                grant_type = "client_credentials",
                client_id = "1d7bfed9-5ea1-4cb7-947d-deb5e57b8ddb",
                client_secret = "8PjGSWCyykzjdc7AvYO2TgN",
                scope = "https://api.botframework.com/.default"
            };
            var postValues = new Dictionary<string, string>
            {
                {"grant_type",tokenRequest.grant_type},
                {"client_id",tokenRequest.client_id},
                {"client_secret",tokenRequest.client_secret},
                {"scope",tokenRequest.scope},
            };


            var content = new FormUrlEncodedContent(postValues);

            var authServiceEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(authServiceEndpoint);
                client.DefaultRequestHeaders.Accept.Clear();
          
                var response = await client.PostAsync("", content);
                var tok = await response.Content.ReadAsStringAsync();

                resp = JsonConvert.DeserializeObject<AuthResponse>(tok);
                
            }
            return resp;
        }

   
        public async Task SendMessageAsync(string from, string text)
        {
            string url = $"{ConversationsApi}/messages";

            var message = new Activity
            {
                Type = "message",
                From = new From(),
                Text = text
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _botConnectorToken);
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
        
    }
}
