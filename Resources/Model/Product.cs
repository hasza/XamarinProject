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
using SQLite;

namespace freshFridge.Resources.Model
{
    public class Product
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string ProdName { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}