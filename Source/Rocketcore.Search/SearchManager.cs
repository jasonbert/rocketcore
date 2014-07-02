using Sitecore.ContentSearch;

namespace Rocketcore.Search
{
	public class SearchManager : ISearchManager
	{
		private readonly ISearchIndex _masterIndex;
		private readonly ISearchIndex _coreIndex;
		private readonly ISearchIndex _webIndex;

		public SearchManager(ISearchIndex masterIndex, ISearchIndex coreIndex, ISearchIndex webIndex)
		{
			_masterIndex = masterIndex;
			_coreIndex = coreIndex;
			_webIndex = webIndex;
		}

		public ISearchIndex MasterIndex
		{
			get { return _masterIndex; }
		}

		public ISearchIndex WebIndex
		{
			get { return _webIndex; }
		}

		public ISearchIndex CoreIndex
		{
			get { return _coreIndex; }
		}

		public ISearchIndex IndexContext { get { return GetIndex(_contextDatabaseName); } }

		public ISearchIndex GetIndex(string databaseName)
		{
			switch (databaseName)
			{
				case "master":
					return MasterIndex;
				case "core":
					return CoreIndex;
				case "web":
				default:
					return WebIndex;
			}
		}


		public IProviderSearchContext SearchContext
		{
			get { return CreateContext(IndexContext); }
		}

		public IProviderSearchContext CreateContext(ISearchIndex index)
		{
			return ContentSearchManager.GetIndex(index.Name).CreateSearchContext(); 
		}

		private string _contextDatabaseName { get { return Sitecore.Context.Database.Name; } }
	}
}
