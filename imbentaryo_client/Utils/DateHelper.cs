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
    internal class DateHelper
    {
        /*
         * Converts sqlite3-converted date time in unix ms
         * to string formatted date time
         */
        public static DateTime ConvertToDateTime(string unixMS)
        {
            return (new DateTime(1970, 1, 1)).AddMilliseconds(double.Parse(unixMS));
        }
    }
}