using System;
using System.Collections.Generic;

namespace Graphs
{
	public class DirectedGraph<TVertex> : Dictionary<TVertex, IList<TVertex>>
	{
		public DirectedGraph()
		{
		}

		public void Add(TVertex vertex, params TVertex[] adjVertexes)
		{
			this[vertex] = new List<TVertex>(adjVertexes);
		}
	}
}

