using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using imbentaryo_client.Http;
using imbentaryo_client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imbentaryo_client
{
    [Activity(Label = "Login")]
    public class LoginActivity : Activity
    {
        EditText usernameEditText;
        EditText passwordEditText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            // Create your application here
            Button loginBtn = FindViewById<Button>(Resource.Id.loginBtn);
            loginBtn.Click += Login_Click;

            TextView signUpLink = FindViewById<TextView>(Resource.Id.signupLink);
            signUpLink.Click += SignUpLink_Click;

            usernameEditText = FindViewById<EditText>(Resource.Id.usernameEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);

            // clear stored preference/session so that when the login view opens its a fresh start
            // or so that when the user clicks back button
            ISharedPreferences session = Application.Context.GetSharedPreferences("UserSession", FileCreationMode.Private);
            ISharedPreferencesEditor edit = session.Edit();
            edit.PutString("accountId", String.Empty);
            edit.PutString("username", String.Empty);
            edit.Apply();
        }

        private void SignUpLink_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SignUpActivity));
            StartActivity(intent);
        }

        private async void Login_Click(object sender, EventArgs e)
        {
            if (this.Validate())
            {
                AccountService service = new AccountService();

                LoginModel loginModel = await service.LoginUser(usernameEditText.Text.Trim(), passwordEditText.Text.Trim());

                Toast.MakeText(this, loginModel.Message, ToastLength.Short).Show();

                if (loginModel.StatusCode == System.Net.HttpStatusCode.OK.ToString())
                {
                    ISharedPreferences session = Application.Context.GetSharedPreferences("UserSession", FileCreationMode.Private);
                    ISharedPreferencesEditor edit = session.Edit();
                    edit.PutString("accountId", loginModel.AccountId);
                    edit.PutString("username", loginModel.Username);
                    edit.Apply();

                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }            
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(usernameEditText.Text.Trim()))
            {
                Toast.MakeText(this, "Username is required", ToastLength.Short).Show();
                return false;
            }
            else if (string.IsNullOrEmpty(passwordEditText.Text.Trim()))
            {
                Toast.MakeText(this, "Password is required", ToastLength.Short).Show();
                return false;
            }

            return true;
        }
    }
}