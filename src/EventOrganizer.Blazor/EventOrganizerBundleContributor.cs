using Volo.Abp.Bundling;

namespace EventOrganizer.Blazor
{
    public class EventOrganizerBundleContributor : IBundleContributor
    {
        public void AddScripts(BundleContext context)
        {
        }

        public void AddStyles(BundleContext context)
        {
            context.Add("main.css", true);
        }
    }
}