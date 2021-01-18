using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EventOrganizer.Events
{
    public class EventManager : DomainService
    {
        private readonly IRepository<Event, Guid> eventRepository;

        public EventManager(IRepository<Event, Guid> eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public async Task<Event> CreateAsync(
                        [NotNull] string title,
                        [NotNull] string description,
                        [NotNull] bool isFree,
                        [NotNull] DateTime startTime)
        {
            Check.NotNullOrWhiteSpace(title, nameof(title));

            //var existingEvent = await eventRepository.FindByTitleAsync(title);
            //if (existingEvent != null)
            //{
            //    throw new EventAlreadyExistsException(title);
            //}

            return new Event(
                GuidGenerator.Create(),
                title,
                description,
                isFree,
                startTime
            );
        }

        public async Task ChangeTitleAsync(
            [NotNull] Event @event,
            [NotNull] string newTitle)
        {
            Check.NotNull(@event, nameof(@event));
            Check.NotNullOrWhiteSpace(newTitle, nameof(newTitle));

            //var existingAuthor = await eventRepository.FindByTitleAsync(newTitle);
            //if (existingAuthor != null && existingAuthor.Id != @event.Id)
            //{
            //    throw new EventAlreadyExistsException(newTitle);
            //}

            @event.ChangeTitle(newTitle);
        }
    }
}
