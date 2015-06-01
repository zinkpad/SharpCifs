using System.Collections.Generic;

namespace SharpCifs.Util.Sharpen
{
    public class ListIterator<T>
	{
		private IList<T> _list;
		private int _pos;

		public ListIterator (IList<T> list, int n)
		{
			this._list = list;
			_pos = n;
		}

		public bool HasPrevious ()
		{
			return (_pos > 0);
		}

	    public bool HasNext()
	    {
	        return (_pos < _list.Count);
	    }

	    public object Next()
	    {
	        object current = _list[_pos];
	        _pos++;

	        return current;
	    } 

		public T Previous ()
		{
			_pos--;
			return _list[_pos];
		}
	}
}
