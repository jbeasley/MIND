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
            services.AddMvc()
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

            services.AddMvcCore()
                    .AddVersionedApiExplorer(o =>
                    {
                        o.GroupNameFormat = "'v'VVV";
                        o.SubstituteApiVersionInUrl = true;
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

                  options.OperationFilter<SwaggerDefaultValues>();
                  var filePath = Path.Combine(System.AppContext.BaseDirectory, "Mind.xml");
                  options.IncludeXmlComments(filePath, true);
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
            builder.RegisterType<TenantDeviceService>().As<ITenantDeviceService>();
            builder.RegisterType<TenantPortService>().As<ITenantPortService>();
            builder.RegisterType<TenantDomainAttachmentService>().As<ITenantDomainAttachmentService>();

            // Provider domain single attachment directors
            builder.RegisterType<ProviderDomainUntaggedAttachmentDirector<SingleAttachmentBuilder>>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedSingleAttachmentDirector");
            builder.RegisterType<ProviderDomainTaggedAttachmentDirector<SingleAttachmentBuilder>>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedSingleAttachmentDirector");

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
            builder.RegisterType<ProviderDomainUntaggedAttachmentUpdateDirector<SingleAttachmentUpdateBuilder>>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainUntaggedSingleAttachmentUpdateDirector");
            builder.RegisterType<ProviderDomainTaggedAttachmentUpdateDirector<SingleAttachmentUpdateBuilder>>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainTaggedSingleAttachmentUpdateDirector");

            //Provider domain bundle attachment update directors
            builder.RegisterType<ProviderDomainUntaggedBundleAttachmentUpdateDirector>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainUntaggedBundleAttachmentUpdateDirector");
            builder.RegisterType<ProviderDomainTaggedBundleAttachmentUpdateDirector>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainTaggedBundleAttachmentUpdateDirector");

            //Provider domain multiport attachment update directors
            builder.RegisterType<ProviderDomainUntaggedAttachmentUpdateDirector<MultiPortAttachmentUpdateBuilder>>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainUntaggedMultiPortAttachmentUpdateDirector");
            builder.RegisterType<ProviderDomainTaggedAttachmentUpdateDirector<MultiPortAttachmentUpdateBuilder>>().As<IProviderDomainAttachmentUpdateDirector>()
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
            builder.RegisterType<TenantDomainUntaggedAttachmentUpdateDirector<SingleAttachmentUpdateBuilder>>().As<ITenantDomainAttachmentUpdateDirector>()
                .Keyed<ITenantDomainAttachmentUpdateDirector>("TenantDomainUntaggedSingleAttachmentUpdateDirector");
            builder.RegisterType<TenantDomainTaggedAttachmentUpdateDirector<SingleAttachmentUpdateBuilder>>().As<ITenantDomainAttachmentUpdateDirector>()
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

            // Attachment set inbound policy directors
            builder.RegisterType<VpnTenantIpNetworkInDirector>().As<IVpnTenantIpNetworkInDirector>();
            builder.RegisterType<VpnTenantIpNetworkInUpdateDirector>().As<IVpnTenantIpNetworkInUpdateDirector>();

            // Attachment set outbound policy directors
            builder.RegisterType<VpnTenantIpNetworkOutDirector>().As<IVpnTenantIpNetworkOutDirector>();
            builder.RegisterType<VpnTenantIpNetworkOutUpdateDirector>().As<IVpnTenantIpNetworkOutUpdateDirector>();

            // Provider domain vif directors
            builder.RegisterType<ProviderDomainVifDirector>().As<IProviderDomainVifDirector>();
            builder.RegisterType<ProviderDomainVifUpdateDirector>().As<IProviderDomainVifUpdateDirector>();

            // VRF routing instance director
            builder.RegisterType<TenantFacingVrfRoutingInstanceDirector>().As<IVrfRoutingInstanceDirector>()
                .Keyed<IVrfRoutingInstanceDirector>("TenantFacingVrfRoutingInstanceDirector");

            // Default routing instance director 
            builder.RegisterType<DefaultRoutingInstanceDirector>().As<IRoutingInstanceDirector>();

            // IP vpn directors
            builder.RegisterType<IpVpnDirector>().As<IVpnDirector>().Keyed<IVpnDirector>("IpVpnDirector");
            builder.RegisterType<IpVpnUpdateDirector>().As<IVpnUpdateDirector>().Keyed<IVpnUpdateDirector>("IpVpnUpdateDirector");

            // BGP Peer directors
            builder.RegisterType<BgpPeerDirector>().As<IBgpPeerDirector>();
            builder.RegisterType<BgpPeerUpdateDirector>().As<IBgpPeerUpdateDirector>();

            // Tenant IP network directors
            builder.RegisterType<TenantIpNetworkDirector>().As<ITenantIpNetworkDirector>();
            builder.RegisterType<TenantIpNetworkUpdateDirector>().As<ITenantIpNetworkUpdateDirector>();

            // VPN attachment set directors
            builder.RegisterType<VpnAttachmentSetDirector>().As<IVpnAttachmentSetDirector>();
            builder.RegisterType<VpnAttachmentSetUpdateDirector>().As<IVpnAttachmentSetUpdateDirector>();

            // Tenant IP network static route directors
            builder.RegisterType<VpnTenantIpNetworkRoutingInstanceStaticRouteDirector>().As<IVpnTenantIpNetworkRoutingInstanceStaticRouteDirector>();
            builder.RegisterType<VpnTenantIpNetworkRoutingInstanceStaticRouteUpdateDirector>().As<IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateDirector>();

            // Infrastructure device directors
            builder.RegisterType<InfrastructureDeviceDirector>().As<IInfrastructureDeviceDirector>();
            builder.RegisterType<InfrastructureDeviceUpdateDirector>().As<IInfrastructureDeviceUpdateDirector>();

            // Port director
            builder.RegisterType<PortDirector>().As<IPortDirector>();
            builder.RegisterType<PortUpdateDirector>().As<IPortUpdateDirector>();

            // Tenant device director
            builder.RegisterType<TenantDeviceDirector>().As<ITenantDeviceDirector>();
            builder.RegisterType<TenantDeviceUpdateDirector>().As<ITenantDeviceUpdateDirector>();

            // Director Factories
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


            builder.Register<Func<SCM.Models.RoutingInstanceType, IVrfRoutingInstanceDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (routingInstanceType) =>
                {
                    if (routingInstanceType.Type == SCM.Models.RoutingInstanceTypeEnum.TenantFacingVrf)
                    {
                        return context.ResolveKeyed<IVrfRoutingInstanceDirector>("TenantFacingVrfRoutingInstanceDirector");
                    }

                    return null;
                };
            });

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
            builder.RegisterType<TenantDeviceBuilder>().As<ITenantDeviceBuilder>();
            builder.RegisterType<TenantDeviceUpdateBuilder>().As<ITenantDeviceUpdateBuilder>();
            builder.RegisterType<PortBuilder>().As<IPortBuilder>();
            builder.RegisterType<PortUpdateBuilder>().As<IPortUpdateBuilder>();
            builder.RegisterType<SingleAttachmentBuilder>().As<IAttachmentBuilder<SingleAttachmentBuilder>>();
            builder.RegisterType<BundleAttachmentBuilder>().As<IBundleAttachmentBuilder>();
            builder.RegisterType<MultiPortAttachmentBuilder>().As<IAttachmentBuilder<MultiPortAttachmentBuilder>>();
            builder.RegisterType<VrfRoutingInstanceBuilder>().As<IVrfRoutingInstanceBuilder>();
            builder.RegisterType<DefaultRoutingInstanceBuilder>().As<IDefaultRoutingInstanceBuilder>();
            builder.RegisterType<SingleAttachmentUpdateBuilder>().As<IAttachmentUpdateBuilder<SingleAttachmentUpdateBuilder>>();
            builder.RegisterType<MultiPortAttachmentUpdateBuilder>().As<IAttachmentUpdateBuilder<MultiPortAttachmentUpdateBuilder>>();
            builder.RegisterType<BundleAttachmentUpdateBuilder>().As<IBundleAttachmentUpdateBuilder>();
            builder.RegisterType<AttachmentSetBuilder>().As<IAttachmentSetBuilder>();
            builder.RegisterType<AttachmentSetUpdateBuilder>().As<IAttachmentSetUpdateBuilder>();
            builder.RegisterType<AttachmentSetRoutingInstanceBuilder>().As<IAttachmentSetRoutingInstanceBuilder>();
            builder.RegisterType<VpnTenantIpNetworkInBuilder>().As<IVpnTenantIpNetworkInBuilder>();
            builder.RegisterType<VpnTenantIpNetworkInUpdateBuilder>().As<IVpnTenantIpNetworkInUpdateBuilder>();
            builder.RegisterType<VpnTenantIpNetworkOutBuilder>().As<IVpnTenantIpNetworkOutBuilder>();
            builder.RegisterType<VpnTenantIpNetworkOutUpdateBuilder>().As<IVpnTenantIpNetworkOutUpdateBuilder>();
            builder.RegisterType<VifBuilder>().As<IVifBuilder>();
            builder.RegisterType<VifUpdateBuilder>().As<IVifUpdateBuilder>();
            builder.RegisterType<IpVpnBuilder>().As<IIpVpnBuilder>();
            builder.RegisterType<IpVpnUpdateBuilder>().As<IIpVpnUpdateBuilder>();
            builder.RegisterType<BgpPeerBuilder>().As<IBgpPeerBuilder>();
            builder.RegisterType<BgpPeerUpdateBuilder>().As<IBgpPeerUpdateBuilder>();
            builder.RegisterType<TenantIpNetworkBuilder>().As<ITenantIpNetworkBuilder>();
            builder.RegisterType<TenantIpNetworkUpdateBuilder>().As<ITenantIpNetworkUpdateBuilder>();
            builder.RegisterType<VpnAttachmentSetBuilder>().As<IVpnAttachmentSetBuilder>();
            builder.RegisterType<VpnAttachmentSetUpdateBuilder>().As<IVpnAttachmentSetUpdateBuilder>();
            builder.RegisterType<VpnTenantIpNetworkRoutingInstanceStaticRouteBuilder>().As<IVpnTenantIpNetworkRoutingInstanceStaticRouteBuilder>();
            builder.RegisterType<VpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder>().As<IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateBuilder>();
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
