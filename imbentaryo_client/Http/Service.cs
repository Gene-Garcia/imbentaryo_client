using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using imbentaryo_client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace imbentaryo_client.Http
{
    internal class Service
    {
        protected const string END_POINT = "http://192.168.1.11:3001/v1";
        //protected const string END_POINT = "https://imbentaryo.herokuapp.com/v1";

        protected HttpClient client; 

        public Service()
        {
            
        }

        /*
         * This service function handles the configuration
         * of the authorization bearer token. So that every request to
         * the servier will send the user account id
         * 
         */
        public void ConfigureAuthorization()
        {
            string userAccountId = new UserSession().GetAccountId();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", userAccountId);
        }
    }
}