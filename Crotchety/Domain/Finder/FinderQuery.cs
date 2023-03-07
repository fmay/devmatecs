using System;
using System.Runtime.Intrinsics.X86;

namespace Crotchety.Domain.Finder
{
	public class FinderQuery
	{
        public double? meLat { get; set; }
        public double? meLng { get; set; }
        public BoundingBox? boundingBox { get; set; }
        public int? skip { get; set; }
        public int? limit { get; set; }
        public FinderQueryType? type { get; set; }

        public Skill[] skills { get; set; } = Array.Empty<Skill>();
        public string merlin { get; set; } = "";
	}
}

