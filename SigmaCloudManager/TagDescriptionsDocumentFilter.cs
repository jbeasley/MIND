using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind
{
    public class TagDescriptionsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new[]
            {
                new Tag { Name = "Tenants", Description = "Manage tenants." },

                new Tag { Name = "Tenant IP Networks", Description = "Manage IPv4 networks for tenants." },

                new Tag { Name = "Tenant Communities", Description = "Manage BGP communities for tenants." },

                new Tag { Name = "Tenant Domain Devices", Description = "Manage devices within the tenant domain, such as CPE." },

                new Tag { Name = "Tenant Domain Ports", Description = "Manage ports for devices in the tenant domain, such as CPE ports." },

                new Tag { Name = "Tenant Domain Attachments", Description = "Manage network attachments in the tenant domain, " +
                "such as the attachment of a CPE to a firewall or data-center switch." },

                new Tag { Name = "Tenant Domain Virtual Interfaces", Description = "Manage virtual interfaces (VIFs) in the tenant domain, such as the " +
                "creation of a vlan between a CPE and a data-center switch." },

                new Tag { Name = "Tenant Domain BGP Peering", Description = "Manage BGP peering in the tenant domain, such as BGP peers established between " +
                "a CPE and a PE or between a CPE and a data-center CE." },

                new Tag { Name = "Tenant Domain IP Network Inbound Policy", Description = "Manage inbound routing policy based upon IP networks within the " +
                "tenant domain. For example, control which tenant IP networks are accepted by the tenant domain from the provider domain." },

                new Tag { Name = "Tenant Domain IP Network Outbound Policy", Description = "Manage outbound routing policy based upon IP networks within the " +
                "tenant domain. For example, control which tenant IP networks are advertised from the tenant domain towards the provider domain."},

                new Tag { Name = "Tenant Domain Community Inbound Policy", Description = "Manage inbound routing policy based upon communities within the " +
                "tenant domain. For example, control which tenant communities are accepted within the tenant domain from the provider domain." },

                new Tag { Name = "Tenant Domain Community Outbound Policy", Description = "Manage outbound routing policy based upon communities within the " +
                "tenant domain. For example, control which tenant communities are advertised from the tenant domain towards the provider domain." },

                new Tag { Name = "Tenant Domain IP Network Static Routing", Description = "" },

                new Tag { Name = "Provider Domain Tenant Attachments", Description = "Manage network attachments for connecting the tenant domain " +
                "to the provider domain, such as the attachment of a CPE to a PE." },

                new Tag { Name = "Provider Domain Tenant Virtual Interfaces", Description = "Manage virtual interfaces (VIFs) for connecting the tenant domain " +
                "to the provider domain, such as vlans between a PE and a CPE." },

                new Tag { Name = "Provider Domain Tenant Attachment Sets", Description = "Manage attachment sets for the resilient attachment of a tenant domain to the provider domain." },

                new Tag { Name = "Provider Domain Tenant Attachment Set Routing Instances", Description = "Manage the membership of routing instances with attachment sets." },

                new Tag { Name = "Provider Domain Tenant BGP Peering", Description = "Manage BGP peering from the provider domain to the tenant domain, such as BGP peers established " +
                "between a PE and a CPE." },

                new Tag { Name = "Provider Domain Tenant IP Network Inbound Policy", Description = "Manage inbound routing policy for controlling acceptance " +
                "of IP networks received from the tenant domain into the provider domain." },

                new Tag { Name = "Provider Domain Tenant IP Network Outbound Policy", Description = "Manage outbound routing policy for controlling the advertisement of IP networks " +
                "from the provider domain towards the tenant domain." },

                new Tag { Name = "Provider Domain Tenant Community Inbound Policy", Description = "Manage inbound routing policy for controlling acceptance " +
                "of communities received from the tenant domain into the provider domain." },

                new Tag { Name = "Provider Domain Tenant Community Outbound Policy", Description = "Manage outbound routing policy for controlling the advertisement of communities " +
                "from the provider domain towards the tenant domain." },

                new Tag { Name = "Provider Domain Tenant IP Network Static Routing", Description = "Manage static routing for tenant IP networks in the provider domain." },

                new Tag { Name = "Infrastructure Devices", Description = "Manage infrastructure devices, such as PEs, P, aggregation routers." },

                new Tag { Name = "Infrastructure Ports", Description = "Manage ports for infrastructure devices." },

                new Tag { Name = "Infrastructure Attachments", Description = "Manage network attachments between infratructure devices, such as the attachment of a " +
                "PE to a P router. " },

                new Tag { Name = "Infrastructure Virtual Interfaces", Description = "Manage virtual interfaces (VIFs) for infrastructure, such as vlans between a " +
                "PE and an aggregation router." },

                new Tag {Name = "Virtual Private Networks", Description = "Manage virtual private networks which can be used to provide either private or shared " +
                "network connectivity between tenant domains."},

                 new Tag {Name = "Provider Domain Attachment Set To VPN Bindings", Description = "Manage bindings of attachment sets to virtual private networks. " +
                 "Bindings of attachment sets to vpns allows network communication between attached tenant domains."}

            };
        }
    }
}
