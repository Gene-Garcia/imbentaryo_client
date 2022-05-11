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
    public class ItemGroup
    {
        [JsonProperty("group_id")]
        public string Id { get; set; }

        [JsonProperty("group_name")]
        public string Name { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }
    }
}