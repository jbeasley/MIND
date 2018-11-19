using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mind.WebUI.Models
{
    public enum PortRoleType
    {
        TenantFacing,
        ProviderInfrastructrure,
        TenantInfrastructure
    }

    public class PortRoleViewModel
    {
        public int PortRoleId { get; set; }
        [Display(Name = "Port Connector")]
        public string Name { get; set; }
        public string Description { get; set; }
        public PortRoleType PortRoleType { get; set; }
        public int DeviceRoleId { get; set; }
        public byte[] RowVersion { get; set; }
    }
}