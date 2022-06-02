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
    public class FragmentItemGroupDetailsView : AndroidX.Fragment.App.Fragment
    {
        // selected item group
        ItemGroup group;

        // group Id of group to be shown
        string groupId;

        // components
        EditText groupNameEditText;
        EditText remarksEditText;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

            this.groupId = Arguments.GetString("groupId");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_item_group_details_view, container, false);

            this.groupNameEditText = view.FindViewById<EditText>(Resource.Id.groupNameEditText);
            this.remarksEditText = view.FindViewById<EditText>(Resource.Id.remarksEditText);

            Button saveUpdateBtn = view.FindViewById<Button>(Resource.Id.saveUpdateBtn);
            Button deleteGroupBtn = view.FindViewById<Button>(Resource.Id.deleteGroupBtn);

            saveUpdateBtn.Click += this.SaveUpdate_Click;
            deleteGroupBtn.Click += this.DeleteGroup_Click;

            return view;
        }

        private void DeleteGroup_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this.Activity);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Confirm Irreversible Action");
            alert.SetMessage("Are you sure in deleting this group? Deleting this group will delete items as well as their inventory records.");

            alert.SetButton("CONFIRM", async (c, ev) =>
            {
                // OK
                // Make api request
                ItemGroupService service = new ItemGroupService();

                HttpMessage message = await service.DeleteItemGroup(this.group.Id);

                Toast.MakeText(this.Activity, message.StatusCode + ". " + message.Message, ToastLength.Long).Show();

                // regardless of status code, always redirect back
                ((MainActivity)this.Activity).GoBackToItemGroups();
                
            });

            alert.SetButton2("CANCEL", (c, ev) => 
            {
                // do nothing
            });

            alert.Show();
        }

        private async void SaveUpdate_Click(object sender, EventArgs e)
        {
            if (this.Validate())
            {
                // update this.group model
                this.group.Name = this.groupNameEditText.Text.Trim();
                this.group.Remarks = this.remarksEditText.Text.Trim();

                ItemGroupService service = new ItemGroupService();

                HttpMessage message = await service.UpdateItemGroup(this.group);

                Toast.MakeText(this.Activity, message.StatusCode + ". " + message.Message, ToastLength.Long).Show();

                if (message.StatusCode == System.Net.HttpStatusCode.NotFound.ToString())
                {
                    // not found
                    // this current group might have been deleted during this time
                    // so go back to item groups view
                    ((MainActivity)this.Activity).GoBackToItemGroups();
                }
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(this.groupNameEditText.Text.Trim()))
            {
                Toast.MakeText(this.Activity, "Group name cannot be empty", ToastLength.Short).Show();
                return false;
            }

            return true;
        }

        public override async void OnResume()
        {
            base.OnResume();

            // Make API request
            ItemGroupService service = new ItemGroupService();

            this.group = await service.GetItemGroup(this.groupId);

            this.DisplayItemGroupData();
        }

        private void DisplayItemGroupData()
        {
            this.groupNameEditText.Text = this.group.Name;
            this.remarksEditText.Text = this.group.Remarks;
        }
    }
}