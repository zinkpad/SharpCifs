using System.Collections.Generic;

namespace SharpCifs.Util.Sharpen
{
    internal class EnumerableWrapper<T> : Iterable<T>
	{
		private IEnumerable<T> _e;

		public EnumerableWrapper (IEnumerable<T> e)
		{
			this._e = e;
		}

		public override Iterator<T> Iterator ()
		{
			return new EnumeratorWrapper<T> (_e, _e.GetEnumerator ());
		}
	}
}
