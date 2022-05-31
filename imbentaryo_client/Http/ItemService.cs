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

        public async Task<HttpMessage> InsertItem(AddItemViewModel item)
        {
            HttpMessage message = new HttpMessage();

            using(this.client = new HttpClient())
            {
                Uri uri = new Uri(this.uri + "/add");

                // serialize item model to json content
                string jsonItem = JsonConvert.SerializeObject(item);

                // api request content with headers
                StringContent content = new StringContent(jsonItem, Encoding.UTF8, "application/json");

                // make request
                HttpResponseMessage response = await this.client.PostAsync(uri, content);

                string rawMessage = await response.Content.ReadAsStringAsync();

                message = JsonConvert.DeserializeObject<HttpMessage>(rawMessage);

                message.StatusCode = response.StatusCode.ToString();
            }

            return message;
        }

        public async Task<List<ItemAPIModel>> GetItems(string? itemGroupId)
        {
            List<ItemAPIModel> items = new List<ItemAPIModel>();

            using (this.client = new HttpClient())
            {
                Uri uri;
                if (string.IsNullOrEmpty(itemGroupId))
                {
                    // we will query all of the items
                    uri = new Uri(this.uri + "/all");
                } 
                else
                {
                    // query all items that are part of a specific group
                    uri = new Uri(this.uri + "/all/group/" + itemGroupId);
                }

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<List<ItemAPIModel>>(content);
                }

            }

            return items;
        }

        public async Task<ItemInventoryModel> GetItemDetails(string itemId)
        {
            ItemInventoryModel item = null;

            using (this.client = new HttpClient())
            {
                Uri uri = new Uri(this.uri + "/one/" + itemId);

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    item = JsonConvert.DeserializeObject<ItemInventoryModel>(content);
                }
            }

            return item;
        }
    }

    /*
     * An item-inventory-item_group model
     *
     */
    internal class ItemInventoryModel
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("inventory_id")]
        public string InventoryId { get; set; }

        [JsonProperty("name")]
        public string ItemName { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

        [JsonProperty("quantity")]
        public string Quantity { get; set; }

        [JsonProperty("unit_price")]
        public string Price { get; set; }

        [JsonProperty("updated")]
        public string DateUpdated { get; set; }

        [JsonProperty("date_added")]
        public string DateAdded { get; set; }
    }

    /*
     * An item model containing properties for input fields
     * in the add item view
     */
    internal class AddItemViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("stock")]
        public int Stock { get; set; }
        [JsonProperty("group_id")]
        public string ItemGroupId { get; set; }
        [JsonProperty("unit_price")]
        public float UnitPrice { get; set; }
        [JsonProperty("remarks")]
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