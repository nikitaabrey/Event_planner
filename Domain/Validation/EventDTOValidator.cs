using System;
using FluentValidation;
using EventPlanner.Domain.Models;

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
        }

    }
}
