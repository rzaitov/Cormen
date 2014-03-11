using System;

namespace Graphs
{
	public interface IDepthFirstSearchAlgorithm<TVertex> : IAlgorithm
	{
		event Action<TVertex> VertexDiscover;
		event Action<TVertex> VertexFinish;
	}
}

