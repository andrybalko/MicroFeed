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
	public partial class DetailsView
	{
		public DetailsView ()
		{
			InitializeComponent ();
			NavigationPage.SetHasBackButton(this, true);
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			WebView.IsVisible = !WebView.IsVisible;
			Button.Text = WebView.IsVisible ? "Hide Target" : "View Target";
		}
	}
}