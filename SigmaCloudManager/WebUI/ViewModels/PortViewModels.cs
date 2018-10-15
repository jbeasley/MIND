using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    public class PortViewModel
    {
        [Display(AutoGenerateField = false)]
        public int ID { get; set; }
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A port type must be specified, e.g. GigabitEthernet")]
        public string Type { get; set; }
        [StringLength(50)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A port name must be specified, e.g. 0/0/0/0")]
        public string Name { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public byte[] RowVersion { get; set; }
        [Required(ErrorMessage = "A device must be selected")]
        public int DeviceID { get; set; }
        public DeviceViewModel Device { get; set; }
        public int? TenantID { get; set; }
        public TenantViewModel Tenant { get; set; }
        public int? InterfaceID { get; set; }
        [Required(ErrorMessage = "A port bandwidth must be selected")]
        public int PortBandwidthID { get; set; }
        [Display(Name="Port Bandwidth (Gbps)")]
        public PortBandwidthViewModel PortBandwidth { get; set; }
        [Required(ErrorMessage = "A port role must be selected")]
        public int PortRoleID { get; set; }
        [Display(Name = "Port Role")]
        public PortRoleViewModel PortRole { get; set; }
        [Required(ErrorMessage = "A port pool must be selected")]
        public int PortPoolID { get; set; }
        [Display(Name = "Port Pool")]
        public PortPoolViewModel PortPool { get; set; }
        [Required(ErrorMessage = "A port status must be selected")]
        public int PortStatusID { get; set; }
        [Display(Name = "Status")]
        public PortStatusViewModel PortStatus { get; set; }
        [Required(ErrorMessage = "A port connector must be selected")]
        public int PortConnectorID { get; set; }
        [Display(Name = "Connector")]
        public PortConnectorViewModel PortConnector { get; set; }
        [Required(ErrorMessage = "A port SFP must be selected")]
        public int PortSfpID { get; set; }
        [Display(Name = "SFP")]
        public PortSfpViewModel PortSfp { get; set; }
    }
}