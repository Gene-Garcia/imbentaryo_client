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

        public async Task<List<ItemAPIModel>> GetItems()
        {
            Uri uri = new Uri(this.uri + "/all");

            List<ItemAPIModel> items = new List<ItemAPIModel>();

            HttpResponseMessage response = await this.client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<ItemAPIModel>>(content);
            }

            return items;
        }
    }


    /*
     * An item model containing properties for input fields
     * in the add item view
     */
    internal class AddItemViewModel
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public string ItemGroupId { get; set; }
        public float UnitPrice { get; set; }
        public string Remarks { get; set; }
    }

    /*
     * A model with properties similar to what
     * the API /all returns.
     * 
     * We will manually convert each property to respective
     * item and inventory model
     */
    internal class ItemAPIModel
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("updated")]
        public string Updated { get; set; }
        [JsonProperty("quantity")]
        public string Quantity { get; set; }
    }
}