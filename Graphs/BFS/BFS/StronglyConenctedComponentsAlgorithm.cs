using System;
using System.Collections.Generic;

namespace Graphs
{
	public class StronglyConenctedComponentsAlgorithm<TVertex> : IAlgorithm
	{
		public event Action CompleteEvent;

		private readonly DirectedGraph<TVertex> _graph;
		private readonly IEnumerable<TVertex> _vertexes;
		private readonly Func<DirectedGraph<TVertex>, IEnumerable<TVertex>, IDepthFirstSearchAlgorithm<TVertex>> _creareDfs;

		public AlgorithmState State { get; private set; }

		LinkedList<TVertex> _descentFinishTimeVertexes;

		public DirectedGraph<TVertex> StronglyConnectedComponents { get; private set; }

		IDepthFirstSearchAlgorithm<TVertex> dfs1;
		IDepthFirstSearchAlgorithm<TVertex> dfs2;

		public StronglyConenctedComponentsAlgorithm(DirectedGraph<TVertex> graph)
			: this(graph, graph.Keys, GetDefaultDfsAlgorithm)
		{
		}

		public StronglyConenctedComponentsAlgorithm(DirectedGraph<TVertex> graph, IEnumerable<TVertex> vertexes)
			: this(graph, vertexes, GetDefaultDfsAlgorithm)
		{
		}

		public StronglyConenctedComponentsAlgorithm(DirectedGraph<TVertex> graph, IEnumerable<TVertex> vertexes, Func<DirectedGraph<TVertex>, IEnumerable<TVertex>, IDepthFirstSearchAlgorithm<TVertex>> createDfs)
		{
			_graph = graph;
			_vertexes = vertexes;
			_creareDfs = createDfs;

			_descentFinishTimeVertexes = new LinkedList<TVertex>();

			StronglyConnectedComponents = new DirectedGraph<TVertex>();

			State = AlgorithmState.Initialized;
		}

		public void Run()
		{
			foreach(var k in _graph.Keys)
				StronglyConnectedComponents[k] = new List<TVertex>();

			DirectedGraph<TVertex> _transposedGraph = _graph.Transpose();

			dfs1 = _creareDfs(_transposedGraph, _vertexes);
			dfs1.VertexFinish += CreateDescentOrderedVertexes;
			dfs1.Run();
			dfs1.VertexFinish -= CreateDescentOrderedVertexes;


			dfs2 = _creareDfs(_graph, _descentFinishTimeVertexes);
			dfs2.ExploreEdge += OnExploreEdge;
			dfs2.Run();
			dfs2.ExploreEdge -= OnExploreEdge;

			State = AlgorithmState.Completed;
			EventHelpers.SafeInvoke(CompleteEvent);
		}

		private void OnExploreEdge(Edge<TVertex> edge)
		{
			if(dfs2.Colors[edge.Destination] != GraphColors.White)
				return;

			StronglyConnectedComponents[edge.Source].Add(edge.Destination);
		}

		private void CreateDescentOrderedVertexes (TVertex vertex)
		{
			_descentFinishTimeVertexes.AddFirst(vertex);
		}

		public void Terminate()
		{
			throw new NotImplementedException();
		}

		private static IDepthFirstSearchAlgorithm<TVertex> GetDefaultDfsAlgorithm(DirectedGraph<TVertex> graph, IEnumerable<TVertex> vertexes)
		{
			IDepthFirstSearchAlgorithm<TVertex> dfs = new RecursiveDepthFirstSearchAlgorithm<TVertex>(graph, vertexes);
			return dfs;
		}
	}

	internal static class DirectedGraphExtensions
	{
		public static IList<TVertex> GetOrCreate<TVertex>(this DirectedGraph<TVertex> graph, TVertex key)
		{
			IList<TVertex> result = null;
			if(!graph.TryGetValue(key, out result))
				graph.Add(key, result = new List<TVertex>());

			return result;
		}
	}
}

