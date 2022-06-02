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

namespace imbentaryo_client.Utils
{
    internal class UserSession
    {
        public ISharedPreferences session;
        
        public UserSession()
        {
            session = Application.Context.GetSharedPreferences("UserSession", FileCreationMode.Private);
        }

        public string GetAccountId()
        {
            return session.GetString("accountId", String.Empty);
        }

        public string GetAccountUsername()
        {
            return session.GetString("username", String.Empty);
        }
    }
}