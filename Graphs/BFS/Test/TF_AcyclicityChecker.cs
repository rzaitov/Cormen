using System;

using NUnit.Framework;
using Graphs;

namespace Test
{
	[TestFixture]
	public class TF_AcyclicityChecker
	{
		[Test]
		public void UncyclicForward()
		{
			DirectedGraph<char> graph = new DirectedGraph<char>
			{
				{ 'a', new char[] { 'b' } },
				{ 'b', new char[] { 'c' } },
				{ 'c', new char[] { 'd' } },
				{ 'c', new char[] { } },
			};

			Check(graph, true);
		}

		[Test]
		public void Tree()
		{
			DirectedGraph<char> graph = new DirectedGraph<char>
			{
				{ 'a', new char[] { 'b', 'c' } },
				{ 'b', new char[] { 'd', 'f' } },
				{ 'c', new char[] { 'e', 'g' } },
				{ 'd', new char[] { } },
				{ 'f', new char[] { } },
				{ 'e', new char[] { } },
				{ 'g', new char[] { } },
			};

			Check(graph, true);
		}

		[Test]
		public void ThreeNodeCycle()
		{
			DirectedGraph<char> graph = new DirectedGraph<char>
			{
				{ 'a', new char[] { 'b' } },
				{ 'b', new char[] { 'c' } },
				{ 'c', new char[] { 'a' } },
			};

			Check(graph, false);
		}

		private void Check<T>(DirectedGraph<T> graph, bool isAcyclic)
		{
			IDepthFirstSearchAlgorithm<T> dfs = new RecursiveDepthFirstSearchAlgorithm<T>(graph);
			AcyclicityCheckerAlgorithm<T> checker = new AcyclicityCheckerAlgorithm<T>(dfs);

			checker.Run();

			Assert.AreEqual(isAcyclic, checker.IsAcyclic);
		}
	}
}

