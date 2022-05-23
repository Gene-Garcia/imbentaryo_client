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
    public class FragmentAddItemGroup : AndroidX.Fragment.App.Fragment
    {
        EditText itemGroupName;
        EditText remarks;
        Button addItemGroupBtn;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_add_item_group, container, false);

            this.itemGroupName = view.FindViewById<EditText>(Resource.Id.itemGroupNameEditText);
            this.remarks = view.FindViewById<EditText>(Resource.Id.remarksEditText);
            this.addItemGroupBtn = view.FindViewById<Button>(Resource.Id.addItemGroupBtn);

            // event handlers
            this.addItemGroupBtn.Click += this.SaveItemGroup_Click;

            return view;
        }

        private async void SaveItemGroup_Click(object sender, EventArgs e)
        {
            if (this.ValidateFields())
            {
                // build model
                ItemGroup itemGroup = new ItemGroup()
                {
                    Name = this.itemGroupName.Text,
                    Remarks = this.remarks.Text
                };

                ItemGroupService service = new ItemGroupService();

                HttpMessage message = await service.InsertItemGroup(itemGroup);

                Toast.MakeText(this.Activity, message.StatusCode + ". " + message.Message, ToastLength.Long).Show();

                if (message.StatusCode == System.Net.HttpStatusCode.Created.ToString())
                {
                    // reset form
                    // this.ResetFragment();

                    this.itemGroupName.Text = "";
                    this.remarks.Text = "";
                }
            }
        }

        private bool ValidateFields()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(this.itemGroupName.Text))
            {
                isValid = false;
                Toast.MakeText(this.Activity, "Item group name is required", ToastLength.Long).Show();
            }

            return isValid;
        }
    }
}