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
            throw new NotImplementedException();
        }

        private void SaveUpdate_Click(object sender, EventArgs e)
        {
            if (this.Validate())
            {
                ItemGroupService service = new ItemGroupService();

                Toast.MakeText(this.Activity, "Success", ToastLength.Short).Show();
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