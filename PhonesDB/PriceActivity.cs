using Android.App;
using Android.OS;
using Android.Widget;
using PhonesDB;
using System.Linq;
using Warehouse.DB;

namespace Warehouse
{
    [Activity(Label = "PriceActivity")]
    public class PriceActivity : Activity
    {
        private WarehousesDBHelper _dBHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_price);

            _dBHelper = new WarehousesDBHelper(this);
            _dBHelper.OnUpgrade(_dBHelper.WritableDatabase, 1, 2);

            var products = _dBHelper.GetAllProducts();
            var minPriceProduct = products.OrderBy(p => p.Price).First();
            var maxPriceProduct = products.OrderByDescending(p => p.Price).First();

            var minPriceTextView = FindViewById<TextView>(Resource.Id.minPrice);
            minPriceTextView.Text = $"Товар з мінімальною ціною: {minPriceProduct.ProductName}, Ціна: {minPriceProduct.Price}";

            var maxPriceTextView = FindViewById<TextView>(Resource.Id.maxPrice);
            maxPriceTextView.Text = $"Товар з максимальною ціною: {maxPriceProduct.ProductName}, Ціна: {maxPriceProduct.Price}";
        }
    }
}
