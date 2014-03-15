using System;

namespace Graphs
{
	public struct Edge<TVertex>
	{
		public TVertex Source;
		public TVertex Destination;

		public Edge(TVertex src, TVertex dst)
		{
			Source = src;
			Destination = dst;
		}
	}
}

