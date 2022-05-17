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

        // spinner values and keys holder
        List<string> groupNames;
        List<string> groupIds;

        // selected spinner key holder
        string selectedItemGroupKey;

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
            this.addItemBtn.Click += this.InsertItem_Click;

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

            // asssign the selected item group key
            this.selectedItemGroupKey = this.groupIds[e.Position];
        }

        private async void InsertItem_Click(object sender, EventArgs e)
        {
            ItemService itemService = new ItemService();
            if (ValidateFields())
            {
                // build model
                Item item = new Item()
                {
                    Name = this.itemNameEditText.Text,
                    UnitPrice = 0, // forgot to implement this one
                    Remarks = this.remarksEditText.Text,
                    GroupId = this.selectedItemGroupKey
                };

                HttpMessage message = await itemService.InsertItem(item);

                Toast.MakeText(this.Activity, message.StatusCode + ". " + message.Message, ToastLength.Long).Show();

                if (message.StatusCode == System.Net.HttpStatusCode.Created.ToString())
                {
                    // reset form
                    // this.ResetFragment();

                    this.itemNameEditText.Text = "";
                    this.remarksEditText.Text = "";
                    this.stockCountEditText.Text = "0";
                }
            }
        }

        private bool ValidateFields()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(itemNameEditText.Text))
            {
                isValid = false;
                Toast.MakeText(this.Activity, "Item Name is required", ToastLength.Short).Show();
            }
            else if (string.IsNullOrEmpty(this.selectedItemGroupKey))
            {
                isValid = false;
                Toast.MakeText(this.Activity, "Item group is required. Select one from the dropdown.", ToastLength.Short).Show();
            }
            else if (string.IsNullOrEmpty(stockCountEditText.Text))
            {
                isValid = false;
                Toast.MakeText(this.Activity, "If there is no available stock, put 0.", ToastLength.Short).Show();
            }
            //else if (string.IsNullOrEmpty(remarksEditText.Text))
            //{
            //    isValid = false;
            //    Toast.MakeText(this.Activity, "Item Name is required", ToastLength.Short).Show();
            //}

            return isValid;
        }
    }
}