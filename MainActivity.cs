using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Widget;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using Android.Util;
using System.Threading;
using Android.Views;
using Android.Content;
using Newtonsoft.Json;

namespace TestApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ListView listView1;
        private List<XElement> list;
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            list = await GetAsync("http://partner.market.yandex.ru/pages/help/YML.xml");
            listView1 = FindViewById<ListView>(Resource.Id.listView1);
            OfferAdapter offerAdapter = new OfferAdapter(this, list,listView1);
            listView1.Adapter = offerAdapter;
            listView1.ItemClick += ListView1_ItemClick;
        }

        private void ListView1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(SecondActivity));
            string json = Newtonsoft.Json.JsonConvert.SerializeXNode(list[e.Position],Newtonsoft.Json.Formatting.Indented);
            json = Regex.Replace(json, @"\s+""#text""([\s\S]+?)],", "");
            intent.PutExtra("Offer", json);
            StartActivity(intent);
        }

        async Task<List<XElement>> GetAsync(string URL)
        {
            List<XElement> xElements = new List<XElement>();
            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse,
                Async = true
            };
            try
            {
                XmlReader reader = XmlReader.Create(URL, settings);
                XDocument xdoc = await XDocument.LoadAsync(reader, LoadOptions.PreserveWhitespace, CancellationToken.None);
                XElement? element = xdoc.Root.Element("shop").Element("offers");
                IEnumerable<XElement> offers = element.Elements();
                foreach (var offer in offers)
                {
                    xElements.Add(offer);
                    string name = offer.Attribute("id").ToString();
                    Log.Info("myapp", name);
                }
            }
            catch
            {
                return xElements;
            }
            return xElements;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}