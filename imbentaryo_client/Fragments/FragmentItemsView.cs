using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using imbentaryo_client.Adapters;
using imbentaryo_client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imbentaryo_client.Fragments
{
    public class FragmentItemsView : AndroidX.Fragment.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        ListView inventoryItemsListView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_items_view, container, false);

            this.inventoryItemsListView = view.FindViewById<ListView>(Resource.Id.inventoryItemsListView);

            return view;
        }


        public override void OnResume()
        {
            base.OnResume();

            // set up adapter
            ItemInventoryAdapter adapter = new ItemInventoryAdapter(this.Activity, GetItems());
            this.inventoryItemsListView.Adapter = adapter;
        }

        private List<Item> GetItems()
        {

            List<Item> items = new List<Item>();

            items.Add(new Item()
            {
                Name = "Test",
                DateAdded = "5/18/2022",
                Stock = 15
            });

            items.Add(new Item()
            {
                Name = "Test 2",
                DateAdded = "4/5/2022",
                Stock = 54
            });

            items.Add(new Item()
            {
                Name = "Test 3",
                DateAdded = "12/11/2022",
                Stock = 50
            });

            items.Add(new Item()
            {
                Name = "Test 4",
                DateAdded = "1/7/2022",
                Stock = 15
            });

            items.Add(new Item()
            {
                Name = "Test",
                DateAdded = "5/18/2022",
                Stock = 15
            });

            items.Add(new Item()
            {
                Name = "Test 2",
                DateAdded = "4/5/2022",
                Stock = 54
            });

            items.Add(new Item()
            {
                Name = "Test 3",
                DateAdded = "12/11/2022",
                Stock = 50
            });

            items.Add(new Item()
            {
                Name = "Test 4",
                DateAdded = "1/7/2022",
                Stock = 15
            });

            items.Add(new Item()
            {
                Name = "Test",
                DateAdded = "5/18/2022",
                Stock = 15
            });

            items.Add(new Item()
            {
                Name = "Test 2",
                DateAdded = "4/5/2022",
                Stock = 54
            });

            items.Add(new Item()
            {
                Name = "Test 3",
                DateAdded = "12/11/2022",
                Stock = 50
            });

            items.Add(new Item()
            {
                Name = "Test 4",
                DateAdded = "1/7/2022",
                Stock = 15
            });

            items.Add(new Item()
            {
                Name = "Test",
                DateAdded = "5/18/2022",
                Stock = 15
            });

            items.Add(new Item()
            {
                Name = "Test 2",
                DateAdded = "4/5/2022",
                Stock = 54
            });

            items.Add(new Item()
            {
                Name = "Test 3",
                DateAdded = "12/11/2022",
                Stock = 50
            });

            items.Add(new Item()
            {
                Name = "Test 4",
                DateAdded = "1/7/2022",
                Stock = 15
            });

            items.Add(new Item()
            {
                Name = "Test",
                DateAdded = "5/18/2022",
                Stock = 15
            });

            items.Add(new Item()
            {
                Name = "Test 2",
                DateAdded = "4/5/2022",
                Stock = 54
            });

            items.Add(new Item()
            {
                Name = "Test 3",
                DateAdded = "12/11/2022",
                Stock = 50
            });

            items.Add(new Item()
            {
                Name = "Test 4",
                DateAdded = "1/7/2022",
                Stock = 15
            });

            items.Add(new Item()
            {
                Name = "Test",
                DateAdded = "5/18/2022",
                Stock = 15
            });

            items.Add(new Item()
            {
                Name = "Test 2",
                DateAdded = "4/5/2022",
                Stock = 54
            });

            items.Add(new Item()
            {
                Name = "Test 3",
                DateAdded = "12/11/2022",
                Stock = 50
            });

            items.Add(new Item()
            {
                Name = "Test 4",
                DateAdded = "1/7/2022",
                Stock = 15
            });

            return items;
        }
    }
}