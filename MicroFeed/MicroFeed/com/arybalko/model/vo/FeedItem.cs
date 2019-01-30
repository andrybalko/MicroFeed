using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using SQLite;

namespace MicroFeed.com.arybalko.model.vo
{
	
	class FeedItem
    {
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string Link { get; set; }

		public DateTime PubDate { get; set; }

		public string Creator { get; set; }
    }

}
