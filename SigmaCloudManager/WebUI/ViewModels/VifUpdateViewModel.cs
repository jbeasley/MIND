using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Models.ViewModels
{
    public class VifUpdateViewModel : IValidatableObject
    {
        public int VifID { get; set; }
        public int AttachmentID { get; set; }
        public string Name { get; set; }
        public bool IsLayer3 { get; set; }
        public int? TenantID { get; set; }
        public TenantViewModel Tenant { get; set; }
        public int? ContractBandwidthPoolID { get; set; }
        [Display(Name = "Select a Contract Bandwidth Pool")]
        public ContractBandwidthPoolViewModel ContractBandwidthPool { get; set; }
        public int? ContractBandwidthID { get; set; }
        [Display(Name = "Trust Received COS and DSCP")]
        public bool TrustReceivedCosDscp { get; set; }
        [Display(Name = "Select a Contract Bandwidth (Megabits/Second)")]
        public ContractBandwidthViewModel ContractBandwidth { get; set; }
        public int? RoutingInstanceID { get; set; }
        [Display(Name = "Existing VRF")]
        public RoutingInstanceViewModel RoutingInstance { get; set; }
        [Display(Name = "Create a new VRF")]
        public bool CreateNewRoutingInstance { get; set; }
        public byte[] RowVersion { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ContractBandwidthID == null && ContractBandwidthPoolID == null)
            {
                yield return new ValidationResult(
                    "Either an existing Contract Bandwidth Pool or a Contract Bandwidth value must be selected.");
            }

            if (RoutingInstanceID == null && !CreateNewRoutingInstance)
            {
                yield return new ValidationResult(
                    "Either an existing VRF must be selected or a new VRF must be created.");
            }
        }
    }
}
