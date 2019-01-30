using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using MicroFeed.com.arybalko.model.vo;
using SQLite;

namespace MicroFeed.com.arybalko.storage
{
    class SqLiteHelper
    {
	    private static readonly SQLiteConnection Connection;

	    private static SQLiteConnection Db;

		/// <summary>
		/// Create database if not exist
		/// </summary>
		public static void Connect()
	    {
		    var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "rss.db");

		    Db = new SQLiteConnection(databasePath);
		    Db.CreateTable<FeedItem>();
		}

		/// <summary>
		/// Pull all feed items from database
		/// </summary>
		/// <returns></returns>
	    public static ObservableCollection<FeedItem> GetItems()
	    {
		    if (Db == null)
		    {
				Connect();
				return new ObservableCollection<FeedItem>();
		    }

		    return new ObservableCollection<FeedItem>(Db.Table<FeedItem>());
	    }

		/// <summary>
		/// Store all feed items to database
		/// </summary>
		/// <param name="items"></param>
	    public static void StoreItems(IEnumerable<FeedItem> items)
	    {
		    Db.InsertAll(items);
	    }

		/// <summary>
		/// Delete all stored feed items
		/// </summary>
	    public static void DeleteItems()
	    {
			Db.DeleteAll<FeedItem>();
		}
	}
}
