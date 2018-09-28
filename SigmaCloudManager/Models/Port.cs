using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SCM.Models
{
    public static class PortQueryableExtensions
    {
        public static IQueryable<Port> IncludeValidationProperties(this IQueryable<Port> query)
        {
            return query.Include(x => x.Device.DeviceRole)
                        .Include(x => x.PortBandwidth)
                        .Include(x => x.PortConnector)
                        .Include(x => x.PortPool.PortRole.DeviceRolePortRoles)
                        .Include(x => x.PortSfp)
                        .Include(x => x.PortStatus)
                        .Include(x => x.Tenant);
        }

        public static IQueryable<Port> IncludeDeleteValidationProperties(this IQueryable<Port> query)
        {
            return query.Include(x => x.PortStatus)
                        .Include(x => x.Tenant)
                        .Include(x => x.Interface);
        }

        public static IQueryable<Port> IncludeDeepProperties(this IQueryable<Port> query)
        {
            return query.Include(x => x.Device.DeviceRole)
                        .Include(x => x.Interface)
                        .Include(x => x.PortBandwidth)
                        .Include(x => x.PortConnector)
                        .Include(x => x.PortPool)
                        .Include(x => x.PortSfp)
                        .Include(x => x.PortStatus)
                        .Include(x => x.Tenant)
                        .Include(x => x.PortPool.PortRole);
        }
    }

    public class Port : IModifiableResource
    {
        public int ID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{Type} {Name}";
            }
        }
        public int PortBandwidthID { get; set; }
        public int DeviceID { get; set; }
        public int? TenantID { get; set; }
        public int? InterfaceID { get; set; }
        public int PortSfpID { get; set; }
        public int PortConnectorID { get; set; }
        public int PortPoolID { get; set; }
        public int PortStatusID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Device Device { get; set; }
        public virtual PortSfp PortSfp { get; set; }
        public virtual PortConnector PortConnector { get; set; }
        public virtual PortStatus PortStatus { get; set; }
        public virtual PortPool PortPool { get; set; }
        public virtual Interface Interface { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual PortBandwidth PortBandwidth { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the port
        /// </summary>
        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(this.Type)) throw new IllegalStateException("The port requires a type to be defined.");
            if (string.IsNullOrEmpty(this.Name)) throw new IllegalStateException("The port requires a name to be defined.");
            if (this.Device == null) throw new IllegalStateException($"Port '{this.FullName}' requires a device association but a device was not found.");
            if (this.PortSfp == null) throw new IllegalStateException($"Port '{this.FullName}' requires a sfp association but a sfp was not found.");
            if (this.PortBandwidth == null) throw new IllegalStateException($"Port '{this.FullName}' requires a bandwidth association but a bandwidth was not found.");
            if (this.PortConnector == null) throw new IllegalStateException($"Port '{this.FullName}' requires a connector but a connector was not found.");
            if (this.PortPool == null) throw new IllegalStateException($"Port '{this.FullName}' requires a port pool association but a port pool was not found.");
            if (this.PortStatus == null) throw new IllegalStateException($"Port '{this.FullName}' requires a status association but a status was not found.");
            if (!this.PortPool.PortRole.DeviceRolePortRoles
                                       .Where(x =>
                                                x.DeviceRoleID == this.Device.DeviceRole.DeviceRoleID)
                                       .Any())
            {
                throw new IllegalStateException($"The port pool of '{this.PortPool.Name}' assigned to port '{this.FullName}' is not valid for the " +
                    $"device with device role '{this.Device.DeviceRole.Name}'.");
            }

            if (this.Tenant != null)
            {
                if (this.PortPool.PortRole.PortRoleType != PortRoleTypeEnum.TenantFacing)
                {
                    throw new IllegalStateException($"A tenant cannot be assigned to port '{this.FullName}' because the port role of " +
                        $"'{this.PortPool.PortRole.Name}' does not support the allocation of the port to a tenant. The port role must be 'tenant-facing'.");
                }

                if (this.PortStatus.PortStatusType != PortStatusTypeEnum.Assigned)
                {
                    throw new IllegalStateException($"The port status for port '{this.FullName}' must be 'Assigned' in order to allocate the port " +
                        $"to a tenant.");
                }
            }

            if (this.PortStatus.PortStatusType == PortStatusTypeEnum.Assigned)
            {
                if (this.Device.DeviceRole.IsProviderDomainRole)
                {
                    if (this.Tenant == null) throw new IllegalStateException($"Port '{this.FullName}' must be assigned to a tenant in order for the port status " +
                        $"to be set to 'Assigned'.");
                }
            }

            if (this.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.TenantFacing)
            {
                if (this.Interface != null)
                {
                    if (this.Tenant == null) throw new IllegalStateException($"Port '{this.FullName}' must be assigned to a tenant but a tenant " +
                        $"was not found.");
                }
            }
        }

        /// <summary>
        /// Validate the port can be deleted
        /// </summary>
        public virtual void ValidateDelete()
        {
            if (this.Tenant != null) throw new IllegalDeleteAttemptException($"Port '{this.FullName}' cannot be deleted because it is assigned to tenant " +
                $"'{this.Tenant.Name}'.");
            if (this.PortStatus.PortStatusType == PortStatusTypeEnum.Assigned) throw new IllegalDeleteAttemptException($"Port '{this.FullName}' " +
                $"cannot be deleted because the port status is 'Assigned'");
        }
    }
}