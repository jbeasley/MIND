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

            // Directors
            builder.RegisterType<ProviderDomainAttachmentDirector<SingleAttachmentBuilder>>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainSingleAttachmentDirector");
            builder.RegisterType<ProviderDomainAttachmentUpdateDirector<SingleAttachmentUpdateBuilder>>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainSingleAttachmentUpdateDirector");
            builder.RegisterType<ProviderDomainBundleAttachmentDirector>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainBundleAttachmentDirector");
            builder.RegisterType<ProviderDomainBundleAttachmentUpdateDirector>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainBundleAttachmentUpdateDirector");
            builder.RegisterType<ProviderDomainAttachmentDirector<MultiPortAttachmentBuilder>>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainMultiPortAttachmentDirector");
            builder.RegisterType<ProviderDomainAttachmentUpdateDirector<MultiPortAttachmentUpdateBuilder>>().As<IProviderDomainAttachmentUpdateDirector>()
                .Keyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainMultiPortAttachmentUpdateDirector");
            builder.RegisterType<AttachmentSetDirector>().As<IAttachmentSetDirector>();
            builder.RegisterType<AttachmentSetUpdateDirector>().As<IAttachmentSetUpdateDirector>();
            builder.RegisterType<AttachmentSetRoutingInstanceDirector>().As<IAttachmentSetRoutingInstanceDirector>();
            builder.RegisterType<VpnTenantIpNetworkInDirector>().As<IVpnTenantIpNetworkInDirector>();
            builder.RegisterType<VpnTenantIpNetworkInUpdateDirector>().As<IVpnTenantIpNetworkInUpdateDirector>();
            builder.RegisterType<VpnTenantIpNetworkOutDirector>().As<IVpnTenantIpNetworkOutDirector>();
            builder.RegisterType<VpnTenantIpNetworkOutUpdateDirector>().As<IVpnTenantIpNetworkOutUpdateDirector>();
            builder.RegisterType<ProviderDomainVifDirector>().As<IProviderDomainVifDirector>();
            builder.RegisterType<ProviderDomainVifUpdateDirector>().As<IProviderDomainVifUpdateDirector>();
            builder.RegisterType<TenantFacingVrfRoutingInstanceDirector>().As<IVrfRoutingInstanceDirector>()
                .Keyed<IVrfRoutingInstanceDirector>("TenantFacingVrfRoutingInstanceDirector");
            builder.RegisterType<DefaultRoutingInstanceDirector>().As<IRoutingInstanceDirector>();
            builder.RegisterType<IpVpnDirector>().As<IVpnDirector>().Keyed<IVpnDirector>("IpVpnDirector");
            builder.RegisterType<IpVpnUpdateDirector>().As<IVpnUpdateDirector>().Keyed<IVpnUpdateDirector>("IpVpnUpdateDirector");
            builder.RegisterType<BgpPeerDirector>().As<IBgpPeerDirector>();
            builder.RegisterType<BgpPeerUpdateDirector>().As<IBgpPeerUpdateDirector>();
            builder.RegisterType<TenantIpNetworkDirector>().As<ITenantIpNetworkDirector>();
            builder.RegisterType<TenantIpNetworkUpdateDirector>().As<ITenantIpNetworkUpdateDirector>();
            builder.RegisterType<VpnAttachmentSetDirector>().As<IVpnAttachmentSetDirector>();
            builder.RegisterType<VpnAttachmentSetUpdateDirector>().As<IVpnAttachmentSetUpdateDirector>();
            builder.RegisterType<VpnTenantIpNetworkRoutingInstanceStaticRouteDirector>().As<IVpnTenantIpNetworkRoutingInstanceStaticRouteDirector>();
            builder.RegisterType<VpnTenantIpNetworkRoutingInstanceStaticRouteUpdateDirector>().As<IVpnTenantIpNetworkRoutingInstanceStaticRouteUpdateDirector>();
            builder.RegisterType<InfrastructureDeviceDirector>().As<IInfrastructureDeviceDirector>();
            builder.RegisterType<InfrastructureDeviceUpdateDirector>().As<IInfrastructureDeviceUpdateDirector>();
            builder.RegisterType<PortDirector>().As<IPortDirector>();
            builder.RegisterType<PortUpdateDirector>().As<IPortUpdateDirector>();

            // Director Factories
            builder.Register<Func<SCM.Models.RequestModels.ProviderDomainAttachmentRequest, IProviderDomainAttachmentDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (request) =>
                {
                    if (request.BundleRequired != null)
                    {
                        if (request.BundleRequired.Value)
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainBundleAttachmentDirector");
                        }
                    }
                    if (request.MultiportRequired != null)
                    {
                        if (request.MultiportRequired.Value)
                        {
                            return context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainMultiPortAttachmentDirector");
                        }
                    }

                    return context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainSingleAttachmentDirector");
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
                    if (attachment.IsBundle)
                    {
                        return context.ResolveKeyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainBundleAttachmentUpdateDirector");
                    }
                    else if (attachment.IsMultiPort)
                    {
                        return context.ResolveKeyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainMultiPortAttachmentUpdateDirector");
                    }
                    else
                    {
                        return context.ResolveKeyed<IProviderDomainAttachmentUpdateDirector>("ProviderDomainSingleAttachmentUpdateDirector");
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
