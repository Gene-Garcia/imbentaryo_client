using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using imbentaryo_client.Http;
using imbentaryo_client.Models;
using imbentaryo_client.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imbentaryo_client.Fragments
{
    public class FragmentItemDetailsView : AndroidX.Fragment.App.Fragment
    {
        // data passed when fragment is created
        string itemIdArgs;

        // model with the necessary data to be displayed in our ui
        Inventory itemInventory;

        // spinner value
        List<ItemGroup> itemGroups;

        // components
        EditText itemNameEditText;
        EditText inventoryQuantityEditText;
        EditText itemPriceEditText;
        Spinner itemGroupNameSpinner;
        EditText itemRemarksEditText;
        TextView inventoryUpdateDate;
        TextView itemDateAdded;
        Button saveUpdateBtn;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            this.itemIdArgs = Arguments.GetString("itemId");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_item_details_view, container, false);

            this.itemNameEditText = view.FindViewById<EditText>(Resource.Id.itemNameEditText);
            this.inventoryQuantityEditText = view.FindViewById<EditText>(Resource.Id.inventoryQuantityEditText);
            this.itemPriceEditText = view.FindViewById<EditText>(Resource.Id.itemPriceEditText);
            this.itemGroupNameSpinner = view.FindViewById<Spinner>(Resource.Id.itemGroupNameSpinner);
            this.itemRemarksEditText = view.FindViewById<EditText>(Resource.Id.itemRemarksEditText);
            this.inventoryUpdateDate = view.FindViewById<TextView>(Resource.Id.inventoryUpdateDate);
            this.itemDateAdded = view.FindViewById<TextView>(Resource.Id.itemDateAdded);
            this.saveUpdateBtn = view.FindViewById<Button>(Resource.Id.saveUpdateBtn);

            this.saveUpdateBtn.Click += this.SaveUpdate_Click;

            return view;
        }

        public override async void OnResume()
        {
            base.OnResume();

            // Handle getting data to be displayed
            ItemService service = new ItemService();

            // get data from api
            ItemInventoryModel data = await service.GetItemDetails(this.itemIdArgs);

            // build formal model of item, inventory, and item group
            this.itemInventory = ModelConverter.ConvertToInventory(data);

            this.ShowItemDetails();

            // spinner configurations
            ItemGroupService itemGroupService = new ItemGroupService();
            this.itemGroups = await itemGroupService.GetItemGroups();

            // we must first mutate the itemGroups so that the item group of the current is the first in the list
            itemGroups.Remove(itemGroups.Where(m => m.Id == this.itemInventory.Item.GroupId).FirstOrDefault());
            itemGroups.Insert(0, this.itemInventory.Item.ItemGroup);

            ArrayAdapter itemGroupAdapter = new ArrayAdapter<string>(this.Activity, Resource.Layout.support_simple_spinner_dropdown_item, itemGroups.Select(m => m.Name).ToList());
            
            this.itemGroupNameSpinner.Adapter = itemGroupAdapter;

            this.itemGroupNameSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(this.ItemGroup_ItemGroupSelected);
        }

        private void ShowItemDetails()
        {
            itemNameEditText.Text = this.itemInventory.Item.Name;

            inventoryQuantityEditText.Text = this.itemInventory.Quantity.ToString();
            inventoryUpdateDate.Text = "Updated last " + this.itemInventory.Updated;

            itemPriceEditText.Text = this.itemInventory.Item.UnitPrice.ToString();

            itemRemarksEditText.Text = this.itemInventory.Item.Remarks;
            itemDateAdded.Text = "Item added on " + this.itemInventory.Item.DateAdded;
        }

        private void ItemGroup_ItemGroupSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            // instead of using a placeholder variable, we can directly update the item group object inside the inventory.item
            ItemGroup selected = this.itemGroups[e.Position];

            // update the inventory object
            this.itemInventory.Item.ItemGroup = selected;
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(itemNameEditText.Text))
            {
                Toast.MakeText(this.Activity, "Item name cannot be empty", ToastLength.Short).Show();
                return false;
            }
            else if (string.IsNullOrEmpty(inventoryQuantityEditText.Text))
            {
                Toast.MakeText(this.Activity, "Inventory quantity be empty", ToastLength.Short).Show();
                return false;
            }
            else if (string.IsNullOrEmpty(itemPriceEditText.Text))
            {
                Toast.MakeText(this.Activity, "Inventory quantity be empty", ToastLength.Short).Show();
                return false;
            }
            else
            {
                // check for numerical values
                int parsedQty;
                if (!int.TryParse(inventoryQuantityEditText.Text, out parsedQty) || parsedQty < 0)
                {
                    Toast.MakeText(this.Activity, "Inventory quantity cannot be a negative value", ToastLength.Short).Show();
                    return false;
                }

                float parsedPrice;
                if (!float.TryParse(itemPriceEditText.Text, out parsedPrice) || parsedPrice < 0)
                {
                    Toast.MakeText(this.Activity, "Price cannot be a negative value", ToastLength.Short).Show();
                    return false;
                }

                return true;
            }
        }

        private async void SaveUpdate_Click(object sender, EventArgs e)
        {
            if (this.Validate())
            {
                //Toast.MakeText(this.Activity, "Save updated success", ToastLength.Short).Show();
                // build models

                // parsing numerical value with error handling
                float parsedPrice = this.itemInventory.Item.UnitPrice; // default price
                float.TryParse(itemPriceEditText.Text.Trim(), out parsedPrice);
                int parsedQty = this.itemInventory.Quantity; // default quantity
                int.TryParse(inventoryQuantityEditText.Text.Trim(), out parsedQty);

                ItemUpdateModel itemModel = new ItemUpdateModel()
                {
                    ItemId = this.itemInventory.Item.Id,
                    Name = this.itemNameEditText.Text.Trim(),
                    UnitPrice = parsedPrice,
                    Remarks = this.itemRemarksEditText.Text.Trim(),
                    GroupId = this.itemInventory.Item.ItemGroup.Id // updated every spinner item click
                };
                InventoryUpdateModel inventoryModel = new InventoryUpdateModel()
                {
                    InventoryId = this.itemInventory.Id,
                    Quantity = parsedQty
                };

                InventoryService service = new InventoryService();

                HttpMessage message = await service.UpdateItemInventory(itemModel, inventoryModel);

                Toast.MakeText(this.Activity, message.StatusCode + ". " + message.Message, ToastLength.Long).Show();

                if (message.StatusCode == System.Net.HttpStatusCode.Created.ToString())
                {
                    // return back to inventory items list
                    ((MainActivity)this.Activity).ChangeFragment(new FragmentItemsView());
                }
            }
        }
    }
}