using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using imbentaryo_client.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace imbentaryo_client.Http
{
    internal class AccountService : Service
    {

        private const string HIGH_PATH = "/account";
        private string uri;

        public AccountService() : base()
        {
            this.uri = END_POINT + HIGH_PATH;
        }

        public async Task<LoginModel> LoginUser(string username, string password)
        {
            LoginModel message = new LoginModel();

            using (this.client = new HttpClient())
            {
                Uri uri = new Uri(this.uri + "/login");

                // convert to json object
                string jsonItem = JsonConvert.SerializeObject(new { username, password });

                // add headers
                StringContent content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                // request
                HttpResponseMessage response = await this.client.PostAsync(uri, content);

                string rawMessage = await response.Content.ReadAsStringAsync();

                message = JsonConvert.DeserializeObject<LoginModel>(rawMessage);

                message.StatusCode = response.StatusCode.ToString();
            }

            return message;
        }

        public async Task<HttpMessage> SignUpUser(string username, string password)
        {
            HttpMessage loginModel = new HttpMessage();

            using (this.client = new HttpClient())
            {
                Uri uri = new Uri(this.uri + "/sign-up");

                // convert to json object
                string jsonItem = JsonConvert.SerializeObject(new { username, password });

                // add headers
                StringContent content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                // request
                HttpResponseMessage response = await this.client.PostAsync(uri, content);

                string rawMessage = await response.Content.ReadAsStringAsync();

                loginModel = JsonConvert.DeserializeObject<HttpMessage>(rawMessage);

                loginModel.StatusCode = response.StatusCode.ToString();
            }

            return loginModel;
        }

    }
    
    /*
     * A view model for the login that will hold the account id,
     * username, status code, and message after a success
     * login
     */
    internal class LoginModel
    {
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        public string StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}