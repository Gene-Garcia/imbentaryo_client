using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using imbentaryo_client.Models;
using imbentaryo_client.Http;

namespace imbentaryo_client.Models.Utilities
{
    internal class ModelConverter
    {
        public static List<Inventory> ConvertToItem(List<ItemAPIModel> itemAPIModel)
        {
            List<Inventory> items = new List<Inventory>();

            foreach (ItemAPIModel itemApi in itemAPIModel)
            {
                Item tempItem = new Item()
                {
                    Id = itemApi.ItemId,
                    Name = itemApi.Name
                };

                // there will be times that quantiy is null so parsing it will raise an error
                int parsedQuantity = 0;
                try
                {
                    parsedQuantity = int.Parse(itemApi.Quantity);
                }
                catch (Exception ex)
                {

                }

                Inventory tempInventory = new Inventory()
                {
                    Item = tempItem,
                    Updated = itemApi.Updated,
                    Quantity = parsedQuantity
                };

                items.Add(tempInventory);
            }

            return items;
        }
    }
}