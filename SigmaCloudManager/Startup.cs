using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SCM.Models.RequestModels;
using SCM.Models.ViewModels;
using SCM.Data;
using SCM.Services;
using SCM.Factories;
using SCM.Validators;
using Newtonsoft.Json;
using SCM.Hubs;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Mind.Api.Filters;
using System.IO;
using Mind.Api.Models;
using Mind.Services;
using Mind.Builders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Autofac;
using Mind.Validators;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Mind;

namespace Mind
{
    public class Startup
    {
        private MapperConfiguration MapperConfiguration { get; set; }
        private IConfiguration Configuration { get; }
        private readonly IHostingEnvironment _hostingEnv;

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            _hostingEnv = env;

            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperServiceModelProfileConfiguration());
                cfg.AddProfile(new AutoMapperViewModelProfileConfiguration());
                cfg.AddProfile(new AutoMapperApiModelProfileConfiguration());
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DB context for our Sigma repository
            services.AddDbContext<SigmaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Get application configuration options
            services.Configure<SCM.ApplicationConfiguration>(Configuration.GetSection("ApplicationConfiguration"));

            // SignalR - library for async updates to clients. This is used by the API controllers to 
            // update clients on progress during network sync/checksync operations
            services.AddSignalR();

            // Add framework services.
            services.AddMvc();

            services.AddMvcCore()
                    .AddVersionedApiExplorer(o =>
                    {
                        o.GroupNameFormat = "'v'VVV";
                        o.SubstituteApiVersionInUrl = true;
                    })
                    .AddJsonOptions(o =>
                    {
                        o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                        o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        o.SerializerSettings.Converters.Add(new StringEnumConverter
                        {
                            CamelCaseText = true
                        });
                    });

            services.AddApiVersioning(o =>
                {
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                    o.AssumeDefaultVersionWhenUnspecified = true;
                }
            );

            services.AddSwaggerGen
            (
              options =>
              {
                  var provider = services.BuildServiceProvider()
                                .GetRequiredService<IApiVersionDescriptionProvider>();

                  options.TagActionsBy(api => api.GroupName);
                  foreach ( var description in provider.ApiVersionDescriptions)
                  {
                      options.SwaggerDoc(
                      description.GroupName,
                      new Info()
                      {
                          Title = $"MIND API {description.ApiVersion}",
                          Version = description.ApiVersion.ToString(),
                          Contact = new Contact
                          {
                              Name = "Jon Beasley",
                              Email = "jonathan.beasley@thomsonreuters.com",
                              Url = "https://thehub.thomsonreuters.com/people/9000359"
                          },
                          Description = "The MIND API library enables API consumers to create and manage resources in the MIND system. " +
                          "Resources include tenants, attachements, attachment sets, BGP peers, IP routes, and virtual private networks"
                      });
                  }

                  var filePath = Path.Combine(System.AppContext.BaseDirectory, "Mind.xml");
                  options.IncludeXmlComments(filePath, true);
                  options.OperationFilter<TagByApiExplorerSettingsOperationFilter>();
                  options.DocumentFilter<TagDescriptionsDocumentFilter>();
              }
            );

            // UnitOfWork for access to Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Services containing all of our service logic
            services.AddScoped<IPortService, PortService>();
            services.AddScoped<IPortRoleService, PortRoleService>();
            services.AddScoped<IPortConnectorService, PortConnectorService>();
            services.AddScoped<IPortSfpService, PortSfpService>();
            services.AddScoped<IPortStatusService, PortStatusService>();
            services.AddScoped<IPortPoolService, PortPoolService>();
            services.AddScoped<IInterfaceService, InterfaceService>();
            services.AddScoped<ILogicalInterfaceService, LogicalInterfaceService>();
            services.AddScoped<IMtuService, MtuService>();
            services.AddScoped<IPortBandwidthService, PortBandwidthService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<ITenantAttachmentService, TenantAttachmentService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceRoleService, DeviceRoleService>();
            services.AddScoped<IDeviceRolePortRoleService, DeviceRolePortRoleService>();
            services.AddScoped<IDeviceModelService, DeviceModelService>();
            services.AddScoped<IDeviceStatusService, DeviceStatusService>();
            services.AddScoped<IRoutingInstanceService, RoutingInstanceService>();
            services.AddScoped<IRoutingInstanceTypeService, RoutingInstanceTypeService>();
            services.AddScoped<IContractBandwidthPoolService, ContractBandwidthPoolService>();
            services.AddScoped<IVpnService, VpnService>();
            services.AddScoped<IExtranetVpnMemberService, ExtranetVpnMemberService>();
            services.AddScoped<IExtranetVpnTenantCommunityInService, ExtranetVpnTenantCommunityInService>();
            services.AddScoped<IExtranetVpnTenantNetworkInService, ExtranetVpnTenantNetworkInService>();
            services.AddScoped<IRouteTargetService, RouteTargetService>();
            services.AddScoped<IRouteTargetRangeService, RouteTargetRangeService>();
            services.AddScoped<IRouteDistinguisherRangeService, RouteDistinguisherRangeService>();
            services.AddScoped<IAttachmentSetService, AttachmentSetService>();
            services.AddScoped<IAttachmentSetRoutingInstanceService, AttachmentSetRoutingInstanceService>();
            services.AddScoped<IVpnAttachmentSetService, VpnAttachmentSetService>();
            services.AddScoped<IBgpPeerService, BgpPeerService>();
            services.AddScoped<ITenantIpNetworkService, TenantIpNetworkService>();
            services.AddScoped<ITenantCommunityService, TenantCommunityService>();
            services.AddScoped<IRoutingPolicyMatchOptionService, RoutingPolicyMatchOptionService>();
            services.AddScoped<ITenantCommunitySetService, TenantCommunitySetService>();
            services.AddScoped<ITenantCommunitySetCommunityService, TenantCommunitySetCommunityService>();
            services.AddScoped<IVpnTenantIpNetworkInService, VpnTenantIpNetworkInService>();
            services.AddScoped<IVpnTenantIpNetworkRoutingInstanceStaticRouteService, VpnTenantIpNetworkRoutingInstanceStaticRouteService>();
            services.AddScoped<IVpnTenantIpNetworkOutService, VpnTenantIpNetworkOutService>();
            services.AddScoped<IVpnTenantIpNetworkRoutingInstanceService, VpnTenantIpNetworkRoutingInstanceService>();
            services.AddScoped<IVpnTenantCommunityInService, VpnTenantCommunityInService>();
            services.AddScoped<IVpnTenantCommunityOutService, VpnTenantCommunityOutService>();
            services.AddScoped<IVpnTenantCommunityRoutingInstanceService, VpnTenantCommunityRoutingInstanceService>();
            services.AddScoped<IVpnTenantIpNetworkCommunityInService, VpnTenantIpNetworkCommunityInService>();
            services.AddScoped<IVpnTenantMulticastGroupService, VpnTenantMulticastGroupService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IInfrastructureAttachmentService, InfrastructureAttachmentService>();
            services.AddScoped<IAttachmentRoleService, AttachmentRoleService>();
            services.AddScoped<IVifRoleService, VifRoleService>();
            services.AddScoped<IVifService, VifService>();
            services.AddScoped<IVlanService, VlanService>();
            services.AddScoped<IAttachmentBandwidthService, AttachmentBandwidthService>();
            services.AddScoped<IAttachmentRedundancyService, AttachmentRedundancyService>();
            services.AddScoped<IContractBandwidthService, ContractBandwidthService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ISubRegionService, SubRegionService>();
            services.AddScoped<IPlaneService, PlaneService>();
            services.AddScoped<IAddressFamilyService, AddressFamilyService>();
            services.AddScoped<IVpnTenancyTypeService, VpnTenancyTypeService>();
            services.AddScoped<IVpnTopologyTypeService, VpnTopologyTypeService>();
            services.AddScoped<IVpnProtocolTypeService, VpnProtocolTypeService>();
            services.AddScoped<IMulticastVpnServiceTypeService, MulticastVpnServiceTypeService>();
            services.AddScoped<IMulticastVpnDirectionTypeService, MulticastVpnDirectionTypeService>();
            services.AddScoped<IMulticastVpnDomainTypeService, MulticastVpnDomainTypeService>();
            services.AddScoped<IMulticastVpnRpService, MulticastVpnRpService>();
            services.AddScoped<IMulticastGeographicalScopeService, MulticastGeographicalScopeService>();
            services.AddScoped<ITenantMulticastGroupService, TenantMulticastGroupService>();
            services.AddScoped<IVpnTenantMulticastGroupService, VpnTenantMulticastGroupService>();

            // Factories for creating complex objects
            services.AddScoped<IDeviceFactory, DeviceFactory>();
            services.AddScoped<IAttachmentFactory, AttachmentFactory>();
            services.AddScoped<IVifFactory, VifFactory>();
            services.AddScoped<IRoutingInstanceFactory, RoutingInstanceFactory>();
            services.AddScoped<IVpnFactory, VpnFactory>();
            services.AddScoped<IRouteTargetFactory, RouteTargetFactory>();
            services.AddScoped<IContractBandwidthPoolFactory, ContractBandwidthPoolFactory>();
            services.AddScoped<ILogicalInterfaceFactory, LogicalInterfaceFactory>();

            // Validators to validate inbound requests from clients
            services.AddScoped<ITenantValidator, TenantValidator>();
            services.AddScoped<IContractBandwidthPoolValidator, ContractBandwidthPoolValidator>();
            services.AddScoped<IDeviceValidator, DeviceValidator>();
            services.AddScoped<IPortValidator, PortValidator>();
            services.AddScoped<IRouteTargetValidator, RouteTargetValidator>();
            services.AddScoped<IAttachmentValidator, AttachmentValidator>();
            services.AddScoped<IVifValidator, VifValidator>();
            services.AddScoped<IRoutingInstanceValidator, RoutingInstanceValidator>();
            services.AddScoped<IVpnValidator, VpnValidator>();
            services.AddScoped<IExtranetVpnMemberValidator, ExtranetVpnMemberValidator>();
            services.AddScoped<IExtranetVpnTenantCommunityInValidator, ExtranetVpnTenantCommunityInValidator>();
            services.AddScoped<IExtranetVpnTenantNetworkInValidator, ExtranetVpnTenantNetworkInValidator>();
            services.AddScoped<IMulticastVpnRpValidator, MulticastVpnRpValidator>();
            services.AddScoped<IVpnTenantMulticastGroupValidator, VpnTenantMulticastGroupValidator>();
            services.AddScoped<IVpnTenantIpNetworkStaticRouteRoutingInstanceValidator, VpnTenantIpNetworkStaticRouteRoutingInstanceValidator>();
            services.AddScoped<IAttachmentSetValidator, AttachmentSetValidator>();
            services.AddScoped<IAttachmentSetRoutingInstanceValidator, AttachmentSetRoutingInstanceValidator>();
            services.AddScoped<IVpnAttachmentSetValidator, VpnAttachmentSetValidator>();
            services.AddScoped<ITenantIpNetworkValidator, TenantIpNetworkValidator>();
            services.AddScoped<ITenantCommunityValidator, TenantCommunityValidator>();
            services.AddScoped<ITenantCommunitySetValidator, TenantCommunitySetValidator>();
            services.AddScoped<ITenantMulticastGroupValidator, TenantMulticastGroupValidator>();
            services.AddScoped<IBgpPeerValidator, BgpPeerValidator>();
            services.AddScoped<ILocationValidator, LocationValidator>();
            services.AddScoped<IVlanValidator, VlanValidator>();
            services.AddScoped<IProviderDomainAttachmentValidator, ProviderDomainAttachmentValidator>();
            services.AddScoped<IProviderDomainVifValidator, ProviderDomainVifValidator>();

            // AutoMapper - mapping engine for conversion between object graphs
            services.AddSingleton<IMapper>(sp => MapperConfiguration.CreateMapper());
        }

        /// <summary>
        /// Register components with the AutoFac DI management service
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Services
            builder.RegisterType<ProviderDomainAttachmentService>().As<IProviderDomainAttachmentService>();
            builder.RegisterType<ProviderDomainVifService>().As<IProviderDomainVifService>();
            builder.RegisterType<InfrastructureDeviceService>().As<IInfrastructureDeviceService>();
            builder.RegisterType<InfrastructurePortService>().As<IInfrastructurePortService>();
            builder.RegisterType<TenantDomainDeviceService>().As<ITenantDomainDeviceService>();
            builder.RegisterType<TenantDomainPortService>().As<ITenantDomainPortService>();
            builder.RegisterType<TenantDomainAttachmentService>().As<ITenantDomainAttachmentService>();
            builder.RegisterType<TenantDomainVifService>().As<ITenantDomainVifService>();
            builder.RegisterType<ProviderDomainBgpPeerService>().As<IProviderDomainBgpPeerService>();
            builder.RegisterType<TenantDomainBgpPeerService>().As<ITenantDomainBgpPeerService>();
            builder.RegisterType<ProviderDomainIpNetworkInboundPolicyService>().As<IProviderDomainIpNetworkInboundPolicyService>();
            builder.RegisterType<ProviderDomainCommunityInboundPolicyService>().As<IProviderDomainCommunityInboundPolicyService>();
            builder.RegisterType<TenantDomainIpNetworkInboundPolicyService>().As<ITenantDomainIpNetworkInboundPolicyService>();
            builder.RegisterType<TenantDomainCommunityInboundPolicyService>().As<ITenantDomainCommunityInboundPolicyService>();
            builder.RegisterType<ProviderDomainIpNetworkOutboundPolicyService>().As<IProviderDomainIpNetworkOutboundPolicyService>();
            builder.RegisterType<TenantDomainCommunityOutboundPolicyService>().As<ITenantDomainCommunityOutboundPolicyService>();
            builder.RegisterType<TenantDomainIpNetworkOutboundPolicyService>().As<ITenantDomainIpNetworkOutboundPolicyService>();
            builder.RegisterType<ProviderDomainCommunityOutboundPolicyService>().As<IProviderDomainCommunityOutboundPolicyService>();
            builder.RegisterType<InfrastructureAttachmentService>().As<IInfrastructureAttachmentService>();
            builder.RegisterType<InfrastructureVifService>().As<IInfrastructureVifService>();

            // Provider domain single attachment directors
            builder.RegisterType<ProviderDomainUntaggedAttachmentDirector<SingleAttachmentBuilder>>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedSingleAttachmentDirector");
            builder.RegisterType<ProviderDomainTaggedAttachmentDirector<SingleAttachmentBuilder>>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedSingleAttachmentDirector");

            // Infrastructure single attachment directors
            builder.RegisterType<InfrastructureUntaggedAttachmentDirector<SingleAttachmentBuilder>>().As<IInfrastructureAttachmentDirector>()
                .Keyed<IInfrastructureAttachmentDirector>("InfrastructureUntaggedSingleAttachmentDirector");
            builder.RegisterType<InfrastructureTaggedAttachmentDirector<SingleAttachmentBuilder>>().As<IInfrastructureAttachmentDirector>()
                .Keyed<IInfrastructureAttachmentDirector>("InfrastructureTaggedSingleAttachmentDirector");

            // Provider domain bundle attachment directors
            builder.RegisterType<ProviderDomainUntaggedBundleAttachmentDirector>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedBundleAttachmentDirector");
            builder.RegisterType<ProviderDomainTaggedBundleAttachmentDirector>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedBundleAttachmentDirector");

            // Provider domain multiport attachment directors
            builder.RegisterType<ProviderDomainUntaggedAttachmentDirector<MultiPortAttachmentBuilder>>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedMultiPortAttachmentDirector");
            builder.RegisterType<ProviderDomainTaggedAttachmentDirector<MultiPortAttachmentBuilder>>().As<IProviderDomainAttachmentDirector>()
            .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedMultiPortAttachmentDirector");

            //Provider domain single attachment update directors
            builder.RegisterType<ProviderDomainUntaggedAttachmentUpdateDirector<SingleAttachmentBuilder>>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainUntaggedSingleAttachmentUpdateDirector");
            builder.RegisterType<ProviderDomainTaggedAttachmentUpdateDirector<SingleAttachmentBuilder>>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainTaggedSingleAttachmentUpdateDirector");

            //Infrastructure single attachment update directors
            builder.RegisterType<InfrastructureUntaggedAttachmentUpdateDirector<SingleAttachmentBuilder>>().As<IInfrastructureAttachmentUpdateDirector>()
                .Keyed<IInfrastructureAttachmentUpdateDirector>("InfrastructureUntaggedSingleAttachmentUpdateDirector");
            builder.RegisterType<InfrastructureTaggedAttachmentUpdateDirector<SingleAttachmentBuilder>>().As<IInfrastructureAttachmentUpdateDirector>()
                .Keyed<IInfrastructureAttachmentUpdateDirector>("InfrastructureTaggedSingleAttachmentUpdateDirector");

            // Infrastructure bundle attachment directors
            builder.RegisterType<InfrastructureUntaggedBundleAttachmentDirector>().As<IInfrastructureAttachmentDirector>()
                .Keyed<IInfrastructureAttachmentDirector>("InfrastructureUntaggedBundleAttachmentDirector");
            builder.RegisterType<InfrastructureTaggedBundleAttachmentDirector>().As<IInfrastructureAttachmentDirector>()
                .Keyed<IInfrastructureAttachmentDirector>("InfrastructureTaggedBundleAttachmentDirector");

            //Provider domain bundle attachment update directors
            builder.RegisterType<ProviderDomainUntaggedBundleAttachmentUpdateDirector>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainUntaggedBundleAttachmentUpdateDirector");
            builder.RegisterType<ProviderDomainTaggedBundleAttachmentUpdateDirector>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainTaggedBundleAttachmentUpdateDirector");

            //Infrastructure bundle attachment update directors
            builder.RegisterType<InfrastructureUntaggedBundleAttachmentUpdateDirector>().As<IInfrastructureAttachmentUpdateDirector>()
                .Keyed<IInfrastructureAttachmentUpdateDirector>("InfrastructureUntaggedBundleAttachmentUpdateDirector");
            builder.RegisterType<InfrastructureTaggedBundleAttachmentUpdateDirector>().As<IInfrastructureAttachmentUpdateDirector>()
                .Keyed<IInfrastructureAttachmentUpdateDirector>("InfrastructureTaggedBundleAttachmentUpdateDirector");

            //Provider domain multiport attachment update directors
            builder.RegisterType<ProviderDomainUntaggedAttachmentUpdateDirector<MultiPortAttachmentBuilder>>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainUntaggedMultiPortAttachmentUpdateDirector");
            builder.RegisterType<ProviderDomainTaggedAttachmentUpdateDirector<MultiPortAttachmentBuilder>>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainTaggedMultiPortAttachmentUpdateDirector");

            // Tenant domain single attachment directors
            builder.RegisterType<TenantDomainUntaggedAttachmentDirector<SingleAttachmentBuilder>>().As<ITenantDomainAttachmentDirector>()
                .Keyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedSingleAttachmentDirector");
            builder.RegisterType<TenantDomainTaggedAttachmentDirector<SingleAttachmentBuilder>>().As<ITenantDomainAttachmentDirector>()
                .Keyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedSingleAttachmentDirector");

            // Tenant domain bundle attachment directors
            builder.RegisterType<TenantDomainUntaggedBundleAttachmentDirector>().As<ITenantDomainAttachmentDirector>()
                .Keyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedBundleAttachmentDirector");
            builder.RegisterType<TenantDomainTaggedBundleAttachmentDirector>().As<ITenantDomainAttachmentDirector>()
                .Keyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedBundleAttachmentDirector");

            // Tenant domain multiport attachment directors
            builder.RegisterType<TenantDomainUntaggedAttachmentDirector<MultiPortAttachmentBuilder>>().As<ITenantDomainAttachmentDirector>()
                .Keyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedMultiPortAttachmentDirector");
            builder.RegisterType<TenantDomainTaggedAttachmentDirector<MultiPortAttachmentBuilder>>().As<ITenantDomainAttachmentDirector>()
                .Keyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedMultiPortAttachmentDirector");

            // Tenant domain single attachment update directors
            builder.RegisterType<TenantDomainUntaggedAttachmentUpdateDirector<SingleAttachmentBuilder>>().As<ITenantDomainAttachmentUpdateDirector>()
                .Keyed<ITenantDomainAttachmentUpdateDirector>("TenantDomainUntaggedSingleAttachmentUpdateDirector");
            builder.RegisterType<TenantDomainTaggedAttachmentUpdateDirector<SingleAttachmentBuilder>>().As<ITenantDomainAttachmentUpdateDirector>()
                .Keyed<ITenantDomainAttachmentUpdateDirector>("TenantDomainTaggedSingleAttachmentUpdateDirector");

            // Tenant domain bundle attachment update directors
            builder.RegisterType<TenantDomainUntaggedBundleAttachmentUpdateDirector>().As<ITenantDomainAttachmentUpdateDirector>()
                .Keyed<ITenantDomainAttachmentUpdateDirector>("TenantDomainUntaggedBundleAttachmentUpdateDirector");
            builder.RegisterType<TenantDomainTaggedBundleAttachmentUpdateDirector>().As<ITenantDomainAttachmentUpdateDirector>()
                .Keyed<ITenantDomainAttachmentUpdateDirector>("TenantDomainTaggedBundleAttachmentUpdateDirector");

            // Attachment set directors
            builder.RegisterType<AttachmentSetDirector>().As<IAttachmentSetDirector>();
            builder.RegisterType<AttachmentSetUpdateDirector>().As<IAttachmentSetUpdateDirector>();

            // Attachment set routing instance director
            builder.RegisterType<AttachmentSetRoutingInstanceDirector>().As<IAttachmentSetRoutingInstanceDirector>();

            // Provider domain inbound policy directors
            builder.RegisterType<ProviderDomainIpNetworkInboundPolicyDirector>().As<IProviderDomainIpNetworkInboundPolicyDirector>();
            builder.RegisterType<ProviderDomainIpNetworkInboundPolicyUpdateDirector>().As<IProviderDomainIpNetworkInboundPolicyUpdateDirector>();
            builder.RegisterType<ProviderDomainCommunityInboundPolicyDirector>().As<IProviderDomainCommunityInboundPolicyDirector>();
            builder.RegisterType<ProviderDomainCommunityInboundPolicyUpdateDirector>().As<IProviderDomainCommunityInboundPolicyUpdateDirector>();

            // Tenant domain inbound policy directors
            builder.RegisterType<TenantDomainIpNetworkInboundPolicyDirector>().As<ITenantDomainIpNetworkInboundPolicyDirector>();
            builder.RegisterType<TenantDomainIpNetworkInboundPolicyUpdateDirector>().As<ITenantDomainIpNetworkInboundPolicyUpdateDirector>();
            builder.RegisterType<TenantDomainCommunityInboundPolicyDirector>().As<ITenantDomainCommunityInboundPolicyDirector>();
            builder.RegisterType<TenantDomainCommunityInboundPolicyUpdateDirector>().As<ITenantDomainCommunityInboundPolicyUpdateDirector>();

            // Provider domain outbound policy directors
            builder.RegisterType<ProviderDomainIpNetworkOutboundPolicyDirector>().As<IProviderDomainIpNetworkOutboundPolicyDirector>();
            builder.RegisterType<ProviderDomainIpNetworkOutboundPolicyUpdateDirector>().As<IProviderDomainIpNetworkOutboundPolicyUpdateDirector>();
            builder.RegisterType<ProviderDomainCommunityOutboundPolicyDirector>().As<IProviderDomainCommunityOutboundPolicyDirector>();
            builder.RegisterType<ProviderDomainCommunityOutboundPolicyUpdateDirector>().As<IProviderDomainCommunityOutboundPolicyUpdateDirector>();

            // Tenant domain outbound policy directors
            builder.RegisterType<TenantDomainIpNetworkOutboundPolicyDirector>().As<ITenantDomainIpNetworkOutboundPolicyDirector>();
            builder.RegisterType<TenantDomainIpNetworkOutboundPolicyUpdateDirector>().As<ITenantDomainIpNetworkOutboundPolicyUpdateDirector>();
            builder.RegisterType<TenantDomainCommunityOutboundPolicyDirector>().As<ITenantDomainCommunityOutboundPolicyDirector>();
            builder.RegisterType<TenantDomainCommunityOutboundPolicyUpdateDirector>().As<ITenantDomainCommunityOutboundPolicyUpdateDirector>();

            // Provider domain vif directors
            builder.RegisterType<ProviderDomainVifDirector>().As<IProviderDomainVifDirector>();
            builder.RegisterType<ProviderDomainVifUpdateDirector>().As<IProviderDomainVifUpdateDirector>();

            // Infrastructure vif directors
            builder.RegisterType<InfrastructureVifDirector>().As<IInfrastructureVifDirector>();
            builder.RegisterType<InfrastructureVifUpdateDirector>().As<IInfrastructureVifUpdateDirector>();

            // Tenant domain vif directors
            builder.RegisterType<TenantDomainVifDirector>().As<ITenantDomainVifDirector>();
            builder.RegisterType<TenantDomainVifUpdateDirector>().As<ITenantDomainVifUpdateDirector>();

            // VRF routing instance directors
            builder.RegisterType<TenantFacingVrfRoutingInstanceDirector>().As<IVrfRoutingInstanceDirector>()
                .Keyed<IVrfRoutingInstanceDirector>("TenantFacingVrfRoutingInstanceDirector");
            builder.RegisterType<InfrastructureVrfRoutingInstanceDirector>().As<IVrfRoutingInstanceDirector>()
                .Keyed<IVrfRoutingInstanceDirector>("InfrastructureVrfRoutingInstanceDirector");

            // Default routing instance director 
            builder.RegisterType<DefaultRoutingInstanceDirector>().As<IRoutingInstanceDirector>();

            // IP vpn directors
            builder.RegisterType<IpVpnDirector>().As<IVpnDirector>().Keyed<IVpnDirector>("IpVpnDirector");
            builder.RegisterType<IpVpnUpdateDirector>().As<IVpnUpdateDirector>().Keyed<IVpnUpdateDirector>("IpVpnUpdateDirector");

            // BGP Peer directors
            builder.RegisterType<ProviderDomainBgpPeerDirector>().As<IProviderDomainBgpPeerDirector>();
            builder.RegisterType<TenantDomainBgpPeerDirector>().As<ITenantDomainBgpPeerDirector>();
            builder.RegisterType<BgpPeerUpdateDirector>().As<IBgpPeerUpdateDirector>();

            // Tenant IP network directors
            builder.RegisterType<TenantIpNetworkDirector>().As<ITenantIpNetworkDirector>();
            builder.RegisterType<TenantIpNetworkUpdateDirector>().As<ITenantIpNetworkUpdateDirector>();

            // Tenant community directors
            builder.RegisterType<TenantCommunityDirector>().As<ITenantCommunityDirector>();
            builder.RegisterType<TenantCommunityUpdateDirector>().As<ITenantCommunityUpdateDirector>();

            // VPN attachment set directors
            builder.RegisterType<VpnAttachmentSetDirector>().As<IVpnAttachmentSetDirector>();
            builder.RegisterType<VpnAttachmentSetUpdateDirector>().As<IVpnAttachmentSetUpdateDirector>();

            // Tenant IP network static route directors
            builder.RegisterType<RoutingInstanceStaticRouteDirector>().As<IRoutingInstanceStaticRouteDirector>();
            builder.RegisterType<RoutingInstanceStaticRouteUpdateDirector>().As<IRoutingInstanceStaticRouteUpdateDirector>();

            // Infrastructure device directors
            builder.RegisterType<InfrastructureDeviceDirector>().As<IInfrastructureDeviceDirector>();
            builder.RegisterType<InfrastructureDeviceUpdateDirector>().As<IInfrastructureDeviceUpdateDirector>();

            // Port director
            builder.RegisterType<PortDirector>().As<IPortDirector>();
            builder.RegisterType<PortUpdateDirector>().As<IPortUpdateDirector>();

            // Tenant device director
            builder.RegisterType<TenantDomainDeviceDirector>().As<ITenantDomainDeviceDirector>();
            builder.RegisterType<TenantDomainDeviceUpdateDirector>().As<ITenantDomainDeviceUpdateDirector>();

            // Director Factories

            // Provider Domain Attachment Director Factory
            builder.Register<Func<SCM.Models.RequestModels.ProviderDomainAttachmentRequest, SCM.Models.AttachmentRole, IProviderDomainAttachmentDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (request, role) =>
                {
                    if (role.IsTaggedRole)
                    {
                        if (request.BundleRequired.HasValue && request.BundleRequired.Value)
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedBundleAttachmentDirector");
                        }
                        else if (request.MultiportRequired.HasValue && request.MultiportRequired.Value)
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedMultiPortAttachmentDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedSingleAttachmentDirector");
                        }
                    }
                    else
                    {
                        if (request.BundleRequired.HasValue && request.BundleRequired.Value)
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedBundleAttachmentDirector");
                        }
                        else if (request.MultiportRequired.HasValue && request.MultiportRequired.Value)
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedMultiPortAttachmentDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedSingleAttachmentDirector");
                        }
                    }
                };
            });

            // Infrastructure Attachment Director Factory
            builder.Register<Func<SCM.Models.RequestModels.InfrastructureAttachmentRequest, SCM.Models.AttachmentRole, IInfrastructureAttachmentDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (request, role) =>
                {
                    if (role.IsTaggedRole)
                    {
                        if (request.BundleRequired.HasValue && request.BundleRequired.Value)
                        {
                            return context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureTaggedBundleAttachmentDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureTaggedSingleAttachmentDirector");
                        }
                    }
                    else
                    {
                        if (request.BundleRequired.HasValue && request.BundleRequired.Value)
                        {
                            return context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureUntaggedBundleAttachmentDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureUntaggedSingleAttachmentDirector");
                        }
                    }
                };
            });

            // Tenant Domain Attachment Director Factory
            builder.Register<Func<SCM.Models.RequestModels.TenantDomainAttachmentRequest, SCM.Models.AttachmentRole, ITenantDomainAttachmentDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (request, role) =>
                {
                    if (role.IsTaggedRole)
                    {
                        if (request.BundleRequired.HasValue && request.BundleRequired.Value)
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedBundleAttachmentDirector");
                        }
                        else if (request.MultiportRequired.HasValue && request.MultiportRequired.Value)
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedMultiPortAttachmentDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedSingleAttachmentDirector");
                        }
                    }
                    else
                    {
                        if (request.BundleRequired.HasValue && request.BundleRequired.Value)
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedBundleAttachmentDirector");
                        }
                        else if (request.MultiportRequired.HasValue && request.MultiportRequired.Value)
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedMultiPortAttachmentDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedSingleAttachmentDirector");
                        }
                    }
                };
            });

            // Routing Instance Director Factory
            builder.Register<Func<SCM.Models.RoutingInstanceType, IVrfRoutingInstanceDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (routingInstanceType) =>
                {
                    if (routingInstanceType.Type == SCM.Models.RoutingInstanceTypeEnum.TenantFacingVrf)
                    {
                        return context.ResolveKeyed<IVrfRoutingInstanceDirector>("TenantFacingVrfRoutingInstanceDirector");
                    }
                    else if (routingInstanceType.Type == SCM.Models.RoutingInstanceTypeEnum.InfrastructureVrf)
                    {
                        return context.ResolveKeyed<IVrfRoutingInstanceDirector>("InfrastructureVrfRoutingInstanceDirector");
                    }

                    return null;
                };
            });

            // Provider Domain Attachment Update Director Factory
            builder.Register<Func<SCM.Models.Attachment, IProviderDomainAttachmentUpdateDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (attachment) =>
                {
                    if (attachment.IsTagged)
                    {
                        if (attachment.IsBundle)
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainTaggedBundleAttachmentUpdateDirector");
                        }
                        else if (attachment.IsMultiPort)
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainTaggedMultiPortAttachmentUpdateDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainTaggedSingleAttachmentUpdateDirector");
                        }
                    }
                    else
                    {
                        if (attachment.IsBundle)
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainUntaggedBundleAttachmentUpdateDirector");
                        }
                        else if (attachment.IsMultiPort)
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainUntaggedMultiPortAttachmentUpdateDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainUntaggedSingleAttachmentUpdateDirector");
                        }
                    }
                };
            });

            // Infrastructure Attachment Update Director Factory
            builder.Register<Func<SCM.Models.Attachment, IInfrastructureAttachmentUpdateDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (attachment) =>
                {
                    if (attachment.AttachmentRole.IsTaggedRole)
                    {
                        if (attachment.IsBundle)
                        {
                            return context.ResolveKeyed<IInfrastructureAttachmentUpdateDirector>("InfrastructureTaggedBundleAttachmentUpdateDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<IInfrastructureAttachmentUpdateDirector>("InfrastructureTaggedSingleAttachmentUpdateDirector");
                        }
                    }
                    else
                    {
                        if (attachment.IsBundle)
                        {
                            return context.ResolveKeyed<IInfrastructureAttachmentUpdateDirector>("InfrastructureUntaggedBundleAttachmentUpdateDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<IInfrastructureAttachmentUpdateDirector>("InfrastructureUntaggedSingleAttachmentUpdateDirector");
                        }
                    }
                };
            });

            // Tenant Domain Attachment Update Director Factory
            builder.Register<Func<SCM.Models.Attachment, ITenantDomainAttachmentUpdateDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (attachment) =>
                {
                    if (attachment.AttachmentRole.IsTaggedRole)
                    {
                        if (attachment.IsBundle)
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentUpdateDirector>("TenantDomainTaggedBundleAttachmentUpdateDirector");
                        }
                        else if (attachment.IsMultiPort)
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentUpdateDirector>("TenantDomainTaggedMultiPortAttachmentUpdateDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentUpdateDirector>("TenantDomainTaggedSingleAttachmentUpdateDirector");
                        }
                    }
                    else
                    {
                        if (attachment.IsBundle)
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentUpdateDirector>("TenantDomainUntaggedBundleAttachmentUpdateDirector");
                        }
                        else if (attachment.IsMultiPort)
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentUpdateDirector>("TenantDomainUntaggedMultiPortAttachmentUpdateDirector");
                        }
                        else
                        {
                            return context.ResolveKeyed<ITenantDomainAttachmentUpdateDirector>("TenantDomainUntaggedSingleAttachmentUpdateDirector");
                        }
                    }
                };
            });

            // VPN Director Factory
            builder.Register<Func<Mind.Models.RequestModels.VpnRequest, IVpnDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (request) =>
                {
                    if (request.AddressFamily == Models.RequestModels.AddressFamilyEnum.IPv4)
                    {
                        return context.ResolveKeyed<IVpnDirector>("IpVpnDirector");
                    }

                    return null;
                };
            });

            // VPN Update Director Factory
            builder.Register<Func<SCM.Models.Vpn, IVpnUpdateDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (vpn) =>
                {
                    if (vpn.AddressFamily.Name == "IPv4")
                    {
                        return context.ResolveKeyed<IVpnUpdateDirector>("IpVpnUpdateDirector");
                    }

                    return null;
                };
            });

            //Builders
            builder.RegisterType<InfrastructureDeviceBuilder>().As<IInfrastructureDeviceBuilder>();
            builder.RegisterType<InfrastructureDeviceUpdateBuilder>().As<IInfrastructureDeviceUpdateBuilder>();
            builder.RegisterType<TenantDomainDeviceBuilder>().As<ITenantDomainDeviceBuilder>();
            builder.RegisterType<TenantDomainDeviceUpdateBuilder>().As<ITenantDomainDeviceUpdateBuilder>();
            builder.RegisterType<PortBuilder>().As<IPortBuilder>();
            builder.RegisterType<PortUpdateBuilder>().As<IPortUpdateBuilder>();
            builder.RegisterType<SingleAttachmentBuilder>().As<IAttachmentBuilder<SingleAttachmentBuilder>>();
            builder.RegisterType<BundleAttachmentBuilder>().As<IBundleAttachmentBuilder>();
            builder.RegisterType<MultiPortAttachmentBuilder>().As<IAttachmentBuilder<MultiPortAttachmentBuilder>>();
            builder.RegisterType<VrfRoutingInstanceBuilder>().As<IVrfRoutingInstanceBuilder>();
            builder.RegisterType<DefaultRoutingInstanceBuilder>().As<IDefaultRoutingInstanceBuilder>();
            builder.RegisterType<AttachmentSetBuilder>().As<IAttachmentSetBuilder>();
            builder.RegisterType<AttachmentSetUpdateBuilder>().As<IAttachmentSetUpdateBuilder>();
            builder.RegisterType<AttachmentSetRoutingInstanceBuilder>().As<IAttachmentSetRoutingInstanceBuilder>();
            builder.RegisterType<TenantIpNetworkInboundPolicyBuilder>().As<ITenantIpNetworkInboundPolicyBuilder>();
            builder.RegisterType<TenantIpNetworkOutboundPolicyBuilder>().As<ITenantIpNetworkOutboundPolicyBuilder>();
            builder.RegisterType<TenantCommunityInboundPolicyBuilder>().As<ITenantCommunityInboundPolicyBuilder>();
            builder.RegisterType<TenantCommunityOutboundPolicyBuilder>().As<ITenantCommunityOutboundPolicyBuilder>();
            builder.RegisterType<VifBuilder>().As<IVifBuilder>();
            builder.RegisterType<VifUpdateBuilder>().As<IVifUpdateBuilder>();
            builder.RegisterType<IpVpnBuilder>().As<IIpVpnBuilder>();
            builder.RegisterType<IpVpnUpdateBuilder>().As<IIpVpnUpdateBuilder>();
            builder.RegisterType<BgpPeerBuilder>().As<IBgpPeerBuilder>();
            builder.RegisterType<BgpPeerUpdateBuilder>().As<IBgpPeerUpdateBuilder>();
            builder.RegisterType<TenantIpNetworkBuilder>().As<ITenantIpNetworkBuilder>();
            builder.RegisterType<TenantCommunityBuilder>().As<ITenantCommunityBuilder>();
            builder.RegisterType<VpnAttachmentSetBuilder>().As<IVpnAttachmentSetBuilder>();
            builder.RegisterType<VpnAttachmentSetUpdateBuilder>().As<IVpnAttachmentSetUpdateBuilder>();
            builder.RegisterType<RoutingInstanceStaticRouteBuilder>().As<IRoutingInstanceStaticRouteBuilder>();
        }
 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApiVersionDescriptionProvider provider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSignalR(routes => routes.MapHub<NetworkSyncHub>("/networkSyncHub"));
            app.UseWebSockets();

            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
