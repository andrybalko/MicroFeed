using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using MicroFeed.com.arybalko.model.vo;
using MicroFeed.com.arybalko.storage;
using Xamarin.Forms;

namespace MicroFeed.com.arybalko.viemodels
{
    class FeedViewModel:INotifyPropertyChanged
    {
	    private static readonly string FeedUrl = "https://news.microsoft.com/feed/";

		//Max number of attempts to load feed
	    private int _maxAttempts = 3;

		//Load attempts counter
	    private int _counter = 0;

	    private ObservableCollection<FeedItem> _feed;
	    private bool _isRefreshing;

		//Indicates updates receiving
	    public bool IsRefreshing
	    {
		    get => _isRefreshing;
		    set
		    {
			    _isRefreshing = value;
				OnPropertyChanged(nameof(IsRefreshing));
		    }
	    }

		/// <summary>
		/// Items collection pulled from RSS
		/// </summary>
	    public ObservableCollection<FeedItem> Feed
	    {
		    get => _feed ?? new ObservableCollection<FeedItem>();
		    set
		    {
			    _feed = value;
			    OnPropertyChanged(nameof(Feed));
			}
		}

	    public ICommand LoadFeedCommand { get; set; }


	    public ICommand RefreshFeedCommand { get; set; }

	    public FeedViewModel()
	    {
		    LoadFeedCommand = new Command(async () =>
		    {
			    IsRefreshing = true;

			    var local = SqLiteHelper.GetItems();

				//try to load feed from db. If db is empty then load remotely
				if (local.Count > 0)
			    {
				    Feed = new ObservableCollection<FeedItem>(local);
					IsRefreshing = false;
					return;
			    }


			    string res = string.Empty;
			    try
			    {
				    res = await PullFeed();
			    }
			    catch (HttpRequestException e)
			    {
				    Console.WriteLine(e);

					//increase attempt counter and repeat loading
				    if (_counter++ < _maxAttempts)
				    {
					    LoadFeedCommand.Execute(null);
					}
				    else
				    {
					    await CompleteAttempt();
					    return;
				    }
				}
			    catch (Exception ex)
			    {
				    Console.WriteLine(ex);
				    await CompleteAttempt();
					return;
			    }

			    _counter = 0;
				ParseFeed(res);
			    IsRefreshing = false;
		    });

			RefreshFeedCommand = new Command(() =>
			{
				SqLiteHelper.DeleteItems();
				LoadFeedCommand.Execute(null);
			});


			//make initial feed loading after app start
			if (Feed.Count == 0)
		    {
			    LoadFeedCommand.Execute(null);
			}
		}

	    private async Task CompleteAttempt()
	    {
			await App.Current.MainPage.DisplayAlert("Problem", "Can't load feed. Try again later", "Ok", "Cancel");
		    IsRefreshing = false;
	    }

		private async Task<string> PullFeed()
	    {
		    var client = new HttpClient();
		    return await client.GetStringAsync(FeedUrl);
	    }

		/// <summary>
		/// Parse feed into items collection
		/// </summary>
		/// <param name="rss"></param>
		private void ParseFeed(string rss)
	    {
		    var x = XDocument.Parse(rss);

			//hardcode this namespace because we need to get Creator value 
			//definitely, it can be done in a better way, but to be fair this is not required in this task, isn't it?
		    XNamespace dc = "http://purl.org/dc/elements/1.1/";

		    var res = from item in x.Descendants("item")
			    select new FeedItem()
			    {
				    Title = (string)item.Element("title"),
				    Description = (string)item.Element("description"),
				    Link = (string)item.Element("link"),
				    PubDate = (DateTime)item.Element("pubDate"),
				    Creator = (string)item.Element(dc + "creator"),
			    };

			
		    Feed = new ObservableCollection<FeedItem>(res);

			SqLiteHelper.StoreItems(res);
		}















		//simple INPC implementation
		public event PropertyChangedEventHandler PropertyChanged;
	    protected void OnPropertyChanged(string propertyName)
	    {
		    var handler = PropertyChanged;
		    handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }
	}
}
