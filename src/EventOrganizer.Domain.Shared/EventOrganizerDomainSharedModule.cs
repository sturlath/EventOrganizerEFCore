using EventOrganizer.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.IdentityServer;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.Localization;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EventOrganizer
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainSharedModule),
        typeof(AbpBackgroundJobsDomainSharedModule),
        typeof(AbpFeatureManagementDomainSharedModule),
        typeof(AbpIdentityDomainSharedModule),
        typeof(AbpIdentityServerDomainSharedModule),
        typeof(AbpPermissionManagementDomainSharedModule),
        typeof(AbpSettingManagementDomainSharedModule),
        typeof(AbpTenantManagementDomainSharedModule)
        )]
    public class EventOrganizerDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EventOrganizerGlobalFeatureConfigurator.Configure();
            EventOrganizerModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EventOrganizerDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<EventOrganizerResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/EventOrganizer");

                options.Resources
                .Get<AbpIdentityServerResource>() //AbpIdentityServerResource
                .AddVirtualJson("/Localization/EventOrganizer");

                options.Resources
                    .Get<IdentityResource>() //AbpIdentity
                    .AddVirtualJson("/Localization/EventOrganizer");

                options.Resources
                    .Get<AbpTenantManagementResource>() //AbpTenantManagement
                    .AddVirtualJson("/Localization/EventOrganizer");

                options.Resources
                    .Get<Volo.Abp.Localization.Resources.AbpLocalization.AbpLocalizationResource>() //
                    .AddVirtualJson("/Localization/AbpUi");

                options.DefaultResourceType = typeof(EventOrganizerResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EventOrganizer", typeof(EventOrganizerResource));
            });
        }
    }
}
