using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TestApp
{
    public class OfferAdapter : BaseAdapter<XElement>
    {
        private List<XElement> list;
        private Context context;
        private ListView viewheader;
        public OfferAdapter(Context context, List<XElement> list,ListView view)
        {
            this.list = list;
            this.context = context;
            this.viewheader = view;
        }

        public override int Count
        {
            get { return list.Count; }
        }
        public override XElement this[int index]
        {
            get { return list[index]; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
            {
                view = LayoutInflater.FromContext(context).Inflate(Resource.Layout.OfferItem, null, false);
                TextView textView = view.FindViewById<TextView>(Resource.Id.textView1);
                textView.Text = list[position].Attribute("id").Value;
            }
            return view;
        }
    }
}