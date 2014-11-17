[assembly: WebActivator.PostApplicationStartMethod(typeof(Rocketcore.Website.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace Rocketcore.Website.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Extensions;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
	using Fortis.Model;
	using Fortis.Providers;
	using Fortis.Mvc.Providers;
	using Rocketcore.Model;
	using Fortis.Search;
	using Fortis.Search.Lucene;
	using Rocketcore.Search;
	using Rocketcore.Content.Global;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
			InitialiseSearch(container);
			InitialiseContentGlobal(container);
			InitialiseFortis(container);
        }

		private static void InitialiseFortis(Container container)
		{
			// Register Fortis
			container.Register<IItemFactory, CustomItemFactory>();
			container.Register<IContextProvider, ContextProvider>();
			container.Register<ISpawnProvider, SpawnProvider>();
			container.Register<ITemplateMapProvider, TemplateMapProvider>();
			container.Register<IModelAssemblyProvider, ModelAssemblyProvider>();
			container.Register<ISearchProvider, SearchProvider>();
			container.Register<IItemSearchFactory, ItemSearchFactory>();

			// Initialise fortis for pipelines and events
			Fortis.Global.Initialise(
				container.GetInstance<ISpawnProvider>(),
				container.GetInstance<IItemFactory>()
			);
		}

		private static void InitialiseSearch(Container container)
		{
			container.Register(typeof(ISearchManager), () => new SearchManager(
					new SearchIndex("sitecore_master_index"),
					new SearchIndex("sitecore_core_index"),
					new SearchIndex("sitecore_web_index")
				), Lifestyle.Singleton);
		}

		private static void InitialiseContentGlobal(Container container)
		{
			container.Register<IAggregateManager, AggregateManager>();
		}
    }
}