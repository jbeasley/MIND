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

namespace SCM
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
            services.Configure<ApplicationConfiguration>(Configuration.GetSection("ApplicationConfiguration"));

            // SignalR - library for async updates to clients. This is used by the API controllers to 
            // update clients on progress during network sync/checksync operations
            services.AddSignalR();

            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(o =>
                {
                    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    o.SerializerSettings.Converters.Add(new StringEnumConverter
                    {
                        CamelCaseText = true
                    });
                });

            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("1.0.0", new Info
                    {
                        Version = "1.0.0",
                        Title = "MIND API",
                        Description = "MIND API (ASP.NET Core 2.0)",
                        Contact = new Contact()
                        {
                            Name = "Swagger Codegen Contributors",
                            Url = "https://github.com/swagger-api/swagger-codegen",
                            Email = "jonathan.beasley@thomsonreuters.com"
                        },
                        TermsOfService = ""
                    });
                    c.CustomSchemaIds(type => type.FriendlyId(true));
                    c.DescribeAllEnumsAsStrings();
                    c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");
                    // Sets the basePath property in the Swagger document generated
                    c.DocumentFilter<BasePathFilter>("/v1");

                    // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g required, pattern, ..)
                    // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                    c.OperationFilter<GeneratePathParamsValidationFilter>();
                });

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
            services.AddScoped<ITenantNetworkService, TenantNetworkService>();
            services.AddScoped<ITenantCommunityService, TenantCommunityService>();
            services.AddScoped<IRoutingPolicyMatchOptionService, RoutingPolicyMatchOptionService>();
            services.AddScoped<ITenantCommunitySetService, TenantCommunitySetService>();
            services.AddScoped<ITenantCommunitySetCommunityService, TenantCommunitySetCommunityService>();
            services.AddScoped<IVpnTenantNetworkInService, VpnTenantNetworkInService>();
            services.AddScoped<IVpnTenantNetworkStaticRouteRoutingInstanceService, VpnTenantNetworkStaticRouteRoutingInstanceService>();
            services.AddScoped<IVpnTenantNetworkOutService, VpnTenantNetworkOutService>();
            services.AddScoped<IVpnTenantNetworkRoutingInstanceService, VpnTenantNetworkRoutingInstanceService>();
            services.AddScoped<IVpnTenantCommunityInService, VpnTenantCommunityInService>();
            services.AddScoped<IVpnTenantCommunityOutService, VpnTenantCommunityOutService>();
            services.AddScoped<IVpnTenantCommunityRoutingInstanceService, VpnTenantCommunityRoutingInstanceService>();
            services.AddScoped<IVpnTenantNetworkCommunityInService, VpnTenantNetworkCommunityInService>();
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
            services.AddScoped<IProviderDomainAttachmentService, ProviderDomainAttachmentService>();

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
            services.AddScoped<IVpnTenantNetworkStaticRouteRoutingInstanceValidator, VpnTenantNetworkStaticRouteRoutingInstanceValidator>();
            services.AddScoped<IAttachmentSetValidator, AttachmentSetValidator>();
            services.AddScoped<IAttachmentSetRoutingInstanceValidator, AttachmentSetRoutingInstanceValidator>();
            services.AddScoped<IVpnAttachmentSetValidator, VpnAttachmentSetValidator>();
            services.AddScoped<ITenantNetworkValidator, TenantNetworkValidator>();
            services.AddScoped<ITenantCommunityValidator, TenantCommunityValidator>();
            services.AddScoped<ITenantCommunitySetValidator, TenantCommunitySetValidator>();
            services.AddScoped<ITenantMulticastGroupValidator, TenantMulticastGroupValidator>();
            services.AddScoped<IBgpPeerValidator, BgpPeerValidator>();
            services.AddScoped<ILocationValidator, LocationValidator>();
            services.AddScoped<IVlanValidator, VlanValidator>();

            // AutoMapper - mapping engine for conversion between object graphs
            services.AddSingleton<IMapper>(sp => MapperConfiguration.CreateMapper());
        }

        /// <summary>
        /// Register components with the AutoFac DI management service
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ProviderDomainAttachmentService>().As<IProviderDomainAttachmentService>();
            builder.RegisterType<ProviderDomainAttachmentDirector>().As<IProviderDomainAttachmentDirector>();
            builder.RegisterType<AttachmentBuilder>().As<IAttachmentBuilder>().Keyed<IAttachmentBuilder>("Attachment");
            builder.RegisterType<BundleAttachmentBuilder>().As<IAttachmentBuilder>().Keyed<IAttachmentBuilder>("BundleAttachment");
            builder.Register<Func<SCM.Models.RequestModels.ProviderDomainAttachmentRequest, IAttachmentBuilder>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (attachmentRequest) =>
                {   if (attachmentRequest.BundleRequired.HasValue)
                    {
                        if (attachmentRequest.BundleRequired.Value)
                        {
                            return context.ResolveKeyed<IAttachmentBuilder>("BundleAttachment");
                        }
                    }

                    return context.ResolveKeyed<IAttachmentBuilder>("Attachment");
                };
            });

            builder.RegisterType<VrfRoutingInstanceBuilder>().As<IRoutingInstanceBuilder>().Keyed<IRoutingInstanceBuilder>("VRF");
            builder.Register<Func<SCM.Models.RoutingInstanceType, IRoutingInstanceBuilder>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (routingInstanceType) =>
                {
                    if (routingInstanceType.IsVrf)
                    {
                        return context.ResolveKeyed<IRoutingInstanceBuilder>("VRF");
                    }

                    return null;
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
                    c.SwaggerEndpoint("/swagger/1.0.0/swagger.json", "MIND API");

                    //TODO: Or alternatively use the original Swagger contract that's included in the static files
                    // c.SwaggerEndpoint("/swagger-original.json", "MIND API Original");
                });
        }
    }
}
