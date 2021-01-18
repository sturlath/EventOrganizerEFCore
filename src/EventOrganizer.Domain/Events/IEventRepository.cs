using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EventOrganizer.Events
{
    public interface IEventRepository : IRepository<Event, Guid>
    {
        Task<Event> FindByTitleAsync(string title);

        Task<List<Event>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}
