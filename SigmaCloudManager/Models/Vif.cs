using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class Vif : IModifiableResource, IEquatable<Vif>
    {
        public int VifID { get; set; }
        public bool IsLayer3 { get; set; }
        public int AttachmentID { get; set; }
        [NotMapped]
        public string Name
        {
            get
            {
                return $"{Attachment.Name}.{VlanTag}";
            }
        }
        [Range(2,4094)]
        public int VlanTag { get; set; }
        public int? RoutingInstanceID { get; set; }
        public int? TenantID { get; set; }
        public int? ContractBandwidthPoolID { get; set; }
        public int VifRoleID { get; set; }
        public int? VlanTagRangeID { get; set; }
        public bool Created { get; set; }
        public bool RequiresSync { get; set; }
        public bool ShowCreatedAlert { get; set; }
        public bool ShowRequiresSyncAlert { get; set; }
        public int MtuID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Attachment Attachment { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
        [ForeignKey("ContractBandwidthPoolID")]
        public virtual ContractBandwidthPool ContractBandwidthPool { get; set; }
        public virtual VlanTagRange VlanTagRange { get; set; }
        [ForeignKey("VifRoleID")]
        public virtual VifRole VifRole { get; set; }
        [ForeignKey("MtuID")]
        public virtual Mtu Mtu { get; set; }
        public ICollection<Vlan> Vlans { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        public bool Equals(Vif other)
        {
            if (ReferenceEquals(null, this)) return false;
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return
                (
                    this.VifID == other.VifID ||
                    this.VifID.Equals(other.VifID)
                );
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;

                // Ignore hashing the name property - we need a deeply populated vif object to calulate
                // 'name' and we have enough properties to hash and avoid chance of collision
                hashCode = hashCode * 59 + this.VifID.GetHashCode();
                hashCode = hashCode * 59 + this.IsLayer3.GetHashCode();
                hashCode = hashCode * 59 + this.VlanTag.GetHashCode();
                hashCode = hashCode * 59 + this.AttachmentID.GetHashCode();
                hashCode = hashCode * 59 + this.TenantID.GetHashCode();
                if (this.RoutingInstance != null)
                    hashCode = hashCode * 59 + this.RoutingInstance.GetHashCode();
                if (this.Vlans != null)
                    hashCode = hashCode * 59 + this.Vlans.GetHashCode();
                if (this.ContractBandwidthPool != null)
                    hashCode = hashCode * 59 + this.ContractBandwidthPool.GetHashCode();
                return hashCode;
            }
        }
    }
}