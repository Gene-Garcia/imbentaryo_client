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
    internal class ItemInventoryAdapter : BaseAdapter
    {

        Context context;

        private Activity activity;
        private List<Item> items;

        public ItemInventoryAdapter(Activity activity, List<Item> items)
        {
            this.activity = activity;
            this.items = items;
        }


        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? this.activity.LayoutInflater.Inflate(Resource.Layout._item_inventory_template, parent, false);

            // initiate components
            TextView itemName = view.FindViewById<TextView>(Resource.Id.itemName);
            TextView stockCount = view.FindViewById<TextView>(Resource.Id.stockCount);
            TextView inventoryDate = view.FindViewById<TextView>(Resource.Id.inventoryDate);

            //fill in your items
            itemName.Text = this.items[position].Name;
            stockCount.Text = this.items[position].Stock.ToString();
            inventoryDate.Text = this.items[position].DateAdded;

            return view;
        }

        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

    }

    internal class ItemInventoryAdapterViewHolder : Java.Lang.Object
    {
        public TextView itemName { get; set; }
        public TextView stockCount { get; set; }
        public TextView inventoryDate { get; set; }
    }
}