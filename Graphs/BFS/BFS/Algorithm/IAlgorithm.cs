using System;

namespace Graphs
{
	public interface IAlgorithm
	{
		event Action CompleteEvent;

		AlgorithmState State { get; }
		void Run();
		void Terminate();
	}
}

