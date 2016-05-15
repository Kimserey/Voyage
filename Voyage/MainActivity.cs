using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Voyage
{
    [Activity(Label = "Voyage", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new Voyage.Mobie.Main.App());
        }
    }
}

