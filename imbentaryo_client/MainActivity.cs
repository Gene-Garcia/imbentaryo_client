using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using imbentaryo_client.Fragments;

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
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {

        // Start Fragment related components
        AndroidX.Fragment.App.Fragment addItem;
        AndroidX.Fragment.App.Fragment itemsView;

        // keeps hold of current shown fragment
        AndroidX.Fragment.App.Fragment activeFragment;

        // so that in back button, we will be able to push and pop to updated the active fragment
        // helps in showing the old active fragment when back button is pressed.
        Stack<AndroidX.Fragment.App.Fragment> stackFragment;
        // End

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            // fragments configuration - AndroidX
            addItem = new FragmentAddItem();
            itemsView = new FragmentItemsView();
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.fragmentContainer, addItem, "Add_Item");
            trans.Add(Resource.Id.fragmentContainer, itemsView, "Items_View");
            this.activeFragment = itemsView;
            trans.Hide(addItem);
            trans.Commit();
            this.stackFragment = new Stack<AndroidX.Fragment.App.Fragment>();
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                SupportFragmentManager.PopBackStack();

                // Clicking back will reopen last Fragment
                this.activeFragment = this.stackFragment.Pop();
            }
            else
            {
                base.OnBackPressed();
            }
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_home)
            {
                this.ShowFragment(this.itemsView);
            }
            else if (id == Resource.Id.nav_items_view)
            {
                this.ShowFragment(this.itemsView);
            }
            else if (id == Resource.Id.nav_add_item)
            {
                this.ShowFragment(this.addItem);
            }
            else if (id == Resource.Id.nav_add_item_group)
            {

            }
            else if (id == Resource.Id.nav_item_groups_view)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void ShowFragment(AndroidX.Fragment.App.Fragment fragment)
        {
            var transaction = SupportFragmentManager.BeginTransaction();

            transaction.Hide(this.activeFragment);
            transaction.Show(fragment);
            transaction.AddToBackStack(null);
            transaction.Commit();

            // we record the current fragment because it will be replaced.
            this.stackFragment.Push(this.activeFragment);

            this.activeFragment = fragment;
        }
    }
}

