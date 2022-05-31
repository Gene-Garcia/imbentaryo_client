using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using imbentaryo_client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace imbentaryo_client.Http
{
    internal class InventoryService : Service
    {
        private const string HIGH_PATH = "/inventory";
        private string uri;

        public InventoryService() : base()
        {
            this.uri = END_POINT + HIGH_PATH;
        }

        public async Task<HttpMessage> UpdateItemInventory(ItemUpdateModel item, InventoryUpdateModel inventory)
        {
            HttpMessage message = new HttpMessage();

            using(this.client= new System.Net.Http.HttpClient())
            {
                Uri uri = new Uri(this.uri + "/update/item-inventory");

                // serialize
                string jsonItem = JsonConvert.SerializeObject(new {item, inventory});

                // header
                StringContent content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                // request
                HttpResponseMessage response = await client.PatchAsync(uri, content);

                string rawMessage = await response.Content.ReadAsStringAsync();

                message = JsonConvert.DeserializeObject<HttpMessage>(rawMessage);

                message.StatusCode = response.StatusCode.ToString();
            }

            return message;
        }
    }

    internal class ItemUpdateModel
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("unit_price")]
        public float UnitPrice { get; set; }
        [JsonProperty("remarks")]
        public string Remarks { get; set; }
        [JsonProperty("group_id")]
        public string GroupId { get; set; }
    }

    internal class InventoryUpdateModel
    {
        [JsonProperty("inventory_id")]
        public string InventoryId { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}
