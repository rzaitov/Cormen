﻿using System;
using System.Collections.Generic;

namespace Graphs
{
	public class NonRecursiveDepthFirstSearchAlgorithm<TVertex> : IDepthFirstSearchAlgorithm<TVertex>
	{
		public event Action<TVertex> VertexDiscover;
		public event Action<TVertex> VertexFinish;

		private readonly DirectedGraph<TVertex> _graph;
		private readonly Dictionary<TVertex, GraphColors> _colors;

		public NonRecursiveDepthFirstSearchAlgorithm(DirectedGraph<TVertex> graph)
		{
			_graph = graph;
			_colors = new Dictionary<TVertex, GraphColors>(_graph.Count);
		}

		public void Run()
		{
			foreach(TVertex w in _graph.Keys)
				_colors[w] = GraphColors.White;

			Stack<StackFrame<TVertex>> stack = new Stack<StackFrame<TVertex>>();

			foreach(var vertex in _graph.Keys)
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

						continue_label:
						while(sf.AdjVertexEnumerator.MoveNext())
						{
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
					}
				}
			}
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
	}

	public class StackFrame<TVertex>
	{
		public int State { get; set; }
		public TVertex Vertex { get; set; }
		public IEnumerator<TVertex> AdjVertexEnumerator { get; set; }
	}
}

