using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public static class DeviceQueryableExtensions
    {
        public static IQueryable<Device> IncludeValidationProperties(this IQueryable<Device> query)
        {
            return query.Include(x => x.Location)
                        .Include(x => x.Attachments)
                        .Include(x => x.DeviceModel)
                        .Include(x => x.DeviceRole)
                        .Include(x => x.DeviceStatus)
                        .Include(x => x.Interfaces)
                        .Include(x => x.Plane)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.PortConnector)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.PortSfp)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.PortStatus)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.Tenant)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.PortBandwidth)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.PortPool.PortRole.DeviceRolePortRoles)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.Device.DeviceRole)
                        .Include(x => x.Tenant);                     
        }

        public static IQueryable<Device> IncludeDeleteValidationProperties(this IQueryable<Device> query)
        {
            return query.Include(x => x.Ports)
                        .ThenInclude(x => x.PortStatus)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.Tenant);
        }

        public static IQueryable<Device> IncludeDeepProperties(this IQueryable<Device> query)
        {
            return query.Include(x => x.Location)
                        .Include(x => x.Attachments)
                        .ThenInclude(x => x.Interfaces)
                        .ThenInclude(x => x.Ports)
                        .Include(x => x.Attachments)
                        .ThenInclude(x => x.Vifs)
                        .ThenInclude(x => x.Vlans)
                        .Include(x => x.Attachments)
                        .ThenInclude(x => x.Vifs)
                        .ThenInclude(x => x.Attachment.Interfaces)
                        .ThenInclude(x => x.Ports)
                        .Include(x => x.DeviceModel)
                        .Include(x => x.DeviceRole)
                        .Include(x => x.DeviceStatus)
                        .Include(x => x.Interfaces)
                        .Include(x => x.Plane)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.PortStatus)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.PortPool)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.PortConnector)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.PortSfp)
                        .Include(x => x.Ports)
                        .ThenInclude(x => x.Tenant)
                        .Include(x => x.Tenant);
        }
    }

        public class Device: IModifiableResource
    {
        public int DeviceID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public int DeviceRoleID { get; set; }
        public int DeviceModelID { get; set; }
        public int DeviceStatusID { get; set; }
        public int LocationID { get; set; }
        public int? PlaneID { get; set; }
        public int? TenantID { get; set; }
        public bool UseLayer2InterfaceMtu { get; set; }
        public bool Created { get; set; }
        public bool RequiresSync { get; set; }
        public bool ShowCreatedAlert { get; set; }
        public bool ShowRequiresSyncAlert { get; set; }
        [MaxLength(250)]
        public string Notes { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual DeviceRole DeviceRole { get; set; }
        public virtual DeviceModel DeviceModel { get; set; }
        public virtual DeviceStatus DeviceStatus { get; set; }
        public virtual Location Location { get; set;}
        public virtual Plane Plane { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<Port> Ports { get; set; }
        public virtual ICollection<Interface> Interfaces { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<RoutingInstance> RoutingInstances { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the device
        /// </summary>
        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(Name)) throw new IllegalStateException("A name for the device is required.");
            if (this.DeviceRole == null) throw new IllegalStateException($"A device role for device '{this.Name}' is required but was not found.");
            if (this.DeviceStatus == null) throw new IllegalStateException($"A device status for device '{this.Name}' is required but was not found.");
            if (this.Location == null) throw new IllegalStateException($"A location for device '{this.Name}' is required but was not found.");
            if (this.DeviceModel == null) throw new IllegalStateException($"A device model for device '{this.Name}' is required but was not found.");
            if (this.DeviceRole.IsProviderDomainRole)
            {
                if (this.Plane == null) throw new IllegalStateException($"A plane for device '{this.Name}' is required because the device is designated as " +
                    $"a provider domain infrastructure device.");
                if (this.Tenant != null) throw new IllegalStateException($"A tenant for device '{this.Name}' was found but it not required because the device " +
                    $"is designated as a provider domain infrastructure device.");
            }
            else if (this.DeviceRole.IsTenantDomainRole)
            {
                if (this.Plane != null) throw new IllegalStateException($"A plane for device '{this.Name}' was found but is not required because the device " +
                    $"is designated as a tenant domain device.");
                if (this.Tenant == null) throw new IllegalStateException($"A tenant for device '{this.Name}' is required but was not found because the device " +
                    $"is designated as a tenant domain device.");
            }
            if (this.DeviceStatus.DeviceStatusType != DeviceStatusTypeEnum.Production)
            {
                if (this.Ports.Any(
                    q =>
                    q.PortStatus.PortStatusType == PortStatusTypeEnum.Assigned))
                {
                    throw new IllegalStateException($"The status of device '{this.Name}' must be 'production' because one or more ports are in the " +
                        $"'assigned' state.");
                }
            }

            if (this.DeviceRole.IsTenantDomainRole)
            {
                if (this.Tenant == null) throw new IllegalStateException($"A tenant for device '{this.Name}' is required but was not fonund");
            }
            else
            {
                if (this.Tenant != null) throw new IllegalStateException($"A tenant for device '{this.Name}' was found but is not required.");
            }

            // Validate each port which belongs to the device
            this.Ports
                .ToList()
                .ForEach(
                    port =>
                        port.Validate()
                );
        }

        /// <summary>
        /// Validate the device can be deleted
        /// </summary>
        public virtual void ValidateDelete()
        {
            this.Ports
                .ToList()
                .ForEach(
                    port =>
                        port.ValidateDelete()
                );
        }
    }
}