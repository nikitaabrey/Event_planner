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
            RuleFor(p => p.StartDate).NotEmpty().Must(d => isValidDate(d)).WithMessage("Invalid StartDate specified");
            RuleFor(p => p.EndDate).NotEmpty().Must(d => isValidDate(d)).WithMessage("Invalid EndDate specified").GreaterThanOrEqualTo(p => p.StartDate)
                .WithMessage("EndDate must be greater than or equal to StartDate");
            RuleFor(p => p.StartTime).NotEmpty().Must(d => isValidTime(d)).WithMessage("Invalid StartTime specified");
            RuleFor(p => p.EndTime).NotEmpty().Must(d => isValidTime(d)).WithMessage("Invalid EndTime specified");

        }


        protected bool isValidTime(string _time) {
            TimeSpan time;
            return TimeSpan.TryParseExact(_time, "hh\\:mm\\:ss", System.Globalization.CultureInfo.InvariantCulture, out time);
        }
  
        protected bool isValidDate(string _date) {
            DateTime date;
            return DateTime.TryParseExact(_date, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None, out date);
        }

    }
}
