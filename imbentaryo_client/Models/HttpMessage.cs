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
    internal class HttpMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        public string StatusCode { get; set; }
    }
}