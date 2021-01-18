using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace EventOrganizer.Events
{
    public class Attendee : AuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }

    }
}
