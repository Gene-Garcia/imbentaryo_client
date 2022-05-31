using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using imbentaryo_client.Adapters;
using imbentaryo_client.Http;
using imbentaryo_client.Models;
using imbentaryo_client.Models.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imbentaryo_client.Fragments
{
    public class FragmentItemsView : AndroidX.Fragment.App.Fragment
    {
        // the group id intent/bundle passed which will tell what items of group to get and show
        string itemGroupId;
        // a value passed through
        string groupName;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            try
            {
                this.itemGroupId = Arguments.GetString("itemGroupId");
                this.groupName = Arguments.GetString("groupName");
            } catch(Exception e) { }

        }

        ListView inventoryItemsListView;
        List<Inventory> items;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_items_view, container, false);

            // change view title heading so that user will know if specific group is selected
            if (!string.IsNullOrEmpty(groupName))
            {
                TextView heading = view.FindViewById<TextView>(Resource.Id.heading);
                heading.Text = this.groupName + " Items Inventory";
            }

            this.inventoryItemsListView = view.FindViewById<ListView>(Resource.Id.inventoryItemsListView);
            this.inventoryItemsListView.ItemClick += this.ItemInventory_Click;

            return view;
        }

        public override async void OnResume()
        {
            base.OnResume();

            // call api
            ItemService service = new ItemService();

            List<ItemAPIModel> itemAPIModels =  await service.GetItems(itemGroupId);

            this.items = ModelConverter.ConvertToItem(itemAPIModels);

            // set up adapter
            ItemInventoryAdapter adapter = new ItemInventoryAdapter(this.Activity, items);
            this.inventoryItemsListView.Adapter = adapter;
        }


        // Item list view on lick
        private void ItemInventory_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            ((MainActivity)this.Activity).StartItemInventoryDetailView(this.items[e.Position].Item.Id);
        }

    }
}