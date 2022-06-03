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

namespace imbentaryo_client.Adapters
{
    internal class ItemGroupAdapter : BaseAdapter
    {

        private Activity activity;
        private List<ItemGroup> itemGroups;

        public ItemGroupAdapter(Activity activity, List<ItemGroup> itemGroups)
        {
            this.activity = activity;
            this.itemGroups = itemGroups;

            if (itemGroups.Count < 1) Toast.MakeText(activity, "No recorded item groups. Add new item groups by accessing Add Item Group on our side drawer.", ToastLength.Long).Show();
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? this.activity.LayoutInflater.Inflate(Resource.Layout._item_group_template, parent, false);

            // initiate components
            TextView itemGroupName = view.FindViewById<TextView>(Resource.Id.itemGroupName);

            //fill in your items
            itemGroupName.Text = this.itemGroups[position].Name;

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return this.itemGroups.Count;
            }
        }

    }

    internal class ItemGroupAdapterViewHolder : Java.Lang.Object
    {
        public TextView itemGroupName { get; set; }
    }
}