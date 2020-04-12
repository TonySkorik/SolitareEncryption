using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solitare.Core.Algorithm
{
	public class LinkedListSwapper<T, TKey>
	{
		private LinkedList<T> _linkedList;
		private Dictionary<TKey, T> _nodeValuesIndex;

		public LinkedListSwapper(LinkedList<T> linkedList, Func<T, TKey> keySelector)
		{
			_linkedList = linkedList;
			_nodeValuesIndex = _linkedList.ToDictionary(keySelector, n => n);
		}

		public void MoveUp(string key)
		{

		}

		public void MoveDown(string key)
		{

		}
	}
}
