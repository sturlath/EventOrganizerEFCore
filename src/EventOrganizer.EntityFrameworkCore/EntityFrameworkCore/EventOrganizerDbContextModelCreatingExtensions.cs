using Microsoft.EntityFrameworkCore;
using EventOrganizer.Events;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EventOrganizer.EntityFrameworkCore
{
    public static class EventOrganizerDbContextModelCreatingExtensions
    {
        public static void ConfigureEventOrganizer(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Event>(b =>
            {
                b.ToTable(EventOrganizerConsts.DbTablePrefix + "Events",
                    EventOrganizerConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(EventConsts.MaxTitleLength);

                b.HasIndex(x => x.Title);

                // ADD THE MAPPING FOR THE RELATION
                //b.HasMany<EventAttendee>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
            });

            builder.Entity<Attendee>(b =>
            {
                b.ToTable(EventOrganizerConsts.DbTablePrefix + "Attendees",
                    EventOrganizerConsts.DbSchema);

                b.ConfigureByConvention();

                // ADD THE MAPPING FOR THE RELATION
                //b.HasOne<Event>()
                //.WithMany(a => a.Attendees)
                //.HasForeignKey(p => p.EventForeignKey).IsRequired();
            });

        }
    }
}