using AutoMapper;
using Event_planner.Repositories;
using EventPlanner.Models;
using EventPlanner.Domain.Models;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EventPlanner.Domain.Validation;
using EventPlanner.Domain.Mapping;

namespace Event_planner.Services
{
    public class EventPlannerService : IEventPlannerService
    {
        private IEventPlannerRepository EventPlannerRepo;

        private readonly IMapper mapper;
        public EventPlannerService(IEventPlannerRepository EventPlannerRepo, IMapper mapper){
            this.EventPlannerRepo = EventPlannerRepo;
            this.mapper = mapper;
        }
        public void DeleteEvent(int id)
        {
            var Event = this.EventPlannerRepo
            .findEvent(id);
            if (Event == null)
            {
                throw new Exception($"Could not find Event {id}");
            }
            this.EventPlannerRepo.DeleteEvent(Event);

        }

         public DateTime getDate(string Date) 
         {
            return DateTime.Parse(Date);
         }
        
        public TimeSpan getTime(string Time) 
        {
            return TimeSpan.Parse(Time);
        }

        public void UpdateEvent(UpdateDTO updateDTO)
        {
             
             var ev = this.EventPlannerRepo.findEvent(updateDTO.EventId);
                          
                    
             if(ev == null)
             {
                 throw new Exception("Event does not exist!");
             }
             else
             {   
                 //ev.UserId = updateDTO.UserId;
                 //ev.EventId = updateDTO.EventId;
                 ev.EventName = updateDTO.EventName != null ? updateDTO.EventName : ev.EventName;
                 ev.EventDesc = updateDTO.EventDesc != null ? updateDTO.EventDesc : ev.EventDesc;
                 ev.RecurringId = updateDTO.RecurringId != null ? updateDTO.RecurringId : ev.RecurringId;
                 ev.StartDate =  updateDTO.StartDate != null ? getDate(updateDTO.StartDate) : ev.StartDate;
                 ev.EndDate =   updateDTO.EndDate != null ? getDate(updateDTO.EndDate) : ev.EndDate;
                 ev.StartTime =  updateDTO.StartTime != null ? getTime (updateDTO.StartTime) : ev.StartTime;
                 ev.EndTime =   updateDTO.EndTime != null ? getTime(updateDTO.EndTime) : ev.EndTime;
                 ev.IsFullDay =  updateDTO.IsFullDay != null ? updateDTO.IsFullDay : ev.IsFullDay;
                       
                 this.EventPlannerRepo.UpdateEvent(ev);
             }       
                     
        }          

        public void CreateEvent(EventDTO EventDTO)
        {
            var Event = mapper.Map<Event>(EventDTO);
            
            this.EventPlannerRepo.CreateEvent(Event);

        }

    }
}