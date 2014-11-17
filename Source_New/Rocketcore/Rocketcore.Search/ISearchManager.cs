using Sitecore.ContentSearch;

namespace Rocketcore.Search
{
	public interface ISearchManager
	{
		ISearchIndex MasterIndex { get; }
		ISearchIndex WebIndex { get; }
		ISearchIndex CoreIndex { get; }
		ISearchIndex IndexContext { get; }
		ISearchIndex GetIndex(string databaseName);
		IProviderSearchContext SearchContext { get; }
		IProviderSearchContext CreateContext(ISearchIndex index);
	}
}
