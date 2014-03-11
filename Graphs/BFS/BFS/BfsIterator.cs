using System;
using System.Collections.Generic;

namespace Graphs
{
	public class BfsIterator
	{
		public BfsIterator()
		{
		}

		public static void DoBFS(LinkedList<int>[] graph, int source, out int[] distance)
		{
			GraphColors[] colors = new GraphColors[graph.Length];
			for(int i = 0; i < colors.Length; i++)
				colors[i] = GraphColors.White;

			distance = new int[graph.Length];

			colors[source] = GraphColors.Gray;
			distance[source] = 0;

			Queue<int> q = new Queue<int>();
			q.Enqueue(source);

			while(q.Count != 0)
			{
				int u = q.Dequeue();

				LinkedList<int> adj = graph[u];
				foreach(int v in adj)
				{
					if(colors[v] == GraphColors.White)
					{
						colors[v] = GraphColors.Gray;
						distance[v] = distance[u] + 1;
						q.Enqueue(v);
					}
				}

				colors[u] = GraphColors.Black;
			}
		}
	}
}