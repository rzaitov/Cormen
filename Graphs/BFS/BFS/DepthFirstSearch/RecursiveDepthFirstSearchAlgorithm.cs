using System;
using System.Collections.Generic;

namespace Graphs
{
	public class RecursiveDepthFirstSearchAlgorithm<TVertex> : IDepthFirstSearchAlgorithm<TVertex>
	{
		public event Action<TVertex> VertexDiscover;
		public event Action<TVertex> VertexFinish;

		private readonly DirectedGraph<TVertex> _graph;
		private readonly Dictionary<TVertex, GraphColors> _colors;

		public RecursiveDepthFirstSearchAlgorithm(DirectedGraph<TVertex> graph)
		{
			_graph = graph;
			_colors = new Dictionary<TVertex, GraphColors>(_graph.Count);
		}

		public void Run()
		{
			foreach(TVertex w in _graph.Keys)
				_colors[w] = GraphColors.White;

			VisitRange(_graph.Keys);
		}

		private void VisitRange(IEnumerable<TVertex> vertexRange)
		{
			foreach(var w in vertexRange)
				if(_colors[w] == GraphColors.White)
					Visit(w);
		}

		private void Visit(TVertex vertex)
		{
			_colors[vertex] = GraphColors.Gray;
			EventHelpers.SafeInvoke(VertexDiscover, vertex);

			var adj = _graph[vertex];
			VisitRange(adj);

			_colors[vertex] = GraphColors.Gray;
			EventHelpers.SafeInvoke(VertexFinish, vertex);
		}
	}
}