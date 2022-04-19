using AutoMapper;
using Event_planner.Domain.Models;
using Event_planner.Domain.UserExceptions;
using Event_planner.Domain.Validation;
using Event_planner.Repositories;
using EventPlanner.Domain.Models;
using EventPlanner.Models;

namespace Event_planner.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository repo;
        private readonly IMapper mapper;
        public CalendarService(ICalendarRepository repo,IMapper mapper) {
            this.repo = repo;
            this.mapper = mapper;
        }
        public CalendarDTO getDay(String fullDate)
        {
            if (!ValidationUtilities.isValidDate(fullDate))
            {
                throw new ArgumentException(fullDate + "is not a valid date");
            }
            DateTime id = DateTime.Parse(fullDate);
            Calendar calendar =  repo.get(id);
            
            if (calendar == null) {

                throw new ResourceNotFoundException(fullDate + "not found");
            }
            return mapper.Map<CalendarDTO>(calendar);
        }


        public IEnumerable<CalendarDTO> getMonth(int month, int year)
        {
            if (!ValidationUtilities.isValidYear(year) && !ValidationUtilities.isValidMonth(month)) { 
                throw new ArgumentException("Invalid month or year");
            }

         IEnumerable<Calendar> calendar = repo.get(filter: s => s.Month == month && s.Year == year);
            return mapper.Map<IEnumerable<CalendarDTO>>(calendar);
        }

        public IEnumerable<CalendarDTO> getWeek(string fullDate)
        {

            if (!ValidationUtilities.isValidDate(fullDate))
            {
                throw new ArgumentException(fullDate + "is not a valid date");
            }

            DateTime id = DateTime.Parse(fullDate);
            Calendar current = repo.get(id);
            
            if (current == null)
            {

                throw new ResourceNotFoundException(fullDate + "not found");
            }
            
            IEnumerable<Calendar> calendar  =  repo.get(filter: f => f.FullDate >= current.FirstOfWeek && f.FullDate <= current.LastOfWeek);
            return mapper.Map<IEnumerable<CalendarDTO>>(calendar);

        }

      
    }
}
