using System;
using System.Collections.Generic;

using G = System.Collections.Generic.IDictionary<int, System.Collections.Generic.IList<int>>;
using System.Collections.ObjectModel;


namespace Graphs
{
	public class DfsAlg
	{
		private readonly G _graph;
		private readonly Action<int, int> _discoveryHook, _finishHook;

		private int[] _tDiscovery, _tFinish;

		public ReadOnlyCollection<int> DiscoveryTime
		{
			get { return Array.AsReadOnly(_tDiscovery); }
		}

		public ReadOnlyCollection<int> FinishTime
		{
			get { return Array.AsReadOnly(_tFinish); }
		}

		private int VertexCount
		{
			get { return _graph.Count; }
		}

		public DfsAlg(G graph, Action<int, int> discoveryHook, Action<int, int> finishHook)
		{
			_graph = graph;
			_discoveryHook = discoveryHook;
			_finishHook = finishHook;
		}

		public void Run()
		{
			GraphColors[] colors = new GraphColors[VertexCount];
			for(int i = 0; i < colors.Length; i++)
				colors[i] = GraphColors.White;

			_tDiscovery = new int[VertexCount];
			_tFinish = new int[VertexCount];

			int time = 0;
			foreach(var u in _graph.Keys)
			{
				if(colors[u] == GraphColors.White)
					Visit(u, _graph, colors, ref time);
			}
		}

		private void Visit(int u, G graph, GraphColors[] colors, ref int time)
		{
			colors[u] = GraphColors.Gray;
			_tDiscovery[u] = time;
			time += 1;

			if(_discoveryHook != null)
				_discoveryHook(u, _tDiscovery[u]);

			foreach(var v in graph[u])
			{
				if(colors[v] == GraphColors.White)
					Visit(v, graph, colors, ref time);
			}

			_tFinish[u] = time;
			time += 1;

			if(_finishHook != null)
				_finishHook(u, _tFinish[u]);
		}
	}
}

