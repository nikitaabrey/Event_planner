using Event_planner.Repositories;
using EventPlanner.Models;
using EventPlanner.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EventPlanner.Domain.Validation;
using AutoMapper;
using EventPlanner.Domain.Mapping;


namespace Event_planner.Services
{
    public class EventPlannerService : IEventPlannerService
    {
        private IEventPlannerRepository EventPlannerRepo;
        public EventPlannerService(IEventPlannerRepository EventPlannerRepo){
            this.EventPlannerRepo = EventPlannerRepo;
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

        public void UpdateEvent(EventDTO eventDTO)
        {
             
             var ev = this.EventPlannerRepo.findEvent(eventDTO.EventId);
                          
                    
             if(ev == null)
             {
                 throw new Exception("Event does not exist!");
             }
             else
             {   
                 ev.UserId = eventDTO.UserId;
                 ev.EventId = eventDTO.EventId;
                 ev.EventName = eventDTO.EventName;
                 ev.EventDesc = eventDTO.EventDesc;
                 ev.RecurringId = eventDTO.RecurringId;
                 ev.EventName =  eventDTO.EventName;
                 ev.EventDesc =  eventDTO.EventDesc;
                 ev.RecurringId =  eventDTO.RecurringId;
                 ev.StartDate =  getDate(eventDTO.StartDate);
                 ev.EndDate =   getDate(eventDTO.EndDate);
                 ev.StartTime =  getTime (eventDTO.StartTime);
                 ev.EndTime =   getTime(eventDTO.EndTime);
                 ev.IsFullDay =  eventDTO.IsFullDay;
                       
                 this.EventPlannerRepo.UpdateEvent(ev);
             }       
                     
        }          

    }
}