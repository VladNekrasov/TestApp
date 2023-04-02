using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft;

namespace TestApp
{
    [Activity(Label ="Json предстваление")]
    public class SecondActivity:Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_second);
            TextView textView = FindViewById<TextView>(Resource.Id.secondtextView1);
            string offer = Intent.GetStringExtra("Offer");
            textView.Text=offer;
        }
    }
}