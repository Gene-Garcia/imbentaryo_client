using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace imbentaryo_client
{
    [Activity(Label = "Sign Up")]
    public class SignUpActivity : Activity
    {
        EditText usernameEditText;
        EditText passwordEditText;
        EditText confirmPasswordEditText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_sign_up);

            // Create your application here
            Button signUpBtn = FindViewById<Button>(Resource.Id.signUpBtn);
            signUpBtn.Click += SignUp_Click;

            TextView loginLink = FindViewById<TextView>(Resource.Id.loginLink);
            loginLink.Click += LoginLink_Click;

            usernameEditText = FindViewById<EditText>(Resource.Id.usernameEditText);
            passwordEditText = FindViewById<EditText>(Resource.Id.passwordEditText);
            confirmPasswordEditText = FindViewById<EditText>(Resource.Id.confirmPasswordEditText);
        }

        private void LoginLink_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            if (this.Validate())
            {
                Intent intent = new Intent(this, typeof(LoginActivity));
                StartActivity(intent);
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
            else if (string.IsNullOrEmpty(confirmPasswordEditText.Text.Trim()))
            {
                Toast.MakeText(this, "Confirm password is required", ToastLength.Short).Show();
                return false;
            }
            else if (passwordEditText.Text.Trim() != confirmPasswordEditText.Text.Trim())
            {
                Toast.MakeText(this, "Password and confirm password does not match", ToastLength.Short).Show();
                return false;
            }
            return true;
        }
    }
}