using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using imbentaryo_client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace imbentaryo_client.Http
{
    internal class ItemService : Service
    {
        private const string HIGH_PATH = "/item";
        private string uri;

        public ItemService() : base()
        {
            this.uri = END_POINT + HIGH_PATH;
        }

        public async Task<HttpMessage> InsertItem(Item item)
        {
            Uri uri = new Uri(this.uri + "/add");

            // serialize item model to json content
            string jsonItem = JsonConvert.SerializeObject(item);

            // api request content with headers
            StringContent content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

            // make request
            HttpResponseMessage response = await this.client.PostAsync(uri, content);

            string rawMessage = await response.Content.ReadAsStringAsync();

            HttpMessage message = JsonConvert.DeserializeObject<HttpMessage>(rawMessage);

            message.StatusCode = response.StatusCode.ToString();

            return message;
        }
    }
}