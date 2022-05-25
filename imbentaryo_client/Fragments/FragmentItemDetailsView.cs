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
        string itemIdArgs;

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
    }
}