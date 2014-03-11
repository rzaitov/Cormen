using System;

using NUnit.Framework;
using Graphs;
using System.Collections.Generic;

namespace Test.DepthFirstSearchAlgorithm
{
	[TestFixture]
	public class Test_RecursiveDepthFirstSearchAlgorithm
	{
		private DirectedGraph<char> _graph;

		[SetUp]
		public void SetUp()
		{
			_graph = new DirectedGraph<char>
			{
				{ 'u', new char[] { 'v', 'x' } },
				{ 'v', new char[] { 'y' } },
				{ 'y', new char[] { 'x' } },
				{ 'x', new char[] { 'v' } },
				{ 'w', new char[] { 'y', 'z' } },
				{ 'z', new char[] { 'z' } }
			};
		}

		[Test]
		public void TestTraverseOrder()
		{
			int time = 1;
			Dictionary<char, DiscoveryFinishTime> times = new Dictionary<char, DiscoveryFinishTime>(_graph.Count);

			RecursiveDepthFirstSearchAlgorithm<char> alg = new RecursiveDepthFirstSearchAlgorithm<char>(_graph);
			alg.VertexDiscover += v =>
			{
				times[v] = new DiscoveryFinishTime{ DiscoveryTime = time++ };
			};
			alg.VertexFinish += v =>
			{
				times[v].FinishTime = time++;
			};

			alg.Run();

			Assert.AreEqual(1, times['u'].DiscoveryTime);
			Assert.AreEqual(8, times['u'].FinishTime);

			Assert.AreEqual(2, times['v'].DiscoveryTime);
			Assert.AreEqual(7, times['v'].FinishTime);

			Assert.AreEqual(3, times['y'].DiscoveryTime);
			Assert.AreEqual(6, times['y'].FinishTime);

			Assert.AreEqual(4, times['x'].DiscoveryTime);
			Assert.AreEqual(5, times['x'].FinishTime);

			Assert.AreEqual(9, times['w'].DiscoveryTime);
			Assert.AreEqual(12, times['w'].FinishTime);

			Assert.AreEqual(10, times['z'].DiscoveryTime);
			Assert.AreEqual(11, times['z'].FinishTime);
		}
	}
}

