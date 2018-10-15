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
    public static class LogicalInterfaceQueryableExtensions
    {
        public static IQueryable<LogicalInterface> IncludeValidationProperties(this IQueryable<LogicalInterface> query)
        {
            return query.Include(x => x.RoutingInstance);
        }

        public static IQueryable<LogicalInterface> IncludeDeepProperties(this IQueryable<LogicalInterface> query)
        {
            return query.Include(x => x.RoutingInstance.RoutingInstanceType)
                        .Include(x => x.RoutingInstance.Device.DeviceRole);
        }
    }

    public enum LogicalInterfaceTypeEnum
    {
        Loopback,
        Tunnel
    }

    public class LogicalInterface : IModifiableResource
    {
        [Key]
        public int LogicalInterfaceID { get; private set; }
        [NotMapped]
        public string Name
        {
            get
            {
                return $"{LogicalInterfaceType} {ID}";
            }
        }
        public int RoutingInstanceID { get; set; }
        public int ID { get; set; }
        [MaxLength(15)]
        public string IpAddress { get; set; }
        [MaxLength(15)]
        public string SubnetMask { get; set; }
        public LogicalInterfaceTypeEnum LogicalInterfaceType { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the device
        /// </summary>
        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(this.Description)) throw new IllegalStateException($"A description for logical interface with ID '{this.ID}' is required.");
            if (this.RoutingInstance == null)
            {
                throw new IllegalStateException($"A routing instance for logical interface with ID '{this.ID}' is required.");
            }
            if (this.RoutingInstance.LogicalInterfaces
                                    .Where(
                                        x => 
                                        x.LogicalInterfaceID != this.LogicalInterfaceID  &&
                                        x.LogicalInterfaceType == this.LogicalInterfaceType && 
                                        x.ID == this.ID)
                                    .Any())
            {
                throw new IllegalStateException($"A logical interface of type '{this.LogicalInterfaceType.ToString()}' with ID '{this.ID}' already exists.");
            }
        }
    }
}