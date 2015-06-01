using System.Collections.Generic;

namespace SharpCifs.Util.Sharpen
{
    internal class CopyOnWriteArrayList<T> : Iterable<T>, IList<T>
	{
		private List<T> _list;

		public CopyOnWriteArrayList ()
		{
			_list = new List<T> ();
		}

		public virtual void Add (T element)
		{
			lock (_list) {
				List<T> newList = new List<T> (_list);
				newList.Add (element);
				_list = newList;
			}
		}

		public virtual void Add (int index, T element)
		{
			lock (_list) {
				List<T> newList = new List<T> (_list);
				newList.Insert (index, element);
				_list = newList;
			}
		}

		public virtual void Clear ()
		{
			lock (_list) {
				_list = new List<T> ();
			}
		}

		public virtual T Get (int index)
		{
			return _list[index];
		}

		public override Iterator<T> Iterator ()
		{
			return new EnumeratorWrapper<T> (_list, _list.GetEnumerator ());
		}

		public virtual T Remove (int index)
		{
			lock (_list) {
				T old = _list[index];
				List<T> newList = new List<T> (_list);
				newList.RemoveAt (index);
				_list = newList;
				return old;
			}
		}

		public virtual T Set (int index, T element)
		{
			lock (_list) {
				T old = _list[index];
				List<T> newList = new List<T> (_list);
				newList[index] = element;
				_list = newList;
				return old;
			}
		}

		bool ICollection<T>.Contains (T item)
		{
			return _list.Contains (item);
		}

		void ICollection<T>.CopyTo (T[] array, int arrayIndex)
		{
			_list.CopyTo (array, arrayIndex);
		}

		bool ICollection<T>.Remove (T item)
		{
			lock (_list) {
				List<T> newList = new List<T> (_list);
				bool removed = newList.Remove (item);
				_list = newList;
				return removed;
			}
		}

		int IList<T>.IndexOf (T item)
		{
			int num = 0;
			foreach (T t in this) {
				if (ReferenceEquals (t, item) || t.Equals (item))
					return num;
				num++;
			}
			return -1;
		}

		void IList<T>.Insert (int index, T item)
		{
			Add (index, item);
		}

		void IList<T>.RemoveAt (int index)
		{
			Remove (index);
		}

		public virtual int Count {
			get { return _list.Count; }
		}

		public T this[int n] {
			get { return Get (n); }
			set { Set (n, value); }
		}

		bool ICollection<T>.IsReadOnly {
			get { return false; }
		}
	}
}
