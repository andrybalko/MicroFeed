using System;
using System.IO;
using MicroFeed.com.arybalko.model.vo;
using MicroFeed.com.arybalko.storage;
using MicroFeed.com.arybalko.views;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MicroFeed
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			SqLiteHelper.Connect();

			//Wrap content page to NavigationPage to provide back button and title
			MainPage = new NavigationPage(new FeedView());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		/// <summary>
		/// Prevent app suspension upon pressing back button on main screen on Android
		/// </summary>
		public bool AllowBack
		{
			get
			{
				NavigationPage mainPage = App.Current.MainPage as NavigationPage;
				if (mainPage != null)
				{
					return mainPage.Navigation.NavigationStack.Count > 1;
				}
				return true;
			}
		}
	}
}
