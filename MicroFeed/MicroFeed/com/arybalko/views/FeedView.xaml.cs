using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MicroFeed.com.arybalko.views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FeedView
	{
		public FeedView ()
		{
			InitializeComponent ();
			NavigationPage.SetHasNavigationBar(this, false);
		}

		private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var page = new DetailsView()
			{
				BindingContext = ((ListView)sender).SelectedItem
			};
			App.Current.MainPage.Navigation.PushAsync(page, true);
		}
	}
}