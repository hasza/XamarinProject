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
using freshFridge.Resources.Model;
using Java.Lang;
using Android.Graphics;

namespace freshFridge.Resources
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtProdName { get; set; }
        //public TextView txtType { get; set; }
        public DatePickerDialog txtExpiryDate { get; set; }
        public TextView txtDaysToDeath { get; set; }
    }
    public class ListViewAdapter:BaseAdapter
    {
        private Activity activity;
        private List<Product> lstProduct;
        public ListViewAdapter(Activity activity, List<Product> lstProduct)
        {
            this.activity = activity;
            this.lstProduct = lstProduct.OrderBy(x => x.ExpiryDate).ToList();
        }

        public override int Count
        {
            get
            {
                return lstProduct.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return lstProduct[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.list_view_dataTemplate, parent, false);

            var txtProdName = view.FindViewById<TextView>(Resource.Id.productName);
            var txtExpiryDate = view.FindViewById<TextView>(Resource.Id.expiryDate);
            var txtDaysToDeath = view.FindViewById<TextView>(Resource.Id.daysToDeath);
            int daysToDeath = (lstProduct[position].ExpiryDate - DateTime.Now).Days+1;

            txtProdName.Text = lstProduct[position].ProdName;
            txtExpiryDate.Text = (
                + lstProduct[position].ExpiryDate.Day + "/"
                + lstProduct[position].ExpiryDate.Month + "/"
                + lstProduct[position].ExpiryDate.Year
                ).ToString();
            if (daysToDeath <= 3)
            {
                txtDaysToDeath.SetBackgroundColor(Color.Red);
            }
            else if(daysToDeath <= 7)
            {
                txtDaysToDeath.SetBackgroundColor(Color.Orange);
            }
            else
            {
                txtDaysToDeath.SetBackgroundColor(Color.LawnGreen);
            }
            txtDaysToDeath.Text = "Pozosta³o: " + daysToDeath.ToString()+ " dni";

            return view;
        }
    }
}