using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using imbentaryo_client.Fragments;
using imbentaryo_client.Http;
using imbentaryo_client.Models;
using Newtonsoft.Json;

/*
 * nav_header_main.xml
 * Is the header layout of the drawer
 *
 * activity_main.xml
 * is the main layout instantiated by MainActivity.
 * it calls/includes the app_bar_main
 *
 * app_bar_main.xml
 * creates the google toolbar and FAB.
 * it calls/includes the app_content_main
 *
 * content_main.xml
 * is the layout where we usually put the components of the main activity.
 *
 * Resource.Id.toolbar
 * & Resource.Id.fab
 * located in app_bar_main.xml
 *
 * Resource.Id.drawer_layout
 * is the actual parent container in activity_main.xml
 *
 * Resource.Id.nav_view
 * located in activity_main.xml and is the container to the drawer itself that appears and disappears 
 *
 */
namespace imbentaryo_client
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    internal class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            // This will serve as our session
            ISharedPreferences session = Application.Context.GetSharedPreferences("UserSession", FileCreationMode.Private);
            string accountId = session.GetString("accountId", String.Empty);
            string username = session.GetString("username", String.Empty);

            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(username))
            {
                Intent intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
            }
            else
            {
                AndroidX.AppCompat.Widget.Toolbar toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
                SetSupportActionBar(toolbar);

                FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
                fab.Click += FabOnClick;

                DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
                drawer.AddDrawerListener(toggle);
                toggle.SyncState();

                NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                navigationView.SetNavigationItemSelectedListener(this);

                // configure navigation header
                LinearLayout header = (LinearLayout)navigationView.GetHeaderView(0);
                TextView navHeaderUsername = header.FindViewById<TextView>(Resource.Id.navHeaderUsername);
                navHeaderUsername.Text = username;

                // fragments configuration - AndroidX
                var tx = this.SupportFragmentManager.BeginTransaction();
                tx.Add(Resource.Id.fragmentContainer, new FragmentItemsView());
                tx.Commit();
            }
        }

        protected override void OnRestart()
        {
            base.OnRestart();

            // check for shared preference if user is still in session
            // This will serve as our session
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserSession", FileCreationMode.Private);
            string accountId = pref.GetString("accountId", String.Empty);
            string username = pref.GetString("username", String.Empty);

            if (string.IsNullOrEmpty(accountId) || string.IsNullOrEmpty(username))
            {
                Intent intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            // clear shared preferences
            ISharedPreferences session = Application.Context.GetSharedPreferences("UserSession", FileCreationMode.Private);
            ISharedPreferencesEditor edit = session.Edit();
            edit.PutString("accountId", String.Empty);
            edit.PutString("username", String.Empty);
            edit.Apply();
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            //else if (SupportFragmentManager.BackStackEntryCount > 0)
            //{
            //    SupportFragmentManager.PopBackStack();

            //    // Clicking back will reopen last Fragment
            //    //this.activeFragment = this.stackFragment.Pop();
            //}
            else
            {
                base.OnBackPressed();
            }
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            //View view = (View) sender;
            //Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
            //    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();

            AndroidX.Fragment.App.Fragment fragment = new FragmentAddItem();
            ChangeFragment(fragment);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            AndroidX.Fragment.App.Fragment fragment = null;

            int id = item.ItemId;

            if (id == Resource.Id.nav_home)
            {
                fragment = new FragmentItemsView();
            }
            else if (id == Resource.Id.nav_items_view)
            {
                fragment = new FragmentItemsView();
            }
            else if (id == Resource.Id.nav_add_item)
            {
                fragment = new FragmentAddItem();
            }
            else if (id == Resource.Id.nav_add_item_group)
            {
                fragment = new FragmentAddItemGroup();
            }
            else if (id == Resource.Id.nav_item_groups_view)
            {
                fragment = new FragmentItemGroupsView();
            }
            else if (id == Resource.Id.nav_sign_out)
            {
                // clear shared preferences
                ISharedPreferences session = Application.Context.GetSharedPreferences("UserSession", FileCreationMode.Private);
                ISharedPreferencesEditor edit = session.Edit();
                edit.PutString("accountId", String.Empty);
                edit.PutString("username", String.Empty);
                edit.Apply();

                // Redirect to login view
                Intent intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);

                // so that no fragment will be opened
                return true;
            }

            //var tx = this.SupportFragmentManager.BeginTransaction();
            //tx.Replace(Resource.Id.fragmentContainer, fragment);
            //// add the transaction to the backstack to allow users to navigate back
            //tx.AddToBackStack(null);
            //tx.Commit();

            this.ChangeFragment(fragment);

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void ChangeFragment(AndroidX.Fragment.App.Fragment fragment)
        {
            var tx = this.SupportFragmentManager.BeginTransaction();
            tx.Replace(Resource.Id.fragmentContainer, fragment);
            // add the transaction to the backstack to allow users to navigate back
            tx.AddToBackStack(null);
            tx.Commit();
        }

        /*
         * Function methods are fragment callables which is designed
         * to change the current active fragment. Event happen on item
         * list click
         */
        public void StartItemInventoryDetailView(string itemId)
        {
            AndroidX.Fragment.App.Fragment fragment = new FragmentItemDetailsView();

            // configure argument
            Bundle args = new Bundle();
            args.PutString("itemId", itemId);
            fragment.Arguments = args;

            this.ChangeFragment(fragment);

            //var tx = this.SupportFragmentManager.BeginTransaction();
            //tx.Replace(Resource.Id.fragmentContainer, fragment);
            //// add the transaction to the backstack to allow users to navigate back
            //tx.AddToBackStack(null);
            //tx.Commit();
        }

        /*
         * Shows details of a specific group
         * 
         */
        public void StartItemGroupDetailView(string groupId)
        {
            AndroidX.Fragment.App.Fragment fragment = new FragmentItemGroupDetailsView();

            // argument
            Bundle args = new Bundle();
            args.PutString("groupId", groupId);
            fragment.Arguments = args;

            this.ChangeFragment(fragment);
        }

        /*
         * Start FragmentItemsView with a payload/bundle/parameter of
         * which items of item group only to show. Triggered by selecting the 
         * item-groups view buttons
         */
        public void StartItemInventoriesOfGroup(string groupId, string groupName)
        {
            AndroidX.Fragment.App.Fragment fragment = new FragmentItemsView();

            // configure argument
            Bundle args = new Bundle();
            args.PutString("itemGroupId", groupId);
            args.PutString("groupName", groupName);
            fragment.Arguments = args;

            this.ChangeFragment(fragment);
        }

        public void GoBackToItemGroups()
        {
            AndroidX.Fragment.App.Fragment fragment = new FragmentItemGroupsView();

            this.ChangeFragment(fragment);
        }
    }
}

