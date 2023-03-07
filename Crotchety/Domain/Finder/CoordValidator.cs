using System;
using FluentValidation;

namespace Crotchety.Domain.Finder
{
	public class CoordValidator : AbstractValidator<Coord>
    {
		public CoordValidator()
		{
            RuleFor(coord => coord.lat).NotNull().GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
            RuleFor(coord => coord.lng).NotNull().GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
        }
    }
}

