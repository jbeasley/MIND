using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mind.WebUI.Models
{
    public class PortPoolViewModel
    {
        public int PortPoolId { get; set; }
        [Display(Name = "Port Pool")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int PortRoleID { get; set; }
        public byte[] RowVersion { get; set; }
    }
}