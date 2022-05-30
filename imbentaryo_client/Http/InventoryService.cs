using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imbentaryo_client.Http
{
    internal class InventoryService : Service
    {
        private const string HIGH_PATH = "/item";
        private string uri;

        public InventoryService() : base()
        {
            this.uri = END_POINT + HIGH_PATH;
        }

        public async void UpdateItemInventory()
        {

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
