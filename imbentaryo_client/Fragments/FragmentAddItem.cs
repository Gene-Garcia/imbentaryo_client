using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using imbentaryo_client.Http;
using imbentaryo_client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imbentaryo_client.Fragments
{
    public class FragmentAddItem : AndroidX.Fragment.App.Fragment
    {
        // form components
        EditText itemNameEditText;
        Spinner itemGroupDropDown;
        EditText stockCountEditText;
        EditText remarksEditText;
        Button addItemBtn;


        // spinner value holder
        List<string> groupNames;
        List<string> groupIds;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            groupNames = new List<string>();
            groupIds = new List<string>();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //base.OnCreateView(inflater, container, savedInstanceState);

            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_add_item, container, false);

            // itemNameEditTxt itemGroupDropDown stockCountEditText remarksEditText addItemBtn
            this.itemNameEditText = view.FindViewById<EditText>(Resource.Id.itemNameEditText);
            this.itemGroupDropDown = view.FindViewById<Spinner>(Resource.Id.itemGroupDropDown);
            this.stockCountEditText = view.FindViewById<EditText>(Resource.Id.stockCountEditText);
            this.remarksEditText = view.FindViewById<EditText>(Resource.Id.remarksEditText);
            this.addItemBtn = view.FindViewById<Button>(Resource.Id.addItemBtn);

            // event handlers
            this.itemGroupDropDown.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinner_ItemGroupSelected);

            return view;
        }

        public async override void OnResume()
        {
            base.OnResume();

            // populate by making api request
            ItemGroupService igs = new ItemGroupService();
            List<ItemGroup> itemGroups = await igs.GetItemGroups();
            foreach (ItemGroup itemGroup in itemGroups)
            {
                groupNames.Add(itemGroup.Name);
                groupIds.Add(itemGroup.Id);
            }

            // populate spinner
            ArrayAdapter spinnerAdapter = new ArrayAdapter<string>(this.Activity, Resource.Layout.support_simple_spinner_dropdown_item, this.groupNames);
            itemGroupDropDown.Adapter = spinnerAdapter;
        }

        private void Spinner_ItemGroupSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = "Item Position " + spinner.GetItemAtPosition(e.Position) + ". Value " + this.groupNames[e.Position] + ". Key " + this.groupIds[e.Position];
            Toast.MakeText(this.Activity, toast, ToastLength.Long).Show();
        }
    }
}