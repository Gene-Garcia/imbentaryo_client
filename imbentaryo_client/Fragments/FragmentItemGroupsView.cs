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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imbentaryo_client.Fragments
{
    public class FragmentItemGroupsView : AndroidX.Fragment.App.Fragment
    {
        ListView itemGroupsListView;
        List<ItemGroup> itemGroups;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.fragment_item_groups_view, container, false);

            this.itemGroupsListView = view.FindViewById<ListView>(Resource.Id.itemGroupsListView);

            // configure listener itemclick
            this.itemGroupsListView.ItemClick += this.ItemGroup_Click;
            this.itemGroupsListView.ItemLongClick += this.ItemGroup_LongClick;

            return view;
        }

        /*
         * Long click of a row of an item group will allow for
         * editing/updating functino
         */
        private void ItemGroup_LongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            string itemGroupId = this.itemGroups[e.Position].Id;
            ((MainActivity)this.Activity).StartItemGroupDetailView(itemGroupId);

        }

        /*
         * Single click will open the inventoried items of an item group
         * 
         */
        private void ItemGroup_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            string itemGroupId = this.itemGroups[e.Position].Id;
            string name = this.itemGroups[e.Position].Name;

            // call items list view fragment
            ((MainActivity)this.Activity).StartItemInventoriesOfGroup(itemGroupId, name);
        }

        public override async void OnResume()
        {
            base.OnResume();

            ItemGroupService service = new ItemGroupService();

            // call API to get data
            this.itemGroups = await service.GetItemGroups();

            // set adapter
            ItemGroupAdapter adapter = new ItemGroupAdapter(this.Activity, itemGroups);
            this.itemGroupsListView.Adapter = adapter;
        }
    }
}