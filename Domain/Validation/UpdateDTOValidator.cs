using System;
using FluentValidation;
using EventPlanner.Domain.Models;

namespace Event_planner.Domain.Validation
{
    public partial class UpdateDTOValidator
       : AbstractValidator<UpdateDTO>
    {

        public UpdateDTOValidator()
        {
            RuleFor(p => p.EventId).NotEmpty();
            RuleFor(p => p.UserId).NotEmpty();
            RuleFor(p => p.StartDate).Must(d => isValidDate(d)).WithMessage("Invalid StartDate specified");
            RuleFor(p => p.EndDate).Must(d => isValidDate(d)).WithMessage("Invalid EndDate specified").GreaterThanOrEqualTo(p => p.StartDate)
                .WithMessage("EndDate must be greater than or equal to StartDate");
            RuleFor(p => p.StartTime).Must(d => isValidTime(d)).WithMessage("Invalid StartTime specified");
            RuleFor(p => p.EndTime).Must(d => isValidTime(d)).WithMessage("Invalid EndTime specified");

        }

        protected bool isValidTime(string _time)
        {
            TimeSpan time;
            return TimeSpan.TryParseExact(_time, "hh\\:mm\\:ss", System.Globalization.CultureInfo.InvariantCulture, out time);
        }

        protected bool isValidDate(string _date)
        {
            DateTime date;
            return DateTime.TryParseExact(_date, "yyyy-mm-dd", System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None, out date);
        }

    }
}