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
    internal class ItemGroupService : Service
    {
        private const string HIGH_PATH = "/group";
        private string uri;

        public ItemGroupService() : base()
        {
            this.uri = END_POINT + HIGH_PATH;
        }

        public async Task<List<ItemGroup>> GetItemGroups()
        {
            List<ItemGroup> itemGroups = new List<ItemGroup>();

            using (this.client = new HttpClient())
            {
                Uri uri = new Uri(this.uri + "/all");

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    itemGroups = JsonConvert.DeserializeObject<List<ItemGroup>>(content);
                }
            }

            return itemGroups;

        }
    }
}