using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Utils
{
    public class OAuthHelp
    {
        private string _clientId { get; set; }

        private string _clientPwd { get; set; }
        private string _userName { get; set; }

        private string _userPwd { get; set; }

        private string _url { get; set; }

        private OAuthHelp()
        {

        }

        public OAuthHelp(string clientId, string clientPwd, string userName, string userPwd, string url)
        {
            this._clientId = clientId;
            this._clientPwd = clientPwd;
            this._userName = userName;
            this._userPwd = userPwd;
            this._url = url;
        }


        public string GetAccessToken()
        {
            var tokenClient = new TokenClient(this._url + "Token", this._clientId, this._clientPwd);

            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync(this._userName, this._userPwd).Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return string.Empty;
            }
            else
            {
                return tokenResponse.AccessToken;
            }
        }

        public virtual HttpResponseMessage Get(string interfaceName, string queryString = "")
        {
            var client = new HttpClient();
            client.SetBearerToken(GetAccessToken());

            //HttpContent content = new StringContent(requestData)
            //{
            //    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            //};
            return client.GetAsync(this._url + "Api/" + interfaceName + "?" + queryString).Result;
        }


        public HttpResponseMessage GetPostResponse(string accessToken, string interfaceName, string requestData)
        {
            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            HttpContent content = new StringContent(requestData)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };
            return client.PostAsync(this._url + "Api/" + interfaceName, content).Result;
        }

        public string GetPostResponse(string interfaceName, string requestData)
        {
            HttpResponseMessage response = this.GetPostResponse(GetAccessToken(), interfaceName, requestData);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content.ReadAsStringAsync().Result; ;
            }
            else
            {
                return string.Empty;
            }
        }

        public HttpResponseMessage GetResponse(string accessToken, string interfaceName)
        {
            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            //HttpContent content = new StringContent(requestData)
            //{
            //    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            //};
            return client.GetAsync(this._url + "Api/" + interfaceName).Result;
        }
        //public string GetAccessTo

        public class MessageObj
        {
            public string Message { get; set; }
        }
    }
}
