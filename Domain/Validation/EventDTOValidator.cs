using System;
using FluentValidation;
using EventPlanner.Domain.Models;
using Event_planner.Domain.Validation;

namespace EventPlanner.Domain.Validation
{
    public partial class EventDTOValidator
        : AbstractValidator<EventDTO>
    {



        public EventDTOValidator()
        {
            RuleFor(p => p.RecurringId).NotEmpty();
            RuleFor(p => p.RecurringId).MaximumLength(1);
            RuleFor(p => p.EventName).NotEmpty();
            RuleFor(p => p.EventName).MaximumLength(31);
            RuleFor(p => p.EventDesc).NotEmpty();
            RuleFor(p => p.EventDesc).MaximumLength(500);
            RuleFor(p => p.StartDate).NotEmpty().Must(d => ValidationUtilities.isValidDate(d)).WithMessage("Invalid StartDate specified");
            RuleFor(p => p.EndDate).NotEmpty().Must(d => ValidationUtilities.isValidDate(d)).WithMessage("Invalid EndDate specified").GreaterThanOrEqualTo(p => p.StartDate)
                .WithMessage("EndDate must be greater than or equal to StartDate");
            RuleFor(p => p.StartTime).NotEmpty().Must(d => ValidationUtilities.isValidTime(d)).WithMessage("Invalid StartTime specified");
            RuleFor(p => p.EndTime).NotEmpty().Must(d => ValidationUtilities.isValidTime(d)).WithMessage("Invalid EndTime specified");

        }



    }
}
