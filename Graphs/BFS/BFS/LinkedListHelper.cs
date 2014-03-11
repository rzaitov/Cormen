using System;
using System.Collections.Generic;

namespace Graphs
{
	public static class LinkedListHelper
	{
		public static LinkedList<T> Add<T>(this LinkedList<T> lList, T item)
		{
			lList.AddLast(item);
			return lList;
		}

		public static LinkedList<T> Add<T>(this LinkedList<T> lList, params T[] items)
		{
			foreach(var item in items)
				lList.AddLast(item);

			return lList;
		}
	}
}

