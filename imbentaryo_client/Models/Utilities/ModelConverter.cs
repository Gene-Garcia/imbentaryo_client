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

        public static Inventory ConvertToInventory(ItemInventoryModel data)
        {
            // not everytime inventory record will be present for some reason
            int parsedQuantity = 0;
            // just to catch any unforseen error in parsing price
            float parsedPrice = 0;
            try
            {
                parsedPrice = float.Parse(data.Price);
                parsedQuantity = int.Parse(data.Quantity);
            }
            catch (Exception ex) { }

            ItemGroup group = new ItemGroup()
            {
                Id = data.GroupId,
                Name = data.GroupName
            };
            Item tempItem = new Item()
            {
                Id = data.ItemId,
                Name = data.ItemName,
                UnitPrice = parsedPrice,
                Remarks = data.Remarks,
                GroupId = data.GroupId,
                DateAdded = data.DateAdded,
                ItemGroup = group
            };
            Inventory inventory = new Inventory()
            {
                Id = data.InventoryId,
                ItemId = data.ItemId,
                Quantity = parsedQuantity,
                Updated = data.DateUpdated,
                Item = tempItem
            };

            return inventory;
        }
    }
}