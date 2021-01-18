using Volo.Abp;

namespace EventOrganizer.Events
{
    public class EventAlreadyExistsException : BusinessException
    {
        public EventAlreadyExistsException(string name)
            : base(EventOrganizerDomainErrorCodes.EventAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
