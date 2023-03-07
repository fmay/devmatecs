using System;
using FluentValidation;

namespace Crotchety.Domain.Finder
{
	public class FinderQueryValidator : AbstractValidator<FinderQuery>
    {
		public FinderQueryValidator()
		{
            RuleFor(query => query.type).NotNull();
            RuleFor(query => query.meLat).NotNull().LessThanOrEqualTo(180).GreaterThanOrEqualTo(-180);
            RuleFor(query => query.meLng).NotNull().LessThanOrEqualTo(180).GreaterThanOrEqualTo(-180);
            RuleFor(query => query.boundingBox).NotNull().WithMessage("Bounding Box missing");
            RuleFor(query => query.boundingBox).SetValidator(new BoundingBoxValidator());
            RuleFor(query => query.skip).GreaterThanOrEqualTo(0);
            RuleFor(query => query.limit).GreaterThan(0).LessThan(100);
        }
    }
}

