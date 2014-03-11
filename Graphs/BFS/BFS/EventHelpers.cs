using System;

namespace Graphs
{
	public static class EventHelpers
	{
		public static void SafeInvoke(Action action)
		{
			if(action != null)
				action();
		}

		public static void SafeInvoke<T>(Action<T> action, T p)
		{
			if(action != null)
				action(p);
		}

		public static void SafeInvoke<T1, T2>(Action<T1, T2> action, T1 p1, T2 p2)
		{
			if(action != null)
				action(p1, p2);
		}
	}
}