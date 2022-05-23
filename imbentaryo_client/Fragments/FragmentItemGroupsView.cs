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

            return view;
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