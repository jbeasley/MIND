using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Models.ViewModels
{
    public class AttachmentUpdateViewModel : IValidatableObject
    {
        public int AttachmentID { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int? MtuID { get; set; }
        [Display(Name="MTU (Bytes)")]
        public MtuViewModel Mtu { get; set; }
        public bool IsBundle { get; set; }
        [Display(Name = "Minimum number of active bundle links")]
        [Range(minimum:1,maximum:32, ErrorMessage = "Enter a value between 1 and 32")]
        public int? BundleMinLinks { get; set; }
        [Display(Name = "Maximum number of active bundle links")]
        [Range(minimum: 1, maximum:32, ErrorMessage = "Enter a value between 1 and 32")]
        public int? BundleMaxLinks { get; set; }
        public bool IsMultiPort { get; set; }
        public bool IsTagged { get; set; }
        public string Name { get; set; }
        public int? ContractBandwidthID { get; set; }
        [Display(Name = "Contract Bandwidth (Megabits/Second)")]
        public ContractBandwidthViewModel ContractBandwidth { get; set; }
        [Display(Name = "Trust Received COS and DSCP")]
        public bool TrustReceivedCosDscp { get; set; }
        public int? RoutingInstanceID { get; set; }
        [Display(Name = "Existing VRF")]
        public RoutingInstanceViewModel RoutingInstance { get; set; }
        [Display(Name = "Create a new VRF")]
        public bool CreateNewRoutingInstance { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [StringLength(250)]
        public string Notes { get; set; }
        public byte[] RowVersion { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MtuID == null)
            {
                yield return new ValidationResult(
                    "A Maximum Transmission Unit (MTU) option must be selected.");
            }
        }
    }
}
