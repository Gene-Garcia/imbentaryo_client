using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace imbentaryo_client.Http
{
    internal class Service
    {
        protected const string END_POINT = "http://192.168.1.14:3001/v1";

        protected HttpClient client; 

        public Service()
        {
            
        }
    }
}