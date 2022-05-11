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
        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            groupNames = new List<string>();
            groupIds = new List<string>();

            // populate by making api request
            groupNames.Add("Abc1");
            groupIds.Add("Abc1id");

            groupNames.Add("Abc2");
            groupIds.Add("Abc2id");

            groupNames.Add("Abc3");
            groupIds.Add("Abc3id");

            groupNames.Add("Abc4");
            groupIds.Add("Abc4id");

            groupNames.Add("Abc5");
            groupIds.Add("Abc5id");

            ItemGroupService igs = new ItemGroupService();
            List<ItemGroup> itemGroups = await igs.GetItemGroups();
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

            // populate spinner
            ArrayAdapter spinnerAdapter = new ArrayAdapter<string>(this.Activity, Resource.Layout.support_simple_spinner_dropdown_item, this.groupNames);
            itemGroupDropDown.Adapter = spinnerAdapter;


            return view;
        }
    }
}