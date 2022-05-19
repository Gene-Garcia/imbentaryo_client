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

namespace imbentaryo_client.Models
{
    internal class Inventory
    {
        [JsonProperty("inventory_id")]
        public string Id { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        // Foreign Table
        public Item Item { get; set; }

    }
}