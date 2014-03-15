using System;

using NUnit.Framework;

using Graphs;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
	[TestFixture]
	public class TF_StronglyConnectedComponents
	{
		[Test]
		public void SingleScc()
		{
			DirectedGraph<char> graph = new DirectedGraph<char>
			{
				{ 'a', new char[] { 'b', 'c' } },
				{ 'b', new char[0] },
				{ 'c', new char[0] }
			};

			StronglyConenctedComponentsAlgorithm<char> alg = new StronglyConenctedComponentsAlgorithm<char>(graph);
			alg.Run();

			DirectedGraph<char> scc = alg.StronglyConnectedComponents;

			Assert.AreEqual(graph.Keys.Count, scc.Keys.Count);

			Assert.AreEqual(2, graph['a'].Count);
			Assert.AreEqual(0, graph['b'].Count);
			Assert.AreEqual(0, graph['c'].Count);

			Assert.AreEqual(true, graph['a'].Contains('b'));
			Assert.AreEqual(true, graph['a'].Contains('c'));
		}

		[Test]
		public void ThreeNodeCycle()
		{
			DirectedGraph<char> graph = new DirectedGraph<char>
			{
				{ 'a', new char[] { 'b' } },
				{ 'b', new char[] { 'c' } },
				{ 'c', new char[] { 'a' } }
			};

			IEnumerable<char> vertexes = new char[]{ 'b', 'c', 'a' };

			StronglyConenctedComponentsAlgorithm<char> alg = new StronglyConenctedComponentsAlgorithm<char>(graph, vertexes);
			alg.Run();

			DirectedGraph<char> scc = alg.StronglyConnectedComponents;
			Assert.AreEqual(graph.Count, scc.Count);

			Assert.AreEqual(1, scc['b'].Count);
			Assert.AreEqual('c', scc['b'].First());

			Assert.AreEqual(1, scc['c'].Count);
			Assert.AreEqual('a', scc['c'].First());

			Assert.AreEqual(0, scc['a'].Count);
		}

		[Test]
		public void FourComponents()
		{
			DirectedGraph<char> graph = new DirectedGraph<char>
			{
				{ 'a', new char[] { 'b' } },
				{ 'b', new char[] { 'e', 'f' } },
				{ 'e', new char[] { 'a', 'f' } },
				{ 'c', new char[] { 'd', 'g' } },
				{ 'd', new char[] { 'c', 'h' } },
				{ 'f', new char[] { 'g' } },
				{ 'g', new char[] { 'f', 'h' } },
				{ 'h', new char[] { 'h' } },
			};

			IEnumerable<char> vertexes = new char[]{ 'b', 'e', 'a', 'c', 'd', 'g', 'f', 'h' };

			StronglyConenctedComponentsAlgorithm<char> alg = new StronglyConenctedComponentsAlgorithm<char>(graph, vertexes);
			alg.Run();

			var scc = alg.StronglyConnectedComponents;

			Assert.AreEqual(graph.Keys.Count, scc.Keys.Count);

			// scc 1
			Assert.AreEqual(1, scc['b'].Count);
			Assert.AreEqual('e', scc['b'][0]);

			Assert.AreEqual(1, scc['e'].Count);
			Assert.AreEqual('a', scc['e'][0]);

			Assert.AreEqual(0, scc['a'].Count);

			// scc 2
			Assert.AreEqual(1, scc['g'].Count);
			Assert.AreEqual('f', scc['g'][0]);

			Assert.AreEqual(0, scc['f'].Count);

			// scc 3
			Assert.AreEqual(1, scc['c'].Count);
			Assert.AreEqual('d', scc['c'][0]);

			Assert.AreEqual(0, scc['d'].Count);

			// scc 3
			Assert.AreEqual(0, scc['h'].Count);
		}
	}
}

