using System;
using System.Collections.Generic;

namespace Graphs
{
	public static class AlgorithmExtensions
	{
		public static DirectedGraph<TVertex> Transpose<TVertex>(this DirectedGraph<TVertex> graph)
		{
			DirectedGraph<TVertex> tg = new DirectedGraph<TVertex>();

			foreach(var v in graph.Keys)//key-value-pair
				tg[v] = new List<TVertex>();

			foreach(var kvp in graph)//key-value-pair
				foreach(var v in kvp.Value)
					tg[v].Add(kvp.Key);

			return tg;
		}
	}
}