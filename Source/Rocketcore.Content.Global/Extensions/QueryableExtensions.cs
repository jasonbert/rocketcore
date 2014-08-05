using Fortis.Model;
using Rocketcore.Search;
using Rocketcore.Model.Templates.UserDefined;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Fortis.Search;
using System.Collections;

namespace Rocketcore.Content.Global.Extensions
{
	public static class QueryableExtensions
	{
		public static IQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IOrderingOptions options)
			where TSource : IItemWrapper
		{
			if (source != null && options != null)
			{
				var orderBy = options.OrderBy.ItemId;

				if (orderBy.Equals(Model.Static.ScSystem.PresentationSettings.FilterOptions.OrderBy.Alphabetic.Id))
				{
					var orderDirection = options.OrderDirection.ItemId;

					if (orderDirection.Equals(Model.Static.ScSystem.PresentationSettings.FilterOptions.OrderDirection.Ascending.Id))
					{
						source = source.OrderBy(keySelector);
					}
					else
					{
						source = source.OrderByDescending(keySelector);
					}
				}
			}

			return source;
		}

		public static IQueryable<TSource> Take<TSource>(this IQueryable<TSource> source, IPaginationOptions options)
			where TSource : IItemWrapper
		{
			if (source != null && options != null)
			{
				var itemsPerPage = 0;

				try
				{
					itemsPerPage = Convert.ToInt32(options.ItemsPerPage.Value);
				}
				catch { }

				if (itemsPerPage > 0)
				{
					source = source.Take(itemsPerPage);
				}
			}

			return source;
		}

        /// <summary>
        /// Aggregate the content as defined in the options. Will only aggregate by tags if tags is selected.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="source"></param>
        /// <param name="tagsSelector"></param>
        /// <param name="options"></param>
        /// <returns></returns>
		public static IQueryable<TSource> AggregateByOptions<TSource, TKey, TOptions>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> tagsSelector, Expression<Func<TSource, TKey>> categoriesSelector, TOptions options)
            where TSource : IItemWrapper
            where TKey : IEnumerable
            where TOptions : IAggregateOptions, ITaggingFilterOptions, ICategoryFilterOptions
		{
		    if (source != null)
		    {
                var aggregateBy = options.AggregateBy.ItemId;

                if (aggregateBy.Equals(Model.Static.ScSystem.PresentationSettings.FilterOptions.AggregateBy.Children.Id) ||
                    aggregateBy.Equals(Model.Static.ScSystem.PresentationSettings.FilterOptions.AggregateBy.Siblings.Id))
                {
                    return source;
                }
                else if (aggregateBy.Equals(Model.Static.ScSystem.PresentationSettings.FilterOptions.AggregateBy.Tags.Id))
                {
                    return AggregateByTags(source, tagsSelector, options);
                }
                else if (aggregateBy.Equals(Model.Static.ScSystem.PresentationSettings.FilterOptions.AggregateBy.Categories.Id))
                {
                    return AggregateByCategories(source, categoriesSelector, options);
                }
                else
                {
                    // if no aggregate by is selected then aggregate by both tags and categories.
                    //source = AggregateByTags(source, tagsSelector, options);
                    //source =  AggregateByCategories(source, categoriesSelector, options);
                }
		    }
		    return source;
		}

		public static IQueryable<TSource> AggregateByTags<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, ITaggingFilterOptions options)
			where TSource : IItemWrapper
			where TKey : IEnumerable
		{
			if (source != null)
			{
				var tags = options.Tags.Value;
                
				if (tags.Any())
				{
					source = source.AggregateBy(keySelector, tags, options.GroupTagsBy.ItemId);
				}
			}

			return source;
		}

		public static IQueryable<TSource> AggregateByCategories<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, ICategoryFilterOptions options)
			where TSource : IItemWrapper
			where TKey : IEnumerable
		{
			if (source != null)
			{
				var categories = options.Categories.Value;

				if (categories.Any())
				{
					source = source.AggregateBy(keySelector, categories, options.GroupCategoriesBy.ItemId);
				}
			}

			return source;
		}

		public static IQueryable<TSource> AggregateBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, IEnumerable values, Guid groupById)
			where TSource : IItemWrapper
			where TKey : IEnumerable
		{
			if (source != null)
			{
				if (groupById.Equals(Model.Static.ScSystem.PresentationSettings.FilterOptions.GroupBy.ContainsAll.Id))
				{
					source = source.ContainsAnd(keySelector, values);
				}
				else
				{
					source = source.ContainsOr(keySelector, values);
				}
			}

			return source;
		}
	}
}
