using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;


namespace Graphs
{
	public interface IDepthFirstSearchAlgorithm<TVertex> : IAlgorithm
	{
		event Action<TVertex> VertexDiscover;
		event Action<TVertex> VertexFinish;

		event Action<Edge<TVertex>> ExploreEdge;

		ReadOnlyDictionary<TVertex, GraphColors> Colors { get; }
	}
}

