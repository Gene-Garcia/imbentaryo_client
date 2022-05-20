using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imbentaryo_client.Fragments
{
    public class FragmentItemGroupsView : AndroidX.Fragment.App.Fragment
    {
        ListView itemGroupsListView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //base.OnCreateView(inflater, container, savedInstanceState);

            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_item_groups_view, container, false);

            this.itemGroupsListView = view.FindViewById<ListView>(Resource.Id.itemGroupsListView);

            return view;
        }
    }
}