using Microsoft.Extensions.Localization;
using EventOrganizer.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EventOrganizer.Blazor
{
    [Dependency(ReplaceServices = true)]
    public class EventOrganizerBrandingProvider : DefaultBrandingProvider
    {
        public EventOrganizerBrandingProvider(IStringLocalizer<EventOrganizerResource> localizer)
        {
            _localizer = localizer;
        }

        private readonly IStringLocalizer<EventOrganizerResource> _localizer;

        public override string AppName => _localizer["AppName"];
    }
}
