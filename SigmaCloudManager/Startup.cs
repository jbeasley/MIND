using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SCM.Data;
using SCM.Services;
using Newtonsoft.Json;
using SCM.Hubs;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Mind.Api.Models;
using Mind.Services;
using Mind.Builders;
using Autofac;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Mind.WebUI.Models;
using Microsoft.Extensions.DependencyModel.Resolution;
using Microsoft.Extensions.DependencyModel;
using Microsoft.DotNet.PlatformAbstractions;
using System.Reflection;
using Mind.Directors;
using SCM.Models;

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
                cfg.AddProfile(new AutoMapperViewModelProfileConfiguration());
                cfg.AddProfile(new AutoMapperApiModelProfileConfiguration());
            });

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
               
               Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // DB context for our Sigma repository
            services.AddDbContext<SigmaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
                                                
            // SignalR - library for async updates to clients. This is used by the API controllers to 
            // update clients on progress during network sync/checksync operations
            services.AddSignalR();

            //register the MyViewLocationExpander into ViewLocationExpanders  
            // This is needed because our MVC views are located in a non-default location (i.e. in the WebUI folder)
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new WebUIViewLocationExpander());
            });

            // Add framework services.
            var mvcBuilder = services.AddMvc();
            // Following is needed so that MVC can find external libraries, such as the IO.Swagger DLL which provides
            // the API towards NSO
            new MvcConfiguration().ConfigureMvc(mvcBuilder);

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
                  foreach (var description in provider.ApiVersionDescriptions)
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
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IVpnService, VpnService>();
            services.AddScoped<IAttachmentSetService, AttachmentSetService>();
            services.AddScoped<IAttachmentSetRoutingInstanceService, AttachmentSetRoutingInstanceService>();
            services.AddScoped<IVpnAttachmentSetService, VpnAttachmentSetService>();
            services.AddScoped<IBgpPeerService, BgpPeerService>();
            services.AddScoped<ITenantIpNetworkService, TenantIpNetworkService>();
            services.AddScoped<ITenantCommunityService, TenantCommunityService>();
            services.AddScoped<IVpnTenantIpNetworkInService, VpnTenantIpNetworkInService>();
            services.AddScoped<IVpnTenantIpNetworkRoutingInstanceStaticRouteService, VpnTenantIpNetworkRoutingInstanceStaticRouteService>();
            services.AddScoped<IVpnTenantIpNetworkOutService, VpnTenantIpNetworkOutService>();
            services.AddScoped<IInfrastructureAttachmentService, InfrastructureAttachmentService>();

            // AutoMapper - mapping engine for conversion between object graphs
            services.AddSingleton<IMapper>(sp => MapperConfiguration.CreateMapper());

            // Configure Swagger 'Nova' API client for accessing the RESTCONF API of Cisco (Tail-F) Network Service Orchestrator (NSO)
            var novaClientApiSettings = Configuration.GetSection("NovaClientApiSettings").Get<NovaClientApiSettings>();

            // Swagger client API for accessing the Attachment API of NSO 
            services.AddScoped<IO.NovaAttSwagger.Api.IDataApi>(sp => new IO.NovaAttSwagger.Api.DataApi(new IO.NovaAttSwagger.Client.Configuration
            {
                Username = novaClientApiSettings.UserName,
                Password = novaClientApiSettings.Password,
                BasePath = novaClientApiSettings.BasePath
            }));

            // Swagger client API for accessing the VPN API of NSO 
            services.AddScoped<IO.NovaVpnSwagger.Api.IDataApi>(sp => new IO.NovaVpnSwagger.Api.DataApi(new IO.NovaVpnSwagger.Client.Configuration
            {
                Username = novaClientApiSettings.UserName,
                Password = novaClientApiSettings.Password,
                BasePath = novaClientApiSettings.BasePath
            }));
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
            builder.RegisterType<InfrastructureLogicalInterfaceService>().As<IInfrastructureLogicalInterfaceService>();
            builder.RegisterType<ProviderDomainLogicalInterfaceService>().As<IProviderDomainLogicalInterfaceService>();
            builder.RegisterType<ProviderDomainLocationService>().As<IProviderDomainLocationService>();
            builder.RegisterType<ProviderDomainRoutingInstanceService>().As<IProviderDomainRoutingInstanceService>();

            // Provider domain single attachment directors
            builder.RegisterType<ProviderDomainUntaggedAttachmentDirector<SingleAttachmentBuilder>>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedSingleAttachmentDirector");
            builder.RegisterType<ProviderDomainTaggedAttachmentDirector<SingleAttachmentBuilder>>().As<IProviderDomainAttachmentDirector>()
                .Keyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedSingleAttachmentDirector");
            builder.RegisterType<ProviderDomainTaggedAttachmentDirector<SingleAttachmentBuilder>>().As<INetworkSynchronizable<Attachment>>()
                .Keyed<INetworkSynchronizable<Attachment>>("ProviderDomainTaggedSingleAttachmentDirector");

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
                
            // Infrastructure bundle attachment directors
            builder.RegisterType<InfrastructureUntaggedBundleAttachmentDirector>().As<IInfrastructureAttachmentDirector>()
                .Keyed<IInfrastructureAttachmentDirector>("InfrastructureUntaggedBundleAttachmentDirector");
            builder.RegisterType<InfrastructureTaggedBundleAttachmentDirector>().As<IInfrastructureAttachmentDirector>()
                .Keyed<IInfrastructureAttachmentDirector>("InfrastructureTaggedBundleAttachmentDirector");

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
                
            // Attachment set directors
            builder.RegisterType<AttachmentSetDirector>().As<IAttachmentSetDirector>();
            builder.RegisterType<AttachmentSetUpdateDirector>().As<IAttachmentSetUpdateDirector>();

            // Attachment set routing instance director
            builder.RegisterType<AttachmentSetRoutingInstanceDirector>().As<IAttachmentSetRoutingInstanceDirector>();
            builder.RegisterType<AttachmentSetRoutingInstanceDirector>().As<IDestroyable<SCM.Models.AttachmentSetRoutingInstance>>();

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
            builder.RegisterType<ProviderDomainVifDirector>().As<IProviderDomainVifDirector>().Keyed<IProviderDomainVifDirector>("ProviderDomainVifDirector");
            builder.RegisterType<ProviderDomainVifDirector>().As<INetworkSynchronizable<Vif>>().Keyed<INetworkSynchronizable<Vif>>("ProviderDomainVifDirector");
            builder.RegisterType<ProviderDomainVifDirector>().As<IDestroyable<Vif>>().Keyed<IDestroyable<Vif>>("ProviderDomainVifDirector");

            // Infrastructure vif directors
            builder.RegisterType<InfrastructureVifDirector>().As<IInfrastructureVifDirector>();
            builder.RegisterType<InfrastructureVifUpdateDirector>().As<IInfrastructureVifUpdateDirector>();
            builder.RegisterType<InfrastructureVifDirector>().As<IDestroyable<Vif>>().Keyed<IDestroyable<Vif>>("InfrastructureVifDirector");

            // Tenant domain vif directors
            builder.RegisterType<TenantDomainVifDirector>().As<ITenantDomainVifDirector>();
            builder.RegisterType<TenantDomainVifUpdateDirector>().As<ITenantDomainVifUpdateDirector>();
            builder.RegisterType<TenantDomainVifDirector>().As<IDestroyable<Vif>>().Keyed<IDestroyable<Vif>>("TenantDomainVifDirector");

            // Infrastructure Logical interface director
            builder.RegisterType<InfrastructureLogicalInterfaceDirector>().As<IInfrastructureLogicalInterfaceDirector>();

            // Provider Domain Logical interface director
            builder.RegisterType<ProviderDomainLogicalInterfaceDirector>().As<IProviderDomainLogicalInterfaceDirector>();

            // VRF routing instance directors
            builder.RegisterType<TenantFacingVrfRoutingInstanceDirector>().As<IVrfRoutingInstanceDirector>()
                .Keyed<IVrfRoutingInstanceDirector>("TenantFacingVrfRoutingInstanceDirector");
            builder.RegisterType<InfrastructureVrfRoutingInstanceDirector>().As<IVrfRoutingInstanceDirector>()
                .Keyed<IVrfRoutingInstanceDirector>("InfrastructureVrfRoutingInstanceDirector");

            // Default routing instance director 
            builder.RegisterType<DefaultRoutingInstanceDirector>().As<IRoutingInstanceDirector>();

            // IP vpn directors
            builder.RegisterType<IpVpnDirector>().As<IVpnDirector>().Keyed<IVpnDirector>("IpVpnDirector");
            builder.RegisterType<IpVpnDirector>().As<IDestroyable<SCM.Models.Vpn>>().Keyed<IDestroyable<SCM.Models.Vpn>>("IpVpnDirector");
            builder.RegisterType<IpVpnDirector>().As<INetworkSynchronizable<SCM.Models.Vpn>>().Keyed<INetworkSynchronizable<SCM.Models.Vpn>>("IpVpnDirector");

            // BGP Peer directors
            builder.RegisterType<ProviderDomainBgpPeerDirector>().As<IProviderDomainBgpPeerDirector>();
            builder.RegisterType<TenantDomainBgpPeerDirector>().As<ITenantDomainBgpPeerDirector>();
            builder.RegisterType<BgpPeerUpdateDirector>().As<IBgpPeerUpdateDirector>();

            // Tenant IP network directors
            builder.RegisterType<TenantIpNetworkDirector>().As<ITenantIpNetworkDirector>();
            builder.RegisterType<TenantIpNetworkUpdateDirector>().As<ITenantIpNetworkUpdateDirector>();

            // Tenant community directors
            builder.RegisterType<TenantCommunityDirector>().As<ITenantCommunityDirector>();

            // VPN attachment set directors
            builder.RegisterType<VpnAttachmentSetDirector>().As<IVpnAttachmentSetDirector>();
            builder.RegisterType<VpnAttachmentSetUpdateDirector>().As<IVpnAttachmentSetUpdateDirector>();

            // Tenant IP network static route directors
            builder.RegisterType<RoutingInstanceStaticRouteDirector>().As<IRoutingInstanceStaticRouteDirector>();
            builder.RegisterType<RoutingInstanceStaticRouteUpdateDirector>().As<IRoutingInstanceStaticRouteUpdateDirector>();

            // Infrastructure device directors
            builder.RegisterType<InfrastructureDeviceDirector>().As<IInfrastructureDeviceDirector>();

            // Port director
            builder.RegisterType<PortDirector>().As<IPortDirector>();

            // Tenant device director
            builder.RegisterType<TenantDomainDeviceDirector>().As<ITenantDomainDeviceDirector>();

            // Director Factories

            // Provider Domain Attachment Director Factory
            builder.Register<Func<SCM.Models.RequestModels.ProviderDomainAttachmentRequest, SCM.Models.AttachmentRole, IProviderDomainAttachmentDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (request, role) =>
                {
                    return role.IsTaggedRole
                        ? request.BundleRequired.GetValueOrDefault()
                            ? context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedBundleAttachmentDirector")
                            : request.MultiportRequired.GetValueOrDefault()
                            ? context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedMultiPortAttachmentDirector")
                            : context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedSingleAttachmentDirector")
                        : request.BundleRequired.GetValueOrDefault()
                            ? context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedBundleAttachmentDirector")
                            : request.MultiportRequired.GetValueOrDefault()
                                ? context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedMultiPortAttachmentDirector")
                                : context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedSingleAttachmentDirector");
                };
            });

            // Infrastructure Attachment Director Factory
            builder.Register<Func<SCM.Models.RequestModels.InfrastructureAttachmentRequest, SCM.Models.AttachmentRole, IInfrastructureAttachmentDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (request, role) =>
                {
                    return role.IsTaggedRole
                        ? request.BundleRequired.GetValueOrDefault()
                            ? context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureTaggedBundleAttachmentDirector")
                            : context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureTaggedSingleAttachmentDirector")
                        : request.BundleRequired.GetValueOrDefault()
                            ? context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureUntaggedBundleAttachmentDirector")
                            : context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureUntaggedSingleAttachmentDirector");
                };
            });

            // Tenant Domain Attachment Director Factory
            builder.Register<Func<SCM.Models.RequestModels.TenantDomainAttachmentRequest, SCM.Models.AttachmentRole, ITenantDomainAttachmentDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (request, role) =>
                {
                    return role.IsTaggedRole
                        ? request.BundleRequired.GetValueOrDefault()
                            ? context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedBundleAttachmentDirector")
                            : request.MultiportRequired.GetValueOrDefault()
                                ? context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedMultiPortAttachmentDirector")
                                : context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedSingleAttachmentDirector")
                        : request.BundleRequired.GetValueOrDefault()
                            ? context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedBundleAttachmentDirector")
                            : request.MultiportRequired.GetValueOrDefault()
                                ? context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedMultiPortAttachmentDirector")
                                : context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedSingleAttachmentDirector");
                };
            });

            // Provider Domain Network Synchronizable Attachment Director Factory - returns a director which provides methods
            // to sync an attachment with the network. Only currently supported for non-bundle, non-multiport tagged attachments
            builder.Register<Func<SCM.Models.Attachment, INetworkSynchronizable<SCM.Models.Attachment>>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (attachment) =>
                {
                    return !attachment.IsBundle && !attachment.IsMultiPort && attachment.IsTagged
                        ? context.ResolveKeyed<INetworkSynchronizable<SCM.Models.Attachment>>("ProviderDomainTaggedSingleAttachmentDirector")
                        : null;
                };
            });

            // Provider Domain Network Synchonizable Vif Director Factory - returns a director which provides methods
            // to sync a vif with the network. Only currently supported for vifs configured under non-bundle, non-multiport tagged attachments
            builder.Register<Func<SCM.Models.Attachment, INetworkSynchronizable<SCM.Models.Vif>>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (attachment) =>
                {
                    return !attachment.IsBundle && !attachment.IsMultiPort && attachment.IsTagged
                        ? context.ResolveKeyed<INetworkSynchronizable<SCM.Models.Vif>>("ProviderDomainVifDirector")
                        : null;
                };
            });

            // Provider Domain Network Destroyable Vif Director Factory - returns a director which provides methods
            // to destroy vifs.
            builder.Register<Func<SCM.Models.PortRole, IDestroyable<SCM.Models.Vif>>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (portRole) =>
                {
                    var portRoleType = portRole.PortRoleType;
                    return portRoleType == PortRoleTypeEnum.TenantFacing
                        ? context.ResolveKeyed<IDestroyable<SCM.Models.Vif>>("ProviderDomainVifDirector")
                        : portRoleType == PortRoleTypeEnum.ProviderInfrastructure
                            ? context.ResolveKeyed<IDestroyable<SCM.Models.Vif>>("InfrastructureVifDirector")
                            : portRoleType == PortRoleTypeEnum.TenantInfrastructure
                                                    ? context.ResolveKeyed<IDestroyable<SCM.Models.Vif>>("TenantDomainVifDirector")
                                                    : null;
                };
            });

            // Provider Domain Attachment Update Director Factory
            builder.Register<Func<SCM.Models.Attachment, IProviderDomainAttachmentDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (attachment) =>
                {
                    return attachment.IsTagged
                        ? attachment.IsBundle
                            ? context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedBundleAttachmentDirector")
                            : attachment.IsMultiPort
                            ? context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedMultiPortAttachmentDirector")
                            : context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainTaggedSingleAttachmentDirector")
                        : attachment.IsBundle
                            ? context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedBundleAttachmentDirector")
                            : attachment.IsMultiPort
                                ? context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedMultiPortAttachmentDirector")
                                : context.ResolveKeyed<IProviderDomainAttachmentDirector>("ProviderDomainUntaggedSingleAttachmentDirector");
                };
            });

            // Infrastructure Attachment Update Director Factory
            builder.Register<Func<SCM.Models.Attachment, IInfrastructureAttachmentDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (attachment) =>
                {
                    return attachment.IsTagged
                        ? attachment.IsBundle
                            ? context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureTaggedBundleAttachmentDirector")
                            : context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureTaggedSingleAttachmentDirector")
                        : attachment.IsBundle
                            ? context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureUntaggedBundleAttachmentDirector")
                            : context.ResolveKeyed<IInfrastructureAttachmentDirector>("InfrastructureUntaggedSingleAttachmentDirector");
                };
            });

            // Tenant Domain Attachment Update Director Factory
            builder.Register<Func<SCM.Models.Attachment, ITenantDomainAttachmentDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (attachment) =>
                {
                    return attachment.IsTagged
                        ? attachment.IsBundle
                            ? context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedBundleAttachmentDirector")
                            : attachment.IsMultiPort
                                ? context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedMultiPortAttachmentDirector")
                                : context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainTaggedSingleAttachmentDirector")
                        : attachment.IsBundle
                            ? context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedBundleAttachmentDirector")
                            : attachment.IsMultiPort
                                ? context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedMultiPortAttachmentDirector")
                                : context.ResolveKeyed<ITenantDomainAttachmentDirector>("TenantDomainUntaggedSingleAttachmentDirector");
                };
            });

            // Routing Instance Director Factory
            builder.Register<Func<SCM.Models.RoutingInstanceType, IVrfRoutingInstanceDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (routingInstanceType) =>
                {
                    return routingInstanceType.Type == SCM.Models.RoutingInstanceTypeEnum.TenantFacingVrf
                        ? context.ResolveKeyed<IVrfRoutingInstanceDirector>("TenantFacingVrfRoutingInstanceDirector")
                        : routingInstanceType.Type == SCM.Models.RoutingInstanceTypeEnum.InfrastructureVrf
                        ? context.ResolveKeyed<IVrfRoutingInstanceDirector>("InfrastructureVrfRoutingInstanceDirector")
                        : null;
                };
            });

            // VPN Director Factory
            builder.Register<Func<Mind.Models.RequestModels.VpnRequest, IVpnDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (request) =>
                {
                    return request.AddressFamily == Models.RequestModels.AddressFamilyEnum.IPv4
                        ? context.ResolveKeyed<IVpnDirector>("IpVpnDirector")
                        : null;
                };
            });

            // Destroyable VPN Director Factory - returns a director which provides methods
            // to destroy vpns.
            builder.Register<Func<SCM.Models.Vpn, IDestroyable<SCM.Models.Vpn>>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (vpn) =>
                {
                    return vpn.AddressFamily.Name == "IPv4"
                        ? context.ResolveKeyed<IDestroyable<SCM.Models.Vpn>>("IpVpnDirector")
                            : null;
                };
            });

            // VPN Update Director Factory
            builder.Register<Func<SCM.Models.Vpn, IVpnDirector>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (vpn) =>
                {
                    return vpn.AddressFamily.Name == "IPv4" 
                              ? context.ResolveKeyed<IVpnDirector>("IpVpnDirector")
                                  : null;
                };
            });

            // Network Sync VPN Director Factory - returns a director which provides methods
            // to sync vpns with the network.
            builder.Register<Func<SCM.Models.Vpn, INetworkSynchronizable<SCM.Models.Vpn>>>((c, p) =>
            {
                var context = c.Resolve<IComponentContext>();
                return (vpn) =>
                {
                    return vpn.AddressFamily.Name == "IPv4"
                        ? context.ResolveKeyed<INetworkSynchronizable<SCM.Models.Vpn>>("IpVpnDirector")
                            : null;
                };
            });

            //Builders
            builder.RegisterType<InfrastructureDeviceBuilder>().As<IInfrastructureDeviceBuilder>();
            builder.RegisterType<TenantDomainDeviceBuilder>().As<ITenantDomainDeviceBuilder>();
            builder.RegisterType<PortBuilder>().As<IPortBuilder>();
            builder.RegisterType<SingleAttachmentBuilder>().As<IAttachmentBuilder<SingleAttachmentBuilder>>();                 
            builder.RegisterType<BundleAttachmentBuilder>().As<IBundleAttachmentBuilder>();
            builder.RegisterType<MultiPortAttachmentBuilder>().As<IAttachmentBuilder<MultiPortAttachmentBuilder>>();                 
            builder.RegisterType<VrfRoutingInstanceBuilder>().As<IVrfRoutingInstanceBuilder>();
            builder.RegisterType<DefaultRoutingInstanceBuilder>().As<IDefaultRoutingInstanceBuilder>();
            builder.RegisterType<AttachmentSetBuilder>().As<IAttachmentSetBuilder>();
            builder.RegisterType<AttachmentSetRoutingInstanceBuilder>().As<IAttachmentSetRoutingInstanceBuilder>();
            builder.RegisterType<TenantIpNetworkInboundPolicyBuilder>().As<ITenantIpNetworkInboundPolicyBuilder>();
            builder.RegisterType<TenantIpNetworkOutboundPolicyBuilder>().As<ITenantIpNetworkOutboundPolicyBuilder>();
            builder.RegisterType<TenantCommunityInboundPolicyBuilder>().As<ITenantCommunityInboundPolicyBuilder>();
            builder.RegisterType<TenantCommunityOutboundPolicyBuilder>().As<ITenantCommunityOutboundPolicyBuilder>();
            builder.RegisterType<VifBuilder>().As<IVifBuilder>();
            builder.RegisterType<IpVpnBuilder>().As<IIpVpnBuilder>();
            builder.RegisterType<BgpPeerBuilder>().As<IBgpPeerBuilder>();
            builder.RegisterType<TenantIpNetworkBuilder>().As<ITenantIpNetworkBuilder>();
            builder.RegisterType<TenantCommunityBuilder>().As<ITenantCommunityBuilder>();
            builder.RegisterType<VpnAttachmentSetBuilder>().As<IVpnAttachmentSetBuilder>();
            builder.RegisterType<RoutingInstanceStaticRouteBuilder>().As<IRoutingInstanceStaticRouteBuilder>();
            builder.RegisterType<LogicalInterfaceBuilder>().As<ILogicalInterfaceBuilder>();

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

    public class MvcConfiguration : IDesignTimeMvcBuilderConfiguration
    {
        public void ConfigureMvc(IMvcBuilder builder)
        {
            // .NET Core SDK v1 does not pick up reference assemblies so
            // they have to be added for Razor manually. Resolved for
            // SDK v2 by https://github.com/dotnet/sdk/pull/876 OR SO WE THOUGHT
            /*builder.AddRazorOptions(razor =>
            {
                razor.AdditionalCompilationReferences.Add(
                    MetadataReference.CreateFromFile(
                        typeof(PdfHttpHandler).Assembly.Location));
            });*/

            // .NET Core SDK v2 does not resolve reference assemblies' paths
            // at all, so we have to hack around with reflection
            typeof(CompilationLibrary)
                .GetTypeInfo()
                .GetDeclaredField("<DefaultResolver>k__BackingField")
                .SetValue(null, new CompositeCompilationAssemblyResolver(new ICompilationAssemblyResolver[]
                {
                    new DirectReferenceAssemblyResolver(),
                    new AppBaseCompilationAssemblyResolver(),
                    new ReferenceAssemblyPathResolver(),
                    new PackageCompilationAssemblyResolver(),
                }));
        }

        private class DirectReferenceAssemblyResolver : ICompilationAssemblyResolver
        {
            public bool TryResolveAssemblyPaths(CompilationLibrary library, List<string> assemblies)
            {
                if (!string.Equals(library.Type, "reference", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                var paths = new List<string>();

                foreach (var assembly in library.Assemblies)
                {
                    var path = Path.Combine(ApplicationEnvironment.ApplicationBasePath, assembly);

                    if (!File.Exists(path))
                    {
                        return false;
                    }

                    paths.Add(path);
                }

                assemblies.AddRange(paths);

                return true;
            }
        }
    }
}
