using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace EventOrganizer.Events
{
    public class Event : FullAuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsFree { get; set; }

        public DateTime StartTime { get; set; }

        public ICollection<Attendee> Attendees { get; set; }

        internal Event()
        {
            //This constructor is for deserialization / ORM purpose
           Attendees = new List<Attendee>();
        }

        internal Event( Guid id,
                        [NotNull] string title,
                        [NotNull] string description,
                        [NotNull] bool isFree,
                        [NotNull] DateTime startTime) : base(id)
        {
            SetTitle(title);
            Description = description;
            IsFree = isFree;
            StartTime = startTime;
        }

        internal Event ChangeTitle([NotNull] string title)
        {
            SetTitle(title);
            return this;
        }

        private void SetTitle([NotNull] string title)
        {
            Title = Check.NotNullOrWhiteSpace(
            title,
            nameof(title),
            maxLength: EventConsts.MaxTitleLength
        );
        }
    }
}
