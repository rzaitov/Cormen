using System;

namespace Graphs
{
	public class AcyclicityCheckerAlgorithm<TVetex> : IAlgorithm, IDisposable
	{
		public event Action CompleteEvent;

		private IDepthFirstSearchAlgorithm<TVetex> _dfs;

		public bool IsAcyclic { get; private set; }

		public AlgorithmState State { get; private set; }

		public AcyclicityCheckerAlgorithm(IDepthFirstSearchAlgorithm<TVetex> dfs)
		{
			_dfs = dfs;

			_dfs.ExploreEdge += OnEdgeExplore;
			IsAcyclic = true;

			State = AlgorithmState.Initialized;
		}

		private void OnEdgeExplore(Edge<TVetex> edge)
		{
			GraphColors dstVertexColor = _dfs.Colors[edge.Destination];
			switch(dstVertexColor)
			{
				case GraphColors.White:
					break;

				case GraphColors.Gray:
					IsAcyclic = false;
					_dfs.Terminate();
					break;

				case GraphColors.Black:
					throw new InvalidOperationException();

				default:
					throw new NotImplementedException();
			}
		}

		public void Run()
		{
			_dfs.Run();

			State = AlgorithmState.Completed;
			EventHelpers.SafeInvoke(CompleteEvent);
		}

		public void Terminate()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			_dfs.ExploreEdge -= OnEdgeExplore;
		}
	}
}