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
                this.ConfigureAuthorization();

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

        public async Task<HttpMessage> InsertItemGroup(ItemGroup itemGroup)
        {
            HttpMessage message = new HttpMessage();

            using(this.client = new HttpClient())
            {
                this.ConfigureAuthorization();

                Uri uri = new Uri(this.uri + "/add");

                // convert to json object
                string jsonItem = JsonConvert.SerializeObject(itemGroup);

                // add headers
                StringContent content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                // request
                HttpResponseMessage response = await this.client.PostAsync(uri, content);

                string rawMessage = await response.Content.ReadAsStringAsync();

                message = JsonConvert.DeserializeObject<HttpMessage>(rawMessage);

                message.StatusCode = response.StatusCode.ToString();
            }

            return message;
        }

        public async Task<ItemGroup> GetItemGroup(string groupId)
        {
            ItemGroup itemGroup = new ItemGroup();

            using (this.client = new HttpClient())
            {
                this.ConfigureAuthorization();

                Uri uri = new Uri(this.uri + "/one/" + groupId);

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    itemGroup = JsonConvert.DeserializeObject<ItemGroup>(content);
                }
            }

            return itemGroup;
        }

        public async Task<HttpMessage> UpdateItemGroup(ItemGroup group)
        {
            HttpMessage message = new HttpMessage();

            using(this.client = new HttpClient())
            {
                this.ConfigureAuthorization();

                Uri uri = new Uri(this.uri + "/update");

                // convert to json object
                string jsonItem = JsonConvert.SerializeObject(group);

                // add headers
                StringContent content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                // request
                HttpResponseMessage response = await this.client.PatchAsync(uri, content);

                string rawMessage = await response.Content.ReadAsStringAsync();

                message = JsonConvert.DeserializeObject<HttpMessage>(rawMessage);

                message.StatusCode = response.StatusCode.ToString();
            }

            return message;
        }

        public async Task<HttpMessage> DeleteItemGroup(string groupId)
        {
            HttpMessage message = new HttpMessage();

            using(this.client = new HttpClient())
            {
                this.ConfigureAuthorization();

                Uri uri = new Uri(this.uri + "/delete/" + groupId);

                // request
                HttpResponseMessage response = await this.client.DeleteAsync(uri);

                string rawMessage = await response.Content.ReadAsStringAsync();

                message = JsonConvert.DeserializeObject<HttpMessage>(rawMessage);

                message.StatusCode = response.StatusCode.ToString();
            }

            return message;
        }
    }
}