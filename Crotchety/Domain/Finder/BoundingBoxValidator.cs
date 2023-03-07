using System;
using FluentValidation;

namespace Crotchety.Domain.Finder
{
	public class BoundingBoxValidator : AbstractValidator<BoundingBox>
    {
		public BoundingBoxValidator()
		{
            RuleFor(bbox => bbox.topLeft).SetValidator(new CoordValidator());
            RuleFor(bbox => bbox.bottomRight).SetValidator(new CoordValidator());
        }
    }
}

