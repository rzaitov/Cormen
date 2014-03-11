using System;
using System.Collections.Generic;

using LL = System.Collections.Generic.LinkedList<int>;
using G = System.Collections.Generic.IDictionary<int, System.Collections.Generic.IList<int>>;

namespace Graphs
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			/*
			LinkedList<int>[] graph = new LinkedList<int>[]
			{
				new LL().Add(1),
				new LL().Add(0, 2),
				new LL().Add(1, 3),
				new LL().Add(2, 4, 5),
				new LL().Add(3, 5, 6),
				new LL().Add(3, 4, 6, 7),
				new LL().Add(4, 5, 7),
				new LL().Add(5, 6)
			};

			int[] distance;
			BfsIterator.DoBFS(graph, 2, out distance);

			foreach(var d in distance)
				Console.WriteLine(d);
			*/

			/*
			G g = new Dictionary<int, IList<int>>()
			{
				{ 0, new int[] { 1, 3 } },
				{ 1, new int[] { 2 } },
				{ 2, new int[] { 3 } },
				{ 3, new int[] { 1 } },
				{ 4, new int[] { 2, 5 } },
				{ 5, new int[] { 5 } }
			};
			*/


			/*
			G g = new Dictionary<int, IList<int>>
			{
				{ 0, new int[]{1,4}},
				{1, new int[]{2, 4}},
				{2, new int[]{7}},
				{3, new int[]{4}},
				{4, new int[]{}},
				{5, new int[]{2, 6}},
				{6, new int[]{7}},
				{7, new int[]{}},
				{8, new int[]{}},
			};
			*/

			/*
			int[] tD, tF;
			LinkedList<int> sort = new LinkedList<int>();
			Action<int, int> fHook = (vertex, time) =>
			{
				sort.AddFirst(vertex);
			};
			DfsAlg dfs = new DfsAlg(g, null, fHook);
			dfs.Run();

			for(int i = 0; i < g.Count; i++)
				Console.WriteLine(string.Format("d:{0} f:{1}", dfs.DiscoveryTime[i], dfs.FinishTime[i]));

			foreach(int v in sort)
				Console.Write(string.Format("{0} ", v));
			*/

			DirectedGraph<int> g = new DirectedGraph<int>
			{
				{ 0, new int[] { 1, 3 } },
				{ 1, new int[] { 2 } },
				{ 2, new int[] { 3 } },
				{ 3, new int[] { 1 } },
				{ 4, new int[] { 2, 5 } },
				{ 5, new int[] { 5 } }
			};
			Print(g);
			Console.WriteLine();

			DirectedGraph<int> tg = g.Transpose();
			Print(tg);
		}

		private static void Print<TVertex>(DirectedGraph<TVertex> graph)
		{
			foreach(var kvp in graph)
			{
				var adjstr = string.Join(", ", kvp.Value);
				Console.WriteLine(string.Format("{0} -> {1}", kvp.Key, adjstr));
			}
		}
	}
}
