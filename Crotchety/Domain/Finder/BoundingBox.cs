using System;
namespace Crotchety.Domain.Finder
{
	public class BoundingBox
	{
		public Coord? topLeft { get; set; } = null;
		public Coord? bottomRight { get; set; } = null;
    }
}

