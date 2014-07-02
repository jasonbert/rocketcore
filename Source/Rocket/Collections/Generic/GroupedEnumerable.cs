using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Collections.Generic
{
	public class GroupedEnumerable<T> : IEnumerable<T>
	{
		public class ChildEnumerator : IEnumerator<T>
		{
			GroupedEnumerable<T> parent;
			int position;
			bool done = false;
			T current;

			public ChildEnumerator(GroupedEnumerable<T> parent)
			{
				this.parent = parent;
				position = -1;
				parent._wrapper.AddRef();
			}

			public T Current
			{
				get
				{
					if (position == -1 || done)
					{
						throw new InvalidOperationException();
					}
					return current;

				}
			}

			public void Dispose()
			{
				if (!done)
				{
					done = true;
					parent._wrapper.RemoveRef();
				}
			}

			object System.Collections.IEnumerator.Current
			{
				get { return Current; }
			}

			public bool MoveNext()
			{
				position++;

				if (position + 1 > parent._groupSize)
				{
					done = true;
				}

				if (!done)
				{
					done = !parent._wrapper.Get(position + parent._start, out current);
				}

				return !done;

			}

			public void Reset()
			{
				// per http://msdn.microsoft.com/en-us/library/system.collections.ienumerator.reset.aspx
				throw new NotSupportedException();
			}
		}

		private EnumeratorWrapper<T> _wrapper;
		private int _groupSize;
		private int _start;

		public GroupedEnumerable(EnumeratorWrapper<T> wrapper, int groupSize, int start)
		{
			_wrapper = wrapper;
			_groupSize = groupSize;
			_start = start;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new ChildEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

	}
}
