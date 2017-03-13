using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Collections.Generic;
using freshFridge.Resources.Model;
using freshFridge.Resources.DataHelper;
using freshFridge.Resources;
using Android.Util;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using System.Globalization;

namespace freshFridge
{
    [Activity(Label = "freshFridge", Theme = "@style/MyTheme", MainLauncher = true, Icon = "@drawable/icon")]
#pragma warning disable CS0618 // Type or member is obsolete
    public class MainActivity : ActionBarActivity
#pragma warning restore CS0618 // Type or member is obsolete
    {
        ListView lstData;
        List<Product> lstSource = new List<Product>();
        DataBase db;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "freshFridge";

            //Create DataBase
            db = new DataBase();
            db.createDataBase();
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            Log.Info("DB_PATH", folder);

            lstData = FindViewById<ListView>(Resource.Id.listView);

            var edtProdName = FindViewById<EditText>(Resource.Id.productName);
            var edtExpiryDate = FindViewById<DatePicker>(Resource.Id.expiryDate);

            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnDelete = FindViewById<Button>(Resource.Id.btnDelete);

            //LoadData
            LoadData();

            //Event
            btnAdd.Click += delegate
            {
                Product product = new Product() {
                    ProdName = edtProdName.Text,
                    ExpiryDate = edtExpiryDate.DateTime
                }; 
                db.insertIntoTableProduct(product);
                LoadData();
            };

            btnEdit.Click += delegate {
                Product product = new Product()
                {
                    Id=int.Parse(edtProdName.Tag.ToString()),
                    ProdName = edtProdName.Text,
                    ExpiryDate = edtExpiryDate.DateTime

                };
                db.updateTableProduct(product);
                LoadData();
            };

            btnDelete.Click += delegate {
                Product product = new Product()
                {
                    Id = int.Parse(edtProdName.Tag.ToString()),
                    ProdName = edtProdName.Text,
                    ExpiryDate = edtExpiryDate.DateTime
                };
                db.deleteTableProduct(product);
                LoadData();
            };

            lstData.ItemClick += (s,e) =>{
                //Set background for selected item
                for(int i = 0; i < lstData.Count; i++)
                {
                    if (e.Position == i)
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Azure);
                    else
                        lstData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);

                }

                //Binding Data
                var txtProdName = e.View.FindViewById<TextView>(Resource.Id.productName);
                var txtExpiryDate = e.View.FindViewById<TextView>(Resource.Id.expiryDate);

                edtProdName.Text = txtProdName.Text;
                edtProdName.Tag = e.Id;

                edtExpiryDate.DateTime = DateTime.ParseExact(txtExpiryDate.Text, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                
            };

        }

        private void LoadData()
        {
            lstSource = db.selectTableProduct();
            var adapter = new ListViewAdapter(this, lstSource);
            lstData.Adapter = adapter;
        }
    }
}