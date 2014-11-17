namespace Rocketcore.Search
{
	public class SearchIndex : ISearchIndex
	{
		public SearchIndex(string indexName)
		{
			Name = indexName;
		}

		public string Name { get; private set; }
	}
}
