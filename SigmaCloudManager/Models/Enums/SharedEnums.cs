using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Models
{
    /// <summary>
    /// Port role type enumeration
    /// </summary>
    public enum PortRoleTypeEnum
    {
        /// <summary>
        /// The tenant facing port role type. This denotes a port in the provider domain which 
        /// faces the tenant domain (e.g. a PE port facing a CE).
        /// </summary>
        TenantFacing,

        /// <summary>
        /// The provider infrastructure port role type. This denotes a port in the provider domain which 
        /// faces other provider infrastructure devies (e.g. a PE port facing a P node).
        /// </summary>
        ProviderInfrastructure,

        /// <summary>
        /// The tenant infrastructure port role type. This denotes a port in the tenant domain which 
        /// faces other tenant infrastructure devices (e.g. a CE port facing a firewall).
        /// </summary>
        TenantInfrastructure,

        /// <summary>
        /// The provider facing port role type. This denotes a port in the tenant domain which 
        /// faces the provider domain (e.g. a CE port facing a PE).
        /// </summary>
        ProviderFacing
    }

    /// <summary>
    /// Network status enumeration
    /// </summary>
    public enum NetworkStatusEnum
    {
        /// <summary>
        /// Not staged enum
        /// </summary>
        NotStaged = 0,

        /// <summary>
        /// Staged emum
        /// </summary>
        Staged = 1,

        /// <summary>
        /// Active enum
        /// </summary>
        Active = 2,

        /// <summary>
        /// Activation failure enum
        /// </summary>
        ActivationFailure = 3,

        /// <summary>
        /// Staged Inconsistent enum
        /// </summary>
        StagedInconsistent = 4,
    }

    /// <summary>
    /// Enumeration of tenant IP routing behaviour options
    /// </summary>
    /// <value>Enumerated list of tenant ip routing behaviour options</value>
    public enum TenantIpRoutingBehaviourEnum
    {
        /// <summary>
        /// Enum for Any-Plane
        /// </summary>
        AnyPlane = 1,

        /// <summary>
        /// Enum for Red-Plane
        /// </summary>
        RedPlane = 2,

        /// <summary>
        /// Enum for Blue-Plane
        /// </summary>
        BluePlane = 3
    }

    /// <summary>
    /// Enumeration of tenant environnent options
    /// </summary>
    /// <value>Enumerated list of tenant environment options</value>
    public enum TenantEnvironmentEnum
    {
        /// <summary>
        /// Enum for Development
        /// </summary>
        Development = 1,

        /// <summary>
        /// Enum for Staging
        /// </summary>
        Staging = 2,

        /// <summary>
        /// Enum for Production
        /// </summary>
        Production = 3
    }
}
