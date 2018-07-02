using Open.Net.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Open.OAuth2
{
    public class OAuth2Client
    {
        public static string GetRequestUrl(string oauthUrl, string clientId, string scope, string callbackUrl, string response_type = "code", Dictionary<string, string> parameters = null)
        {
            var query = "client_id=" + clientId;
            if (!string.IsNullOrWhiteSpace(scope))
                query += "&scope=" + Uri.EscapeUriString(scope);
            query += "&response_type=" + response_type;
            query += "&redirect_uri=" + Uri.EscapeUriString(callbackUrl);
            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    if (parameter.Value == null) continue;
                    query += $"&{parameter.Key}={parameter.Value}";
                }
            }
            return new UriBuilder(oauthUrl) { Query = query }.Uri.ToString();
        }

        public static async Task<OAuth2Token> ExchangeCodeForAccessTokenAsync(string tokenUrl, string code, string clientId, string clientSecret, string callbackUrl)
        {

            return await ExchangeCodeForAccessTokenAsync<OAuth2Token>(tokenUrl, code, clientId, clientSecret, callbackUrl);
        }

        public static async Task<T> ExchangeCodeForAccessTokenAsync<T>(string tokenUrl, string code, string clientId, string clientSecret, string callbackUrl)
        {
            var uri = new Uri(tokenUrl);
            var client = new HttpClient(HttpMessageHandlerFactory.Default.GetHttpMessageHandler());
            var entry = string.Format(@"code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code",
                                        code,
                                        clientId,
                                        clientSecret,
                                        Uri.EscapeUriString(callbackUrl));
            var content = new StringContent(entry);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<T>();
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        public static async Task<OAuth2Token> RefreshAccessTokenAsync(string tokenUrl, string refreshToken, string clientId, string clientSecret, CancellationToken cancellationToken)
        {
            return await RefreshAccessTokenAsync<OAuth2Token>(tokenUrl, refreshToken, clientId, clientSecret, cancellationToken);
        }

        public static async Task<T> RefreshAccessTokenAsync<T>(string tokenUrl, string refreshToken, string clientId, string clientSecret, CancellationToken cancellationToken)
        {

            var uri = new Uri(tokenUrl);
            var client = new HttpClient(HttpMessageHandlerFactory.Default.GetHttpMessageHandler());
            var entry = string.Format(@"client_id={0}&client_secret={1}&refresh_token={2}&grant_type=refresh_token",
                                        clientId,
                                        clientSecret,
                                        refreshToken);
            var content = new StringContent(entry);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<T>();
            }
            else
            {
                throw await ProcessException(response.Content);
            }
        }

        private static async Task<Exception> ProcessException(HttpContent httpContent)
        {
            var error = await httpContent.ReadJsonAsync<OAuth2Error>();
            return new OAuth2Exception(error);
        }
    }
}
