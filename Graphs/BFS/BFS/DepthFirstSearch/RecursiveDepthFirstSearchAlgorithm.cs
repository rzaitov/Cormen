using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Graphs
{
	public class RecursiveDepthFirstSearchAlgorithm<TVertex> : IDepthFirstSearchAlgorithm<TVertex>
	{
		public event Action CompleteEvent;

		public event Action<TVertex> VertexDiscover;
		public event Action<TVertex> VertexFinish;
		public event Action<Edge<TVertex>> ExploreEdge;

		private readonly DirectedGraph<TVertex> _graph;
		private readonly IEnumerable<TVertex> _vertexes;

		private readonly Dictionary<TVertex, GraphColors> _colors;

		private readonly ReadOnlyDictionary<TVertex, GraphColors> _readOnlyColors;
		public ReadOnlyDictionary<TVertex, GraphColors> Colors
		{
			get { return _readOnlyColors; }
		}

		public AlgorithmState State { get; private set; }

		public RecursiveDepthFirstSearchAlgorithm(DirectedGraph<TVertex> graph)
			: this(graph, graph.Keys)
		{
		}

		public RecursiveDepthFirstSearchAlgorithm(DirectedGraph<TVertex> graph, IEnumerable<TVertex> vertexes)
		{
			_graph = graph;
			_vertexes = vertexes;

			_colors = new Dictionary<TVertex, GraphColors>(_graph.Count);
			_readOnlyColors = new ReadOnlyDictionary<TVertex, GraphColors>(_colors);

			State = AlgorithmState.Initialized;
		}

		public void Run()
		{
			State = AlgorithmState.Runing;

			foreach(TVertex w in _vertexes)
				_colors[w] = GraphColors.White;

			foreach(var w in _vertexes)
 				if(_colors[w] == GraphColors.White)
				{
					Visit(w);

					if(State != AlgorithmState.Runing)
						return;
				}

			State = AlgorithmState.Completed;
			EventHelpers.SafeInvoke(CompleteEvent);
		}

		private void Visit(TVertex vertex)
		{
			_colors[vertex] = GraphColors.Gray;
			EventHelpers.SafeInvoke(VertexDiscover, vertex);

			IList<TVertex> adj = _graph[vertex];
			foreach(var w in adj)
			{
				EventHelpers.SafeInvoke(ExploreEdge, new Edge<TVertex>(vertex, w));

				if(State != AlgorithmState.Runing)
					return;

				if(_colors[w] == GraphColors.White)
					Visit(w);
			}

			_colors[vertex] = GraphColors.Black;
			EventHelpers.SafeInvoke(VertexFinish, vertex);
		}

		public void Terminate()
		{
			State = AlgorithmState.Terminated;
		}
	}
}