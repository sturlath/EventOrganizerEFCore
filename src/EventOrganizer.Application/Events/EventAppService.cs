using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Users;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using EventOrganizer.Permissions;

namespace EventOrganizer.Events
{
    //[Authorize(EventOrganizerPermissions.Events.Default)]
    public class EventAppService : EventOrganizerAppService, IEventAppService
    {
        private readonly IRepository<Event, Guid> eventRepository;
        private readonly IRepository<AppUser, Guid> userRepository;
        private readonly EventManager eventManager;

        public EventAppService(IRepository<Event, Guid> eventRepository, IRepository<AppUser, Guid> userRepository, EventManager eventManager)
        {
            this.eventRepository = eventRepository;
            this.userRepository = userRepository;
            this.eventManager = eventManager;
        }

        [Authorize]
        public async Task<Guid> CreateAsync(EventCreationDto input)
        {
            var eventEntity = ObjectMapper.Map<EventCreationDto, Event>(input);
            await eventRepository.InsertAsync(eventEntity);
            return eventEntity.Id;
        }

        public async Task<List<EventDto>> GetUpcomingAsync()
        {
            var events = await AsyncExecuter.ToListAsync(
                eventRepository
                    .Where(x => x.StartTime > Clock.Now)
                    .OrderBy(x => x.StartTime)
            );

            return ObjectMapper.Map<List<Event>, List<EventDto>>(events);
        }

        public async Task<EventDetailDto> GetAsync(Guid id)
        {
            var @event = await eventRepository.GetAsync(id);
            //@event has 0 Attendees even thought they are saved in the DB
            var attendeeIds = @event.Attendees.Select(a => a.UserId).ToList();
            var attendees = (await AsyncExecuter.ToListAsync(userRepository.Where(u => attendeeIds.Contains(u.Id))))
                .ToDictionary(x => x.Id);

            var result = ObjectMapper.Map<Event, EventDetailDto>(@event);

            foreach (var attendeeDto in result.Attendees)
            {
                attendeeDto.UserName = attendees[attendeeDto.UserId].UserName;
            }

            return result;
        }

        [Authorize]
        public async Task RegisterAsync(Guid id)
        {
            var @event = await eventRepository.GetAsync(id);
            if (@event.Attendees.Any(a => a.UserId == CurrentUser.Id))
            {
                return;
            }

            // Hérna er attendee vistaður!
            @event.Attendees.Add(new Attendee {UserId = CurrentUser.GetId(), CreationTime = Clock.Now});
            await eventRepository.UpdateAsync(@event);
        }

        [Authorize]
        public async Task UnregisterAsync(Guid id)
        {
            var @event = await eventRepository.GetAsync(id);
            var removedItems = @event.Attendees.RemoveAll(x => x.UserId == CurrentUser.Id);
            if (removedItems.Any())
            {
                await eventRepository.UpdateAsync(@event);
            }
        }

        [Authorize]
        public async Task DeleteAsync(Guid id)
        {
            var @event = await eventRepository.GetAsync(id);

            if (CurrentUser.Id != @event.CreatorId)
            {
                throw new UserFriendlyException("You don't have the necessary permission to delete this event!");
            }

            await eventRepository.DeleteAsync(id);
        }
    }
}
