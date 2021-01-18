using System;
using System.Threading.Tasks;
using EventOrganizer.Events;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace EventOrganizer
{
    public class EventOrganizerDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Event, Guid> eventRepository;
        private readonly EventManager eventManager;

        public EventOrganizerDataSeederContributor(
            IRepository<Event, Guid> eventRepository,
            EventManager eventManager)
        {
            this.eventRepository = eventRepository;
            this.eventManager = eventManager;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await eventRepository.GetCountAsync() > 0)
            {
                return;
            }

            var event1 = await eventRepository.InsertAsync(
                await eventManager.CreateAsync(
                    "Event 1",
                    "Awesome event1",
                    false,
                    new DateTime(2021, 01, 20)
                )
            );

            var event2 = await eventRepository.InsertAsync(
                await eventManager.CreateAsync(
                    "Event 2",
                    "Awesome event2",
                    true,
                    new DateTime(2021, 01, 25)
                )
            );


        }
    }
}
