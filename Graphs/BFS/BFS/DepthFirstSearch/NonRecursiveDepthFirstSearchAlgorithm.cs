using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Graphs
{
	public class NonRecursiveDepthFirstSearchAlgorithm<TVertex> : IDepthFirstSearchAlgorithm<TVertex>
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

		public NonRecursiveDepthFirstSearchAlgorithm(DirectedGraph<TVertex> graph)
			: this(graph, graph.Keys)
		{
		}

		public NonRecursiveDepthFirstSearchAlgorithm(DirectedGraph<TVertex> graph, IEnumerable<TVertex> vertexes)
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

			Stack<StackFrame<TVertex>> stack = new Stack<StackFrame<TVertex>>();

			foreach(var vertex in _vertexes)
			{
				if(_colors[vertex] == GraphColors.White)
				{
					// prepare function call
					StackFrame<TVertex> frame = CreateNewFrameFor(vertex);
					stack.Push(frame);

					while(stack.Count != 0) // function call
					{
						enter_label:
						// fetch local variables
						StackFrame<TVertex> sf = stack.Pop();
						switch(sf.State)
						{
							case 1: goto initial_label;
							case 2: goto continue_label;
						}

						initial_label:
						_colors[sf.Vertex] = GraphColors.Gray;
						EventHelpers.SafeInvoke(VertexDiscover, sf.Vertex);

						if(State != AlgorithmState.Runing)
							return;

						continue_label:
						while(sf.AdjVertexEnumerator.MoveNext())
						{
							EventHelpers.SafeInvoke(ExploreEdge, new Edge<TVertex>(sf.Vertex, sf.AdjVertexEnumerator.Current));

							if(State != AlgorithmState.Runing)
								return;

							if(_colors[sf.AdjVertexEnumerator.Current] != GraphColors.White)
								continue;

							// store local variables
							sf.State = 2;
							stack.Push(sf);

							// prepare function call
							var context = CreateNewFrameFor(sf.AdjVertexEnumerator.Current);
							stack.Push(context);

							// perform recursion call
							goto enter_label;
						}

						sf.AdjVertexEnumerator.Dispose();

						_colors[sf.Vertex] = GraphColors.Black;
						EventHelpers.SafeInvoke(VertexFinish, sf.Vertex);

						if(State != AlgorithmState.Runing)
							return;
					}
				}

				if(State != AlgorithmState.Runing)
					return;
			}

			State = AlgorithmState.Completed;
			EventHelpers.SafeInvoke(CompleteEvent);
		}

		private StackFrame<TVertex> CreateNewFrameFor(TVertex vertex)
		{
			StackFrame<TVertex> frame = new StackFrame<TVertex>
			{
				State = 1,
				Vertex = vertex,
				AdjVertexEnumerator = _graph[vertex].GetEnumerator()
			};

			return frame;
		}

		public void Terminate()
		{
			State = AlgorithmState.Terminated;
		}
	}

	public class StackFrame<TVertex>
	{
		public int State { get; set; }
		public TVertex Vertex { get; set; }
		public IEnumerator<TVertex> AdjVertexEnumerator { get; set; }
	}
}

