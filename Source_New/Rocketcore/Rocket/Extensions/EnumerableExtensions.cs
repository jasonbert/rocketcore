using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rocket.Collections.Generic;

namespace Rocket.Extensions
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<IEnumerable<T>> Group<T>(this IEnumerable<T> source, int groupSize)
		{
			if (groupSize < 1) throw new InvalidOperationException();

			var wrapper = new EnumeratorWrapper<T>(source);

			int currentPos = 0;
			T ignore;

			try
			{
				wrapper.AddRef();
				while (wrapper.Get(currentPos, out ignore))
				{
					yield return new GroupedEnumerable<T>(wrapper, groupSize, currentPos);
					currentPos += groupSize;
				}
			}
			finally
			{
				wrapper.RemoveRef();
			}
		}
	}
}
