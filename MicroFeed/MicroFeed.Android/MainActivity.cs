using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using LabelHtml.Forms.Plugin.Droid;
using Xamarin.Forms;
using Application = Xamarin.Forms.Application;

namespace MicroFeed.Droid
{
    [Activity(Label = "MicroFeed", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, 
	    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
	        HtmlLabelRenderer.Initialize();
			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

		/// <summary>
		/// Piece of logic to prevent app suspension upon pressing Back button on main screen
		/// </summary>
	    public override void OnBackPressed()
	    {
			
		    if (((App)App.Current).AllowBack)
		    {
			    base.OnBackPressed();
		    }
	    }

	}
}