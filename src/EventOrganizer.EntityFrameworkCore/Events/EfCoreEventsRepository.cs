using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventOrganizer.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EventOrganizer.Events
{
    public class EfCoreEventsRepository
    : EfCoreRepository<EventOrganizerDbContext, Event, Guid> 
    {
        public EfCoreEventsRepository(
            IDbContextProvider<EventOrganizerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<Event> FindByTitleAsync(string title)
        {
            return await DbSet.FirstOrDefaultAsync(@event => @event.Title == title);
        }

        public async Task<List<Event>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            return await DbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    @event => @event.Title.Contains(filter)
                 )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
