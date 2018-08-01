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
using SCM.Api.Models;
using SCM.Models.NetModels.IpVpnNetModels;
using SCM.Models.NetModels.Ipv4MulticastVpnNetModels;
using SCM.Models.NetModels.AttachmentNetModels;
using SCM.Models.SerializableModels;
using SCM.Data;
using SCM.Services;
using SCM.Api.Validators;
using SCM.Factories;
using SCM.Validators;
using Newtonsoft.Json;
using SCM.Hubs;
using SCM.Api;
using NSwag.AspNetCore;
using System.Reflection;
using NJsonSchema;

namespace SCM
{
    public class Startup
    {
        private MapperConfiguration MapperConfiguration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        
        MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperServiceModelProfileConfiguration());
                cfg.AddProfile(new AutoMapperViewModelProfileConfiguration());
                cfg.AddProfile(new AutoMapperApiModelProfileConfiguration());
                cfg.AddProfile(new AutoMapperAttachmentServiceProfileConfiguration());
                cfg.AddProfile(new AutoMapperIpv4VpnServiceProfileConfiguration());
                cfg.AddProfile(new AutoMapperIpv4MulticastVpnServiceProfileConfiguration());
                cfg.AddProfile(new AutoMapperSerializableIpv4VpnServiceProfileConfiguration());
                cfg.AddProfile(new AutoMapperSerializableIpv4MulticastVpnServiceProfileConfiguration());
                cfg.AddProfile(new AutoMapperSerializableAttachmentServiceProfileConfiguration());
            });
        }

        public IConfiguration Configuration { get; }

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
                .AddJsonOptions(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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

            // Network service for managing REST API access to the network server (NSO)
            services.AddScoped<INetworkSyncService, NetworkSyncService>();

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

            // API Input Validators to perform extra validation for inbound web API requests
            services.AddScoped<ITenantApiValidator, TenantApiValidator>();
            services.AddScoped<IDeviceApiValidator, DeviceApiValidator>();
            services.AddScoped<IAttachmentApiValidator, AttachmentApiValidator>();
            services.AddScoped<IVifApiValidator, VifApiValidator>();
            services.AddScoped<IVpnApiValidator, VpnApiValidator>();
            services.AddScoped<IAttachmentSetApiValidator, AttachmentSetApiValidator>();
            services.AddScoped<IAttachmentSetRoutingInstanceApiValidator, AttachmentSetRoutingInstanceApiValidator>();
            services.AddScoped<IVpnAttachmentSetApiValidator, VpnAttachmentSetApiValidator>();

            // Filter attribute to validate the requests for network sync/checksync services
            services.AddScoped<ValidateNetworkServiceRequestAttribute>();

            // AutoMapper - mapping engine for conversion between object graphs
            services.AddSingleton<IMapper>(sp => MapperConfiguration.CreateMapper());

            // JSON serializer - need to ignore reference loops for cases where objects to be serialized by SignalR are self-referencing
            // Create our own serializer to ignore reference loops and add to the DI container

            var settings = new JsonSerializerSettings { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore };
            var serializer = JsonSerializer.Create(settings);
            services.AddSingleton(serializer);
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

            app.UseStaticFiles();

            // Enable the Swagger UI middleware and the Swagger generator
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });

            app.UseSignalR(routes => routes.MapHub<NetworkSyncHub>("/networkSyncHub"));
            app.UseWebSockets();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
